using UnityEngine;

namespace SSFS
{
	[RequireComponent(typeof(MeshRenderer))]
	public class SSFS_GeneratorTester : MonoBehaviour
	{
		MeshRenderer _mr;
		MeshRenderer mr { get { if ( _mr == null ) _mr = GetComponent<MeshRenderer>(); return _mr; } }
		Material mat;

		public SSFSGenerator generator = null;
		public KeyCode key = KeyCode.R;

		private void Start()
		{
			mat = generator.GenerateMaterial();
			mr.material = mat;
		}

		private void Update()
		{
			if ( Input.GetKeyDown( key ) )
			{
				if ( generator == null )
					Debug.Log( "Null SSFS Generator" );
				else
					generator.GenerateMaterial( ref mat );
			}
		}
	}
}