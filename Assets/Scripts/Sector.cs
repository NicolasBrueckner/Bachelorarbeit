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
	map_01,
	map_02,
	map_03,
}

[System.Serializable]
public class Sector
{
	public float3 position;
	public uint2 index;
	public byte[,] costs;

	public static readonly float cellRadius = 0.5f;
	public static readonly float cellDiameter = cellRadius * 2;
	public static readonly int2 gridSize = new int2( 10, 10 );
	public static readonly float2 sectorSize = new float2( 10f, 10f );

	public Sector( float3 position, uint2 index, CostMap costMap )
	{
		this.position = position;
		this.index = index;

		costs = costsBySector[ costMap ];
	}

	private static byte[,] costArray_1 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1, 255, 255, 255,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private static byte[,] costArray_2 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1, 255,   1,   1,   1, 255,   1 },
		{   1, 255,   1, 255,   1, 255,   1 },
		{   1, 255,   1,   1,   1, 255,   1 },
		{   1, 255,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private static byte[,] costArray_3 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1, 255,   1,   1,   1,   1 },
		{   1,   1, 255,   1,   1,   1,   1 },
		{   1,   2,   2,   2,   2, 255,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	public static readonly Dictionary<CostMap, byte[,]> costsBySector = new()
	{
		{ CostMap.map_01, costArray_1 },
		{ CostMap.map_02, costArray_2 },
		{ CostMap.map_03, costArray_3 },
	};
}
