using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum WorldMap
{
	map_01,
	map_02,
	map_03,
}

public static class WorldData
{

	public static byte[,] indexArray_01 =
	{
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
	};

	public static byte[,] indexArray_02 =
	{
		{ 8, 8, 8, 8, 8, 8 },
		{ 8, 8, 8, 8, 8, 8 },
		{ 8, 8, 8, 8, 8, 8 },
		{ 8, 8, 8, 8, 8, 8 },
		{ 8, 8, 8, 8, 8, 8 },
		{ 8, 8, 8, 8, 8, 8 },
	};

	public static byte[,] indexArray_03 =
	{
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0 },
	};

	public static readonly Dictionary<WorldMap, byte[,]> sectorsByMap = new()
	{
		{WorldMap.map_01, indexArray_01 },
		{WorldMap.map_02, indexArray_02 },
		{WorldMap.map_03, indexArray_03 },
	};
}
