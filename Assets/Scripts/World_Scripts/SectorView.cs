using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SectorView : MonoBehaviour
{
	public Sector Sector { get; private set; }
	public int2 Index { get; private set; }
	public CostMap costMap;

	public void InitilizeValues( int2 index )
	{
		Index = index;
	}

	public void InitializeSector()
	{
		Sector = new( transform.position, Index, costMap );
	}
}
