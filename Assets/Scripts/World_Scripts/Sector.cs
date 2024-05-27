using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public enum CostMap
{
	map_01_01,
	map_02_01,
	map_03_01,
	map_03_02,
	map_03_03,
	map_04_01,
	map_04_02,
	map_04_03,
	map_05_01,
	map_05_02,
}

[System.Serializable]
public class Sector
{
	public float3 position;
	public int2 index;
	public byte[,] costs;

	public Sector( float3 position, int2 index, CostMap costMap )
	{
		this.position = position;
		this.index = index;

		costs = costsBySector[ costMap ];
	}

	private static readonly byte[,] costs_01_01 =
	{
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2, 255 },
		{   2,   2,   2, 255, 255, 255, 255, 255, 255,   2,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255,   2,   2 },
		{ 255,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2,   2, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2,   2,   2, 255, 255, 255, 255, 255,   3,   3 },
		{ 255,   2,   2,   2,   2,   2,   2,   3,   3,   3,   3 },
		{   2,   2,   2,   2,   2,   2,   2,   3, 255,   3,   3 },
		{   2,   2, 255,   2,   2,   2,   2,   3,   3,   3,   3 },
	};

	private static readonly byte[,] costs_02_01 =
	{
		{ 255,   2, 255,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2, 255,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2,   2,   3,   3,   3,   2,   2,   2,   2,   2 },
		{   2,   2,   3,   3,   3,   3,   3,   2,   2,   2,   2 },
		{   2,   2,   3,   3,   3,   3,   3,   2,   2,   2,   2 },
		{   2,   2,   3,   3,   3,   3,   3,   2,   2,   2,   2 },
		{   2,   2, 255,   3,   3,   3, 255,   2,   2,   2,   2 },
		{   2,   2, 255,   2,   2,   2, 255,   2,   2,   2,   2 },
		{ 255, 255, 255,   2,   2,   2, 255, 255, 255, 255, 255 },
		{ 255,   2,   2,   2,   2,   2,   2,   2,   2,   2, 255 },
		{ 255,   2,   2,   2,   2,   2,   2,   2,   2,   2, 255 },
		{ 255,   2,   2,   2,   2,   3,   3,   2,   2,   2, 255 },
		{ 255,   2,   2,   2,   2,   3,   3,   2,   2,   2, 255 },
		{ 255, 255, 255, 255,   2,   2,   2, 255, 255, 255, 255 },
	};

	private static readonly byte[,] costs_03_01 =
	{
		{ 255, 255, 255, 255,   2,   2,   2, 255, 255, 255, 255 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2, 255 },
		{   2, 255, 255, 255, 255,   1, 255, 255, 255,   2, 255 },
		{   2, 255,   1,   1, 255,   1,   1,   1, 255,   2, 255 },
		{   2, 255,   1,   1, 255,   1,   1,   1, 255,   2, 255 },
		{   2, 255,   1,   1, 255,   1,   1,   1, 255,   2, 255 },
		{   2, 255,   1,   1,   1,   1,   1,   1, 255,   2,   2 },
		{   2, 255,   1,   1, 255,   1,   1,   1, 255,   2,   2 },
		{   2, 255, 255, 255, 255,   1,   1,   1, 255,   2,   2 },
		{   2, 255,   1,   1,   1,   1, 255,   1, 255,   2, 255 },
		{   2,   1,   1,   1,   1,   1, 255,   1, 255,   2, 255 },
		{   2, 255,   1,   1,   1,   1, 255,   1, 255,   2, 255 },
		{   2, 255, 255, 255, 255,   1, 255, 255, 255,   2, 255 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2, 255 },
		{ 255, 255, 255, 255,   2,   2,   2, 255, 255, 255, 255 },
	};

	private static readonly byte[,] costs_03_02 =
	{
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2, 255, 255, 255, 255, 255, 255, 255, 255, 255 },
		{   1,   1,   1,   1, 255,   1,   1,   1, 255,   2,   2 },
		{   1,   1,   1,   1,   1,   1,   1,   1,   1,   2,   2 },
		{   1,   1,   1,   1, 255,   1,   1,   1, 255,   2,   2 },
		{   2, 255, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2, 255,   1,   1,   1,   1, 255,   1,   1, 255,   2 },
		{   2,   1,   1,   1,   1,   1,   1,   1,   1, 255,   2 },
		{   2, 255,   1,   1,   1,   1, 255,   1,   1, 255,   2 },
		{   2, 255, 255,   1,   1,   1, 255, 255, 255, 255,   2 },
		{   2, 255,   1,   1, 255,   1,   1,   1,   1, 255,   2 },
		{   2, 255,   1,   1, 255,   1,   1,   1,   1,   1,   2 },
		{   2, 255,   1,   1, 255,   1,   1,   1,   1, 255,   2 },
		{   2, 255, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
	};

	private static readonly byte[,] costs_03_03 =
	{
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2, 255, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{ 255, 255,   1,   1,   1,   1,   1,   1,   1, 255,   2 },
		{ 255, 255,   1,   1, 255,   1,   1,   1,   1,   1,   2 },
		{ 255, 255,   1,   1, 255,   1, 255, 255, 255, 255,   2 },
		{   2, 255,   1,   1, 255,   1, 255,   1,   1, 255,   2 },
		{   2, 255, 255, 255, 255,   1, 255,   1,   1, 255,   2 },
		{   2,   1,   1,   1,   1,   1, 255,   1,   1, 255,   2 },
		{   2, 255,   1,   1,   1,   1,   1,   1,   1, 255,   2 },
		{   2, 255, 255, 255, 255, 255, 255, 255, 255, 255,   2 },
		{   1,   1,   1,   1, 255,   1,   1,   1, 255, 255,   2 },
		{   1,   1,   1,   1,   1,   1,   1,   1,   1,   2,   2 },
		{   1,   1,   1,   1, 255,   1,   1,   1, 255,   2,   2 },
		{   2,   2,   2,   2, 255, 255, 255, 255, 255,   2,   2 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
	};

	private static readonly byte[,] costs_04_01 =
	{
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1, 255, 255,   3,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   3,   3,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   3,   3,   2 },
		{   2,   2,   2,   1,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   2, 255, 255,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   2, 255, 255,   1,   1,   1, 255, 255,   2,   2 },
		{   2,   2,   2, 255, 255,   1,   1,   1,   2,   2,   2 },
		{   2,   3,   3, 255, 255,   1,   1,   1,   2,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1,   1,   2,   2,   2 },
	};

	private static readonly byte[,] costs_04_02 =
	{
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   3,   3, 255, 255,   1,   1,   1,   2,   2,   2 },
		{   2,   3,   3, 255, 255,   1,   1,   1,   2, 255,   2 },
		{   2,   3, 255, 255,   1,   1,   1,   1,   3,   2,   2 },
		{   2,   3, 255, 255,   1,   1,   1,   1,   3,   2,   2 },
		{   2,   3,   3,   1,   1,   1,   1,   1,   3,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2, 255,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1, 255,   2,   2 },
		{   2, 255,   2,   1,   1,   1,   1,   1, 255,   3,   3 },
		{   2, 255,   2,   1,   1,   1,   1,   1,   3, 255,   3 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   3,   3,   3 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   3,   3,   3 },
	};

	private static readonly byte[,] costs_04_03 =
	{
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
		{   2,   2,   2,   1,   1,   1,   1,   1,   2,   2,   2 },
	};

	private static readonly byte[,] costs_05_01 =
	{
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2, 255,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   3,   3,   2,   2,   2,   2,   2,   2, 255,   2 },
		{   2,   3,   3,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   3,   3,   2,   3,   3,   3,   3,   3,   3,   2 },
		{   2,   2,   2,   2,   3, 255,   3,   3,   3,   3,   2 },
		{   2,   2,   2,   2,   3, 255,   3,   3,   3,   3,   2 },
		{   2,   2,   2,   2,   3, 255,   3,   3,   3,   3,   2 },
		{   2,   2,   2,   2,   3,   3,   3,   3,   3,   3,   2 },
		{   2,   2,   2,   2,   3,   3,   3,   3,   3,   3,   2 },
		{   3,   3,   3,   2,   3,   3,   3,   3,   3,   3,   2 },
		{   3, 255,   3,   2,   3,   3,   3, 255,   3,   3,   2 },
		{   3,   3,   3,   2, 255, 255,   3,   3,   3,   3,   2 },
		{   3,   3,   3,   2,   3, 255, 255,   3,   3,   3,   2 },
		{   3,   3,   3,   2,   2,   2,   2,   2,   2,   2,   2 },
	};

	private static readonly byte[,] costs_05_02 =
	{
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2, 255,   2,   2,   2,   2,   2,   2, 255, 255 },
		{   2, 255,   2,   2,   2,   3,   3,   3, 255, 255,   2 },
		{   2,   2,   2,   2,   2,   3,   3,   3,   3,   2,   2 },
		{   2,   2,   2,   2,   2,   3,   3,   3,   3,   2,   2 },
		{   2,   3,   3,   2,   2,   3,   3,   3,   3,   2,   2 },
		{   2,   3,   3,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   3,   3,   2,   2,   2,   3,   3,   2,   2,   2 },
		{   2,   3,   3,   2,   2,   2,   3,   3,   2,   2,   2 },
		{   2,   2,   3,   3,   3,   2,   3,   3,   2,   2,   2 },
		{   2,   2,   3,   3,   3,   2,   3,   3,   2,   2,   2 },
		{   2,   2,   3,   3,   3,   2,   2,   2,   2, 255,   2 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
		{   2,   2,   2,   2,   2,   2,   2,   2,   2,   2,   2 },
	};

	public static readonly float cellRadius = 0.5f;
	public static readonly float cellDiameter = cellRadius * 2;
	public static readonly int2 gridSize = new( costs_01_01.GetLength( 0 ), costs_01_01.GetLength( 1 ) );
	public static readonly float2 sectorSize = new( gridSize.x * cellDiameter, gridSize.y * cellDiameter );

	public static readonly Dictionary<CostMap, byte[,]> costsBySector = new()
	{
		{ CostMap.map_01_01, costs_01_01 },
		{ CostMap.map_02_01, costs_02_01 },
		{ CostMap.map_03_01, costs_03_01 },
		{ CostMap.map_03_02, costs_03_02 },
		{ CostMap.map_03_03, costs_03_03 },
		{ CostMap.map_04_01, costs_04_01 },
		{ CostMap.map_04_02, costs_04_02 },
		{ CostMap.map_04_03, costs_04_03 },
		{ CostMap.map_05_01, costs_05_01 },
		{ CostMap.map_05_02, costs_05_02 },
	};
}
