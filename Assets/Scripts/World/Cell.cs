using System;
using Unity.Mathematics;
using UnityEngine;

public class Cell
{
	public bool realDirection;

	public float3 position;
	public int2 index;
	public byte cost;
	public short integrationCost;


	private float2 _directFlowDirection;
	private Direction _realFlowDirection;
	private readonly byte _defaultCost;
	private readonly short _defaultIntegrationCost;

	public Cell( float3 position, int2 index, byte cost )
	{
		this.position = position;
		this.index = index;
		this.cost = cost;

		integrationCost = short.MaxValue;

		_defaultCost = cost;
		_defaultIntegrationCost = integrationCost;
	}

	public object Direction
	{
		get
		{
			if ( _realFlowDirection != null )
				return _realFlowDirection;
			else
				return _directFlowDirection;
		}
		set
		{
			if ( value is Direction direction )
				_realFlowDirection = direction;
			else if ( value is float2 float2 )
				_directFlowDirection = math.normalize( float2 );
			else
				throw new ArgumentException( "Invalid Type" );
		}
	}

	public void RestoreDefault()
	{
		cost = _defaultCost;
		integrationCost = _defaultIntegrationCost;
	}
}
