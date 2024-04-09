using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SectorData
{
	public Sector[] sectors;
	public Sprite[] backgrounds;
	public int2[] sizes;

	private void InitializeSectors()
	{
		sectors = new Sector[ 3 ];

	}
}
