

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
	public Sector[,] sectors { get; private set; }
	public Cell[,] grid { get; private set; }
	public float3 gridOrigin { get; private set; }
	public int2 gridSize { get; private set; }

	private Cell _destinationCell;
	private float _cellSize;

	public FlowField( Sector[,] sectors )
	{
		this.sectors = sectors;

		gridOrigin = new float3( 0, 0, 0 );
		gridSize = Sector.gridSize;
		_cellSize = Sector.cellRadius * 2;
	}

	public void CreateGrid()
	{
		grid = new Cell[ gridSize.x, gridSize.y ];

		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				float3 position = new float3( gridOrigin.x + ( _cellSize * x ) + Sector.cellRadius, gridOrigin.y + ( _cellSize * y ) + Sector.cellRadius, 0 );
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
			List<int2> neighbors = GridUtility.GetUnsafeNeighborIndexes( currentCell.index, Direction.cardinals );

			foreach ( int2 neighbor in neighbors )
			{
				int x = neighbor.x;
				int y = neighbor.y;

				if ( !GridUtility.ValidateIndex( neighbor, grid ) )
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
			List<int2> neighbors = GridUtility.GetUnsafeNeighborIndexes( cell.index, Direction.trueDirections );
			int integrationCost = cell.integrationCost;

			for ( int i = 0; i < neighbors.Count; i++ )
			{
				if ( GridUtility.ValidateIndex( neighbors[ i ], grid ) )
				{
					Cell validStraight = grid[ neighbors[ i ].x, neighbors[ i ].y ];

					if ( validStraight.integrationCost < integrationCost )
					{
						integrationCost = validStraight.integrationCost;
						cell.flowDirection = Direction.GetDirection( validStraight.index - cell.index );
					}

					if ( GridUtility.ValidateIndex( neighbors[ i + 1 ], grid ) && GridUtility.ValidateIndex( neighbors[ ( i + 2 ) % neighbors.Count ], grid ) )
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



}
