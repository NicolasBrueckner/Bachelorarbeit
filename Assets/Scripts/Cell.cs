using Unity.Mathematics;
using UnityEngine;

public class Cell
{
	public float3 position;
	public int2 index;
	public byte cost;
	public ushort integrationCost;
	public float2 flowDirection;

	public Cell( float3 position, int2 index )
	{
		this.position = position;
		this.index = index;

		cost = 1;
		integrationCost = ushort.MaxValue;
	}

	public void SetDirection( float2 direction )
	{
		math.normalize( direction );
		flowDirection = direction;
	}
}
