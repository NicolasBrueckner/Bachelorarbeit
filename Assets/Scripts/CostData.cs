using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CostMap
{
	map_01,
	map_02,
	map_03,
}

public class CostData
{
	private static readonly Lazy<CostData> _instance = new Lazy<CostData>( () => new CostData() );
	public static CostData Instance => _instance.Value;

	public Dictionary<CostMap, byte[,]> costs { get; private set; }

	private byte[,] costArray_1 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1, 255, 255, 255,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private byte[,] costArray_2 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1, 255,   1,   1,   1, 255,   1 },
		{   1, 255,   1, 255,   1, 255,   1 },
		{   1, 255,   1,   1,   1, 255,   1 },
		{   1, 255,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private byte[,] costArray_3 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1, 255,   1,   1,   1,   1 },
		{   1,   1, 255,   1,   1,   1,   1 },
		{   1,   2,   2,   2,   2, 255,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private CostData()
	{
		InitializeCosts();
	}

	private void InitializeCosts()
	{
		costs = new Dictionary<CostMap, byte[,]>
		{
			{ CostMap.map_01, costArray_1 },
			{ CostMap.map_02, costArray_2 },
			{ CostMap.map_03, costArray_3 },
		};
	}
}
