

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Grid
{
	public Cell[,] grid { get; private set; }
	public int2 gridOrigin { get; private set; }
	public int2 gridSize { get; private set; }
	public float cellHalfSize { get; private set; }
	public Cell destinationCell;

	private float _cellSize;
	private int _terrain;
	private float3 _cellHalfExtends;

	public Grid( float cellHalfSize, int2 gridOrigin, int2 gridSize )
	{
		this.gridOrigin = gridOrigin;
		this.gridSize = gridSize;
		this.cellHalfSize = cellHalfSize;

		_cellSize = cellHalfSize * 2;
		_terrain = LayerMask.GetMask( "Impassable" );
		_cellHalfExtends = Vector3.one * cellHalfSize;
	}

	public void CreateGrid()
	{
		grid = new Cell[ gridSize.x, gridSize.y ];

		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				float3 position = new float3( gridOrigin.x + ( _cellSize * x ) + cellHalfSize, gridOrigin.y + ( _cellSize * y ) + cellHalfSize, 0 );
				grid[ x, y ] = new Cell( position, new int2( x, y ) );
			}
		}
	}

	public void CreateCostField()
	{
		foreach ( Cell cell in grid )
		{
			Collider[] obstacles = Physics.OverlapBox( cell.position, _cellHalfExtends, Quaternion.identity, _terrain );
			bool hasIncreasedCost = false;
			foreach ( Collider collider in obstacles )
			{
				if ( collider.gameObject.layer == 8 )
					cell.IncreaseCost( 255 );
			}
		}
	}

	public void CreateIntegrationField( Cell destination )
	{
		destinationCell = destination;
		destinationCell.cost = 0;
		destinationCell.integrationCost = 0;

		Queue<Cell> cells = new Queue<Cell>();
		cells.Enqueue( destinationCell );

		while ( cells.Count > 0 )
		{
			Cell currentCell = cells.Dequeue();
			List<Cell> neighbors = GetNeighborCells( currentCell.index, CellDirection.cardinals );

			foreach ( Cell currentNeighbor in neighbors )
			{
				if ( currentNeighbor.cost == byte.MaxValue )
					continue;
				if ( currentNeighbor.cost + currentCell.integrationCost < currentNeighbor.integrationCost )
				{
					currentNeighbor.integrationCost = ( ushort )( currentNeighbor.cost + currentCell.integrationCost );
					cells.Enqueue( currentNeighbor );
				}
			}
		}
	}

	public void CreateFlowField()
	{
		foreach ( Cell cell in grid )
		{
			List<Cell> neighbors = GetNeighborCells( cell.index, CellDirection.allDirections );
			int integrationCost = cell.integrationCost;

			foreach ( Cell currentNeighbor in neighbors )
			{
				if ( currentNeighbor.integrationCost < integrationCost )
				{
					integrationCost = currentNeighbor.integrationCost;
					cell.flowDirection = CellDirection.GetDirection( currentNeighbor.index - cell.index );
				}
			}
		}
	}

	private List<Cell> GetNeighborCells( int2 index, List<CellDirection> directions )
	{
		List<Cell> cells = new List<Cell>();

		foreach ( int2 currentDirection in directions )
		{
			Cell currentCell = GetCellInDirection( index, currentDirection );
			if ( currentCell != null )
				cells.Add( currentCell );
		}
		return cells;
	}

	private Cell GetCellInDirection( int2 index, int2 direction )
	{
		int2 resultIndex = index + direction;

		if ( resultIndex.x < 0 || resultIndex.x >= gridSize.x || resultIndex.y < 0 || resultIndex.y >= gridSize.y )
			return null;
		else
			return grid[ resultIndex.x, resultIndex.y ];
	}

	public Cell GetCellFromPosition( float3 position )
	{
		float2 adjustedPosition = new float2( position.x - gridOrigin.x, position.y - gridOrigin.y );
		float2 normalizedPosition = new float2(
			math.clamp( adjustedPosition.x / ( gridSize.x * _cellSize ), 0f, 1f ),
			math.clamp( adjustedPosition.y / ( gridSize.y * _cellSize ), 0f, 1f ) );

		int2 gridPosition = new int2(
			math.clamp( ( int )math.floor( normalizedPosition.x * gridSize.x ), 0, gridSize.x - 1 ),
			math.clamp( ( int )math.floor( normalizedPosition.y * gridSize.y ), 0, gridSize.y - 1 ) );

		return grid[ gridPosition.x, gridPosition.y ];
	}
}
