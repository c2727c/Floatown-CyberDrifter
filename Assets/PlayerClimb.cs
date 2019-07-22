using System.Collections;

using System.Collections.Generic;

using UnityEngine;




[RequireComponent(typeof(Rigidbody))]

public class PlayerClimb : MonoBehaviour

{

public GravityCollider GravityColliderScript;




private Rigidbody playerRig;//就是XR_Rig=玩家的Rigidbody，




private List<PlayerInput_Interactor> ClimbInfluencers;//【这里不是被抓物体列表，而是左右手，PlayerInput_Interactor是左右手上的脚本

private GameObject firstHeldObj;//【新增】记录第一个被抓物







private Vector3 ClimbXRRigReference;//【】

private Vector3 CollisionCorrection;




private Vector3 BodyCollisionPosition;

private Vector3 LastKnownGoodPosition;

private bool Colliding;

private float CorrectionRecoveryRate = 0.01f;




void Awake()

{

playerRig = GetComponent<Rigidbody>();

ClimbInfluencers = new List<PlayerInput_Interactor>(2);

CollisionCorrection = Vector3.zero;

}




public void AddInfluencer(PlayerInput_Interactor NewInfluencer)//抓住时。被谁调用：

{

if (ClimbInfluencers.Contains(NewInfluencer))

return;

ClimbInfluencers.Add(NewInfluencer);//记录被抓物

if(ClimbInfluencers.Count == 1){

firstHeldObj=NewInfluencer.GetHeldObject();

}

playerRig.useGravity = false;//AddInfluencer时，玩家不算重力，很可能是抓取时

GravityColliderScript.CollisionStatusChanged += HandleCollision;




ResetReferences();

}




public void RemoveInfluencer(PlayerInput_Interactor NewInfluencer)

{

if (ClimbInfluencers.Contains(NewInfluencer))

{

GravityColliderScript.CollisionStatusChanged -= HandleCollision;

ClimbInfluencers.Remove(NewInfluencer);

}




if (ClimbInfluencers.Count == 0)

{

playerRig.useGravity = true;//RemoveInfluencer，放手时，算重力

CollisionCorrection = Vector3.zero;

Colliding = false;

}

}




// Update is called once per frame

void FixedUpdate()

{

if (!Colliding)

{

LastKnownGoodPosition = playerRig.position;

}




if (ClimbInfluencers.Count != 0)

{

Vector3 TargetPositionDeltaAverage = Vector3.zero;

for (int i = 0; i < ClimbInfluencers.Count;
i++)

{

TargetPositionDeltaAverage += ClimbInfluencers[i].XRRigTargetDelta;

}

TargetPositionDeltaAverage /= ClimbInfluencers.Count;


if (Colliding)

CollisionCorrection += LastKnownGoodPosition - BodyCollisionPosition;

else if (CollisionCorrection != Vector3.zero)

{

CollisionCorrection -= CollisionCorrection.normalized * Mathf.Min(CorrectionRecoveryRate,
CollisionCorrection.magnitude);

}




Debug.Log("CollisionCorrection " + CollisionCorrection);




playerRig.MovePosition(

firstHeldObj.transform.position//[NEW]

+ ClimbXRRigReference

+ CollisionCorrection 

+ TargetPositionDeltaAverage

);




/*Rigidbody（刚体）的成员函数 void MovePosition(Vector3 position) 

是用来改变刚体对象的 transform.position 值的，

而且是“瞬间”改变到新的位置，而不是“逐渐移动过去”。

如果它的新位置上有碰撞器，那么会在下一次进入 Update 时立即对刚体应用受力。

在新旧两个位置之间的其它刚体不会受到它移动的影响（因为它是瞬间穿过去的）。

【经常把 rigidbody.MovePosition 放到物理帧更新的函数 FixedUpdate，让它步进移动，这样就会碰撞一路上的其他碰撞体而不是穿越过去】 */

/* 新的位置 = ClimbXRRigReference + CollisionCorrection + TargetPositionDeltaAverage

1.ClimbXRRigReference 每次ResetReferences即抓住东西时 重置为 playerRig.position即玩家的世界坐标;




2.CollisionCorrection在一开始和每次松手时置为0，

Colliding即正在碰撞标志位，初始为false，每次全松手时置为false

没在碰撞的每一帧，置LastKnownGoodPosition为玩家世界坐标。

在碰撞的时候，置BodyCollisionPosition为玩家世界坐标

在攀爬的每一帧，若正在碰撞，置CollisionCorrection为LastKnownGoodPosition - BodyCollisionPosition;

CollisionCorrection的引入即在抓住climbable时，若碰撞到其他物体，会受到阻力而不是自由穿过。




3.TargetPositionDeltaAverage在抓住的每一帧计算，为左右手移动量的平均值。




###若加上被抓物体运动后玩家跟随运动的影响，则ClimbXRRigReference应换为 移动前人的位置减移动前被抓物的位置+移动后被抓物的位置，而且应该抓住中的每帧都更新

移动钱即抓住前，把ClimbXRRigReference换为人-物，在这里再加上物体新位置

*/

}

}




void HandleCollision(Transform transform, bool colliding)

{

Debug.Log("HandleCollision. colliding = " + colliding);




Colliding = colliding;




if (Colliding)

BodyCollisionPosition = playerRig.position;

}




void ResetReferences()

{

ClimbXRRigReference = playerRig.position-firstHeldObj.transform.position;//[NEW]




for (int i = 0; i < ClimbInfluencers.Count;
i++)

{

ClimbInfluencers[i].ResetReference();

}

}

}