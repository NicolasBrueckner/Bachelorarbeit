using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{
	public GridController gridController;
	public bool showGrid;
	public Color32 gridColor;

	private Grid _currentGrid;
	private int2 _gridSize;
	private float _cellHalfSize;


	public void SetFlowField(Grid grid)
	{
		_currentGrid = grid;
		_gridSize = grid.gridSize;
		_cellHalfSize = grid.cellHalfSize;
	}

	private void OnDrawGizmos()
	{
		if ( showGrid )
		{
			if ( _currentGrid == null )
				DrawGizmoGrid(gridController.gridSize, gridController.cellHalfSize);
			else
				DrawGizmoGrid(_gridSize, _cellHalfSize);
		}
	}

	private void DrawGizmoGrid(int2 gridSize, float cellHalfSize)
	{
		Gizmos.color = gridColor;
		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				Vector3 center = new float3(cellHalfSize * 2 * x + cellHalfSize, cellHalfSize * 2 * y + cellHalfSize, 0);
				Vector3 size = Vector3.one * cellHalfSize * 2;
				Gizmos.DrawWireCube(center, size);
			}
		}
	}
}
