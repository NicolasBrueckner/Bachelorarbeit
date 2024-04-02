

using Unity.Mathematics;
using UnityEngine.UIElements;

public class Grid
{
	public Cell[,] grid { get; private set; }
	public int2 gridOrigin { get; private set; }
	public int2 gridSize { get; private set; }
	public float cellHalfSize { get; private set; }
	public float cellSize { get; private set; }


	public Grid(float cellHalfSize, int2 gridOrigin, int2 gridSize)
	{
		this.gridOrigin = gridOrigin;
		this.gridSize = gridSize;
		this.cellHalfSize = cellHalfSize;
		cellSize = cellHalfSize * 2;
	}

	public void CreateGrid()
	{
		grid = new Cell[gridSize.x, gridSize.y];

		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				float2 position = new float2(gridOrigin.x + cellSize * x + cellHalfSize, gridOrigin.y + cellSize * y + cellHalfSize);
				grid[x, y] = new Cell(position, new int2(x, y));
			}
		}
	}
}
