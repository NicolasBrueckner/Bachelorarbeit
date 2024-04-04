using Unity.Mathematics;

public class Cell
{
	public float3 position;
	public int2 index;
	public byte cost;
	public ushort integrationCost;
	public CellDirection flowDirection;

	public Cell( float3 position, int2 index )
	{
		this.position = position;
		this.index = index;
		cost = 1;
		integrationCost = ushort.MaxValue;
	}

	public void IncreaseCost( int amount )
	{
		if ( cost == byte.MaxValue )
			return;
		if ( cost + amount > byte.MaxValue )
			cost = byte.MaxValue;
		else
			cost += ( byte )amount;
	}
}
