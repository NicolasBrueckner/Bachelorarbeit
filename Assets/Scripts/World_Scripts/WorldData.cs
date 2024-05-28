using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum WorldMap
{
	map_01,
	map_02,
	blank,
}

public static class WorldData
{

	public static byte[,] indexArray_01 =
	{
		{ 6, 1, 2, 6, 3, 1, 5, 0, 0, 0 },
		{ 6, 8, 4, 5, 3, 0, 6, 0, 0, 0 },
		{ 5, 8, 4, 6, 1, 2, 6, 1, 0, 0 },
		{ 7, 4, 7, 5, 4, 7, 8, 6, 1, 0 },
		{ 6, 3, 1, 5, 7, 8, 0, 2, 0, 0 },
		{ 5, 8, 7, 5, 3, 0, 0, 0, 0, 0 },
		{ 8, 7, 8, 6, 4, 0, 5, 2, 0, 0 },
		{ 7, 0, 7, 6, 1, 0, 5, 3, 5, 2 },
		{ 8, 7, 8, 6, 8, 0, 6, 7, 5, 2 },
		{ 1, 1, 1, 3, 7, 0, 5, 4, 6, 2 },
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

	public static byte[,] blank =
	{
		{ 9, 9, 9, 9, 9, 9 },
		{ 9, 9, 9, 9, 9, 9 },
		{ 9, 9, 9, 9, 9, 9 },
		{ 9, 9, 9, 9, 9, 9 },
		{ 9, 9, 9, 9, 9, 9 },
		{ 9, 9, 9, 9, 9, 9 },
	};

	public static readonly Dictionary<WorldMap, byte[,]> sectorsByMap = new()
	{
		{WorldMap.map_01, indexArray_01 },
		{WorldMap.map_02, indexArray_02 },
		{WorldMap.blank, blank },
	};
}
