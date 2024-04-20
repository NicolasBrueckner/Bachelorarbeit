using Unity.Mathematics;
using UnityEngine;

public class Cell
{
	public float3 position;
	public int2 index;
	public byte cost;
	public short integrationCost;
	public float2 flowDirection;

	private byte _defaultCost;
	private short _defaultIntegrationCost;

	public Cell( float3 position, int2 index, byte cost )
	{
		this.position = position;
		this.index = index;
		this.cost = cost;

		integrationCost = short.MaxValue;

		_defaultCost = cost;
		_defaultIntegrationCost = integrationCost;
	}

	public void SetDirection( float2 direction )
	{
		math.normalize( direction );
		flowDirection = direction;
	}

	public void RestoreDefault()
	{
		cost = _defaultCost;
		integrationCost = _defaultIntegrationCost;
	}
}
