using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CostType
{
	type1,
	type2,
	type3,
}

public class CostData
{
	private static readonly Lazy<CostData> _instance = new Lazy<CostData>( () => new CostData() );
	public static CostData Instance => _instance.Value;

	public Dictionary<CostType, byte[,]> costs { get; private set; }

	private byte[,] cost_1 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1,   1,   1, 255,   1,   1 },
		{   1,   1, 255, 255, 255,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private byte[,] cost_2 =
	{
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
		{   1, 255,   1,   1,   1, 255,   1 },
		{   1, 255,   1, 255,   1, 255,   1 },
		{   1, 255,   1,   1,   1, 255,   1 },
		{   1, 255,   1,   1,   1,   1,   1 },
		{   1,   1,   1,   1,   1,   1,   1 },
	};

	private byte[,] cost_3 =
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
		costs = new Dictionary<CostType, byte[,]>
		{
			{ CostType.type1, cost_1 },
			{ CostType.type2, cost_2 },
			{ CostType.type3, cost_3 },
		};
	}
}
