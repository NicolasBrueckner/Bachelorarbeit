using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum SectorMap
{
	map_01,
	map_02,
	map_03,
}

public class SectorData
{
	private static readonly Lazy<SectorData> _instance = new Lazy<SectorData>( () => new SectorData() );
	public static SectorData Instance => _instance.Value;

	public Sector[] sectors;

	private void InitializeSectors()
	{
		sectors = new Sector[ 3 ];

	}
}
