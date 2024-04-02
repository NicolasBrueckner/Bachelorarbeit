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
	private int2 _gridOrigin;
	private int2 _gridSize;
	private float _cellHalfSize;


	public void SetFlowField(Grid grid)
	{
		_currentGrid = grid;
		_gridOrigin = grid.gridOrigin;
		_gridSize = grid.gridSize;
		_cellHalfSize = grid.cellHalfSize;
	}

	private void OnDrawGizmos()
	{
		if ( showGrid )
		{
			if ( _currentGrid == null )
				DrawGizmoGrid(gridController.gridOrigin, gridController.gridSize, gridController.cellHalfSize);
			else
				DrawGizmoGrid(_gridOrigin, _gridSize, _cellHalfSize);
		}
	}

	private void DrawGizmoGrid(int2 gridOrigin, int2 gridSize, float cellHalfSize)
	{
		Gizmos.color = gridColor;
		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				Vector3 center = new Vector3(gridOrigin.x + cellHalfSize * 2 * x + cellHalfSize, gridOrigin.y + cellHalfSize * 2 * y + cellHalfSize, 0);
				Vector3 size = Vector3.one * cellHalfSize * 2;
				Gizmos.DrawWireCube(center, size);
			}
		}
	}
}
