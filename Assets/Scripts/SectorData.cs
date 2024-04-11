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

	public Dictionary<SectorMap, byte[,]> sectors { get; private set; }

	public byte[,] indexArray_01 =
	{
		{ 1, 0, 0, 2, 0, 2 },
		{ 0, 1, 2, 1, 0, 0 },
		{ 0, 2, 0, 2, 0, 2 },
		{ 2, 0, 2, 0, 2, 0 },
		{ 1, 0, 1, 1, 0, 2 },
		{ 0, 0, 2, 0, 1, 0 },
	};

	public byte[,] indexArray_02 =
	{
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
	};

	public byte[,] indexArray_03 =
	{
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
	};

	private SectorData()
	{
		InitializeSectors();
	}

	private void InitializeSectors()
	{
		sectors = new Dictionary<SectorMap, byte[,]>
		{
			{SectorMap.map_01, indexArray_01 },
			{SectorMap.map_02, indexArray_02 },
			{SectorMap.map_03, indexArray_03 },
		};
	}
}
