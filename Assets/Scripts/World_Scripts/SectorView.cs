using Unity.Mathematics;
using UnityEngine;

public class SectorView : MonoBehaviour
{
	public int2 Index;
	public Sector Sector { get; private set; }

	public void InitilizeSectorView( int2 index )
	{
		Index = index;
		//ScaleSectorObject();
		Sector = new Sector( transform.position, Index );
	}

	//private void ScaleSectorObject()
	//{
	//	transform.localScale *= stats.gridScalar;
	//}
}
