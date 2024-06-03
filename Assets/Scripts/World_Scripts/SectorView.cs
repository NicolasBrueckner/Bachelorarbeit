using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using stats = SectorStats;

public class SectorView : MonoBehaviour
{
	public Sector Sector { get; private set; }
	public int2 Index { get; private set; }

	public void InitilizeSectorView( int2 index )
	{
		Index = index;
		ScaleSectorObject();
		Sector = new Sector( transform.position, Index );
	}

	private void ScaleSectorObject()
	{
		transform.localScale *= stats.gridScalar;
	}
}
