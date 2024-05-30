using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SectorView : MonoBehaviour
{
	public Sector Sector { get; private set; }
	public int2 Index { get; private set; }
	public CostMap costMap;

	public void InitilizeSectorView( int2 index )
	{
		Index = index;
		ScaleSectorObject();
		Sector = new Sector( transform.position, Index, costMap );
	}

	private void ScaleSectorObject()
	{
		float width = SectorStats.Instance.sectorSize.x;
		float height = SectorStats.Instance.sectorSize.y;

		transform.localScale = new( width, height, 0 );
	}
}
