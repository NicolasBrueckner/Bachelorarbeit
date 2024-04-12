

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FlowField
{
	public Sector[] sectors { get; private set; }
	public Cell[,] grid { get; private set; }
	public float3 gridOrigin { get; private set; }
	public int2 gridSize { get; private set; }
	public float cellHalfSize { get; private set; }

	private Cell _destinationCell;
	private float _cellSize;

	public FlowField( Sector[] sectors )
	{
		this.sectors = sectors;

		gridOrigin = sectors[ 0 ].transform.position;
		gridSize = Sector.gridSize;
		cellHalfSize = Sector.cellRadius;
		_cellSize = cellHalfSize * 2;
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

	public void CreateCostField( byte[,] costs )
	{
		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
				grid[ x, y ].cost = costs[ x, y ];
		}
	}

	public void CreateIntegrationField( Cell destinationCell )
	{
		_destinationCell = destinationCell;
		_destinationCell.cost = 0;
		_destinationCell.integrationCost = 0;

		Queue<Cell> cells = new Queue<Cell>();
		cells.Enqueue( _destinationCell );

		while ( cells.Count > 0 )
		{
			Cell currentCell = cells.Dequeue();
			List<int2> neighbors = GetNeighborIndexes( currentCell.index, Direction.cardinals );

			foreach ( int2 neighbor in neighbors )
			{
				int x = neighbor.x;
				int y = neighbor.y;

				if ( !ValidateIndex( neighbor ) )
					continue;
				if ( grid[ x, y ].cost + currentCell.integrationCost < grid[ x, y ].integrationCost )
				{
					grid[ x, y ].integrationCost = ( ushort )( grid[ x, y ].cost + currentCell.integrationCost );
					cells.Enqueue( grid[ x, y ] );
				}
			}
		}
	}

	public void CreateFlowField()
	{
		foreach ( Cell cell in grid )
		{
			List<int2> neighbors = GetNeighborIndexes( cell.index, Direction.trueDirections );
			int integrationCost = cell.integrationCost;

			for ( int i = 0; i < neighbors.Count; i++ )
			{
				if ( ValidateIndex( neighbors[ i ] ) )
				{
					Cell validStraight = grid[ neighbors[ i ].x, neighbors[ i ].y ];

					if ( validStraight.integrationCost < integrationCost )
					{
						integrationCost = validStraight.integrationCost;
						cell.flowDirection = Direction.GetDirection( validStraight.index - cell.index );
					}

					if ( ValidateIndex( neighbors[ i + 1 ] ) && ValidateIndex( neighbors[ ( i + 2 ) % neighbors.Count ] ) )
					{
						Cell validDiagonal = grid[ neighbors[ i + 1 ].x, neighbors[ i + 1 ].y ];

						if ( validDiagonal.integrationCost < integrationCost )
						{
							integrationCost = validDiagonal.integrationCost;
							cell.flowDirection = Direction.GetDirection( validDiagonal.index - cell.index );
						}
					}
				}
				i++;
			}
		}
	}

	//private void SetFlowDirection(Cell cell, Cell neighborCell)

	private List<int2> GetNeighborIndexes( int2 index, List<Direction> directions )
	{
		List<int2> cells = new List<int2>();

		foreach ( Direction currentDirection in directions )
			cells.Add( index + currentDirection );

		return cells;
	}

	private bool ValidateIndex( int2 index )
	{
		if ( index.x >= 0 && index.x < gridSize.x && index.y >= 0 & index.y < gridSize.y && grid[ index.x, index.y ].cost < byte.MaxValue )
			return true;
		return false;
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
