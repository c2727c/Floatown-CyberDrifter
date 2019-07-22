using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class AlleyDemoControl : MonoBehaviour
{
	[Header("Dimensions")]
	public float width = 6f;
	public float height = 4f;
	public float depth = 24f;

	public float speed = 5f;

	[Header("Population")]
	public int maxBlocks = 200;
	public int maxPebbles = 200;
	public int maxWires = 50;
	public int maxSigns = 150;

	[Header("Rendering")]
	public Color fogColor = Color.white;
	public Material blockMaterial;
	public Material pebbleMaterial;
	public Material wireMaterial;

	public SSFSGenerator ssfsGenerator;

	Light sun;
	Transform[] leftblocks;
	Transform[] rightblocks;
	Transform[] pebbles;
	Transform[] signs;
	List<Transform> wires = new List<Transform>();
	Vector3 tunnelMovement { get { return -Vector3.forward * Time.deltaTime * speed * ( 1f + 3f * Mathf.PerlinNoise( Time.time * 0.05f , 111.111f ) ); } }

	private void Start()
	{
		CreateSun();

		CreateWall( Vector3.back , new Vector3( 0f , height * 5f , depth ) , new Vector3( width * 5f , 1f , height * 2.5f ) , true );//light wall
		CreateWall( Vector3.up , new Vector3( 0f , 0f , depth * 0.5f ) , new Vector3( height * 0.1f , 1f , depth * 0.1f ) );
		CreateWall( Vector3.left , new Vector3( width * 0.5f , height * 0.5f , depth * 0.5f ) , new Vector3( height * 0.1f , 1f , depth * 0.1f ) );
		CreateWall( Vector3.right , new Vector3( -width * 0.5f , height * 0.5f , depth * 0.5f ) , new Vector3( height * 0.1f , 1f , depth * 0.1f ) );

		PopulateBlockList( ref leftblocks , -1f );
		PopulateBlockList( ref rightblocks , 1f );
		PopulateSigns();
		PopulatePebbles();
	}

	private void Update()
	{
		UpdateBlocks( ref leftblocks , -1f );
		UpdateBlocks( ref rightblocks , 1f );
		UpdateDetail();

		//fogColor = Color.HSVToRGB( Mathf.PerlinNoise( Time.time * 0.1f , 31.13415f ) , 0.25f , 1f );
		Shader.SetGlobalFloat( "_TunnelDemoDepth" , depth );
		Shader.SetGlobalColor( "_TunnelDemoFogColor" , fogColor );
		sun.color = fogColor;

		Vector3 campos = Vector3.up;
		campos.x += ( -1f + 2f * Mathf.PerlinNoise( Time.time * 0.01f , 1f ) ) * width * 0.5f;
		campos.y += Mathf.PerlinNoise( 2f , Time.time * 0.02f );
		campos.z += Mathf.PerlinNoise( Time.time * 0.03f , 3f );
		transform.position = campos;
		transform.rotation = Quaternion.Euler( Mathf.PerlinNoise( 1.11f , Time.time * 0.01f ) * 10f - 10f , Mathf.PerlinNoise( Time.time * 0.02f , 0.5f ) * 5f , 12f * Mathf.Sin( Time.time * 0.01f ) );
		sun.transform.forward = new Vector3( -0.5f + Mathf.PerlinNoise( 11.111f , Time.time * 0.01f ) , -1f , -1f ).normalized;
	}

	private void CreateSun ()
	{
		GameObject o = new GameObject( "Sun" );
		o.hideFlags = HideFlags.HideInHierarchy;
		sun = o.AddComponent<Light>();
		sun.type = LightType.Directional;
		sun.transform.rotation = Quaternion.Euler( 35f , 170f , 0f );
		sun.intensity = 1.2f;
		sun.shadows = LightShadows.Soft;
		sun.shadowStrength = 0.95f;
		sun.shadowResolution = LightShadowResolution.VeryHigh;
		sun.shadowBias = 0.025f;
		sun.shadowNormalBias = 0f;
		sun.shadowNearPlane = 5f;
	}

	private void RandColor( ref Transform block , float minValue = 0.2f , float valueRange = 0.4f )
	{
		MaterialPropertyBlock matprops = new MaterialPropertyBlock();
		matprops.SetColor( "_Color" , Color.HSVToRGB( Random.value , Random.value * 0.3f , Random.value * valueRange + minValue ) );
		block.GetComponent<MeshRenderer>().SetPropertyBlock( matprops );
	}

	void CreateWall( Vector3 up , Vector3 pos , Vector3 scl , bool noshadow = false )
	{
		Transform w = GameObject.CreatePrimitive( PrimitiveType.Plane ).transform;
		w.hideFlags = HideFlags.HideInHierarchy;
		MeshRenderer mr = w.GetComponent<MeshRenderer>();
		mr.shadowCastingMode = noshadow ? ShadowCastingMode.Off : ShadowCastingMode.TwoSided;
		mr.sharedMaterial = blockMaterial;
		w.up = up;
		w.position = pos;
		w.localScale = scl;
	}

	void PopulateBlockList( ref Transform[] blocks , float side )
	{
		blocks = new Transform[ maxBlocks ];
		for ( int i = 0 ; i < maxBlocks ; i++ )
		{
			blocks[ i ] = GameObject.CreatePrimitive( PrimitiveType.Cube ).transform;
			blocks[ i ].hideFlags = HideFlags.HideInHierarchy;
			blocks[ i ].GetComponent<MeshRenderer>().sharedMaterial = blockMaterial;
			bool bridge = Random.value > 0.98f;
			ScaleBlock( ref blocks[ i ] , bridge );
			PositionBlock( ref blocks[ i ] , side * width * 0.5f , bridge );
			Vector3 p = blocks[i].position;
			p.z = Random.value * depth;
			blocks[ i ].position = p;
			RandColor( ref blocks[ i ] );
		}
	}

	void PopulatePebbles()
	{
		pebbles = new Transform[ maxPebbles ];
		for ( int i = 0 ; i < maxPebbles ; i++ )
		{
			pebbles[ i ] = GameObject.CreatePrimitive( PrimitiveType.Cube ).transform;
			pebbles[ i ].hideFlags = HideFlags.HideInHierarchy;
			MeshRenderer mr = pebbles[ i ].GetComponent<MeshRenderer>();
			mr.sharedMaterial = pebbleMaterial;
			mr.shadowCastingMode = ShadowCastingMode.Off;//we don't need the tiny pebbles on the floor to cast shadows
			NewPebblePos( ref pebbles[ i ] , true );
			RandColor( ref pebbles[ i ] , 0.25f , 0.5f );
		}
	}

	private void ScaleBlock( ref Transform block , bool bridge )
	{
		Vector3 s = new Vector3( Random.value * 2f + 0.2f , Random.value * height + 0.5f , Random.value * depth * 0.1f + 0.25f);
		if ( bridge ) { s.x = width; s.y = 1f; s.z = Random.value * 3f + 1f; }
		block.localScale = s;
	}

	private void PositionBlock(ref Transform block , float xpos , bool bridge )
	{
		Vector3 p = new Vector3( xpos , Random.value * height , depth );
		if ( bridge ) { p.x = 0f; p.y = Mathf.Max( p.y , 3f ); }
		block.position = p;
	}

	private void TripBlock( ref Transform block , float side )
	{
		bool bridge = Random.value > 0.98f;
		ScaleBlock( ref block , bridge );
		PositionBlock( ref block , side * width * 0.5f , bridge );

		if ( block.position.y > 3f && Random.value > 0.8f && wires.Count < maxWires )
			CreateWire( block.position , -Mathf.Sign( block.position.x ) * Vector3.right * width + Vector3.up * Random.value );
	}

	private void PopulateSigns ()
	{
		signs = new Transform[ maxSigns ];
		if ( ssfsGenerator == null ) return;

		for ( int i = 0 ; i < maxSigns ; i ++ )
		{
			GameObject o = GameObject.CreatePrimitive( PrimitiveType.Quad );
			o.hideFlags = HideFlags.HideInHierarchy;
			o.transform.localScale = Vector3.one * ( 2f * Random.value + 1f );
			Vector3 p = NewSignPosition();
			o.transform.position = p;
			o.transform.forward = Random.value > 0.8f ? Vector3.forward : Mathf.Sign( p.x ) * Vector3.right;
			//Use an SSFSGenerator to generate a random sign material:
			MeshRenderer mr = o.GetComponent<MeshRenderer>();
			mr.sharedMaterial = ssfsGenerator.GenerateMaterial();
			mr.shadowCastingMode = ShadowCastingMode.Off;
			Light l = o.AddComponent<Light>();
			l.color = Color.HSVToRGB( Random.value , Random.value * 0.5f + 0.5f , 1f );
			l.range = 2f;
			signs[ i ] = o.transform;
		}
	}

	private Vector3 NewSignPosition ( bool atEnd = false )
	{
		Vector3 pos = Vector3.zero;
		Vector3 wallpos = new Vector3( 0f , ( Random.value * 0.8f + 0.1f ) * height , atEnd ? depth - 5f : Random.value * depth );
		RaycastHit h;
		Ray r = new Ray( wallpos , Random.value > 0.5f ? Vector3.right : Vector3.left );
		if(Physics.Raycast(r,out h))
		{
			pos = h.point + h.normal * ( Random.value + 0.1f );
		}
		return pos;
	}

	private void UpdateBlocks( ref Transform[] blocks , float side )
	{
		for ( int i = 0 ; i < maxBlocks ; i++ )
		{
			blocks[ i ].position += tunnelMovement;
			if ( blocks[ i ].position.z < -blocks[ i ].localScale.z )
				TripBlock( ref blocks[ i ] , side );
		}
	}

	private void NewPebblePos( ref Transform pebble , bool randomDepth = false)
	{
		Vector3 p = new Vector3( width * ( Random.value - 0.5f ) , 0f , randomDepth ? Random.value * depth : depth );
		Vector3 s = Random.onUnitSphere * 0.1f + Vector3.one * 0.2f;
		s.y *= 0.5f;
		pebble.position = p;
		pebble.localScale = s;
	}

	private void UpdateDetail ()
	{
		for ( int i = wires.Count - 1 ; i > -1 ; i-- )
		{
			wires[ i ].position += tunnelMovement;
			if ( wires[ i ].position.z < -1f )
			{
				Destroy( wires[ i ].gameObject );
				wires.RemoveAt( i );
			}
		}

		for ( int i = 0 ; i < maxSigns ; i++ )
		{
			if ( signs[ i ] != null )
			{
				signs[ i ].position += tunnelMovement;
				if ( signs[ i ].position.z < -1f )
				{
					signs[ i ].position = NewSignPosition( true );
					signs[ i ].forward = Random.value > 0.8f ? Vector3.forward : Mathf.Sign( signs[ i ].position.x ) * Vector3.right;
					Material m = signs[ i ].GetComponent<MeshRenderer>().sharedMaterial;
					ssfsGenerator.GenerateMaterial( ref m );

					Light l = signs[ i ].GetComponent<Light>();
					if ( l != null )
					{
						l.color = Color.HSVToRGB( Random.value , Random.value * 0.5f + 0.5f , 1f );
						l.range = 2f;
					}
				}
			}
		}

		for ( int i = 0 ; i < maxPebbles ; i++ )
		{
			pebbles[ i ].position += tunnelMovement;
			if ( pebbles[ i ].position.z < -1f )
				NewPebblePos( ref pebbles[ i ] );
		}
	}

	public int maxWireVerts = 7;

	private void CreateWire(Vector3 p1 , Vector3 p2)
	{
		GameObject o = new GameObject();
		o.hideFlags = HideFlags.HideInHierarchy;
		o.transform.position = p1;
		LineRenderer l = o.AddComponent<LineRenderer>();
		l.material = wireMaterial;
		Vector3[] verts = new Vector3[ maxWireVerts ];
		for ( int i = 0 ; i < maxWireVerts ; i++ )
		{
			float p = ( float )i / ( ( float )maxWireVerts - 1f );
			verts[ i ] = Vector3.Lerp( Vector3.zero , p2 , p );
			verts[ i ] -= Vector3.up * p2.magnitude * 0.1f * ( 1f - Mathf.Pow( 2f * p - 1f , 2f ) );
		}
		l.useWorldSpace = false;
		l.widthMultiplier = ( Random.value + 0.1f ) * 0.1f;
		l.positionCount = maxWireVerts;
		l.SetPositions( verts );
		wires.Add( o.transform );
	}
}