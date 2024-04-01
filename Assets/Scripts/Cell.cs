using Unity.Mathematics;

public class Cell
{
	public float2 position;
	public int2 index;

	public Cell(float2 position, int2 index)
	{
		this.position = position;
		this.index = index;
	}
}
