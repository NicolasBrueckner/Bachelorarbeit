using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class FlowField
{
	public Sector[,] Sectors { get; private set; }
	public Cell[,] Cells { get; private set; }

	private float3 _gridOrigin;
	private int2 _gridSize;
	private Cell _destinationCell;
	private float _cellRadius;
	private float _cellDiameter;


	public FlowField( Sector[,] sectors )
	{
		Sectors = sectors;

		_gridOrigin = Sectors[ 0, 0 ].position;
		_gridSize = Sector.gridSize * Sectors.Length;
		_cellRadius = Sector.cellRadius;
		_cellDiameter = Sector.cellDiameter;
	}

	public void CreateGrid()
	{
		Cells = new Cell[ _gridSize.x, _gridSize.y ];

		for ( int x = 0; x < _gridSize.x; x++ )
		{
			for ( int y = 0; y < _gridSize.y; y++ )
			{
				float3 position = new float3(
					_gridOrigin.x + ( _cellDiameter * x ) + _cellRadius,
					_gridOrigin.y + ( _cellDiameter * y ) + _cellRadius, 0 );
				Cells[ x, y ] = new Cell( position, new int2( x, y ) );
			}
		}
	}

	public void CreateCostField( byte[,] costs )
	{
		for ( int x = 0; x < _gridSize.x; x++ )
		{
			for ( int y = 0; y < _gridSize.y; y++ )
				Cells[ x, y ].cost = costs[ x, y ];
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

				if ( !GridUtility.ValidateIndex( neighbor, Cells ) )
					continue;
				if ( Cells[ x, y ].cost + currentCell.integrationCost < Cells[ x, y ].integrationCost )
				{
					Cells[ x, y ].integrationCost = ( ushort )( Cells[ x, y ].cost + currentCell.integrationCost );
					cells.Enqueue( Cells[ x, y ] );
				}
			}
		}
	}

	public void CreateFlowField()
	{
		foreach ( Cell cell in Cells )
		{
			List<int2> neighbors = GridUtility.GetUnsafeNeighborIndexes( cell.index, Direction.trueDirections );
			int integrationCost = cell.integrationCost;

			for ( int i = 0; i < neighbors.Count; i++ )
			{
				if ( GridUtility.ValidateIndex( neighbors[ i ], Cells ) )
				{
					Cell validStraight = Cells[ neighbors[ i ].x, neighbors[ i ].y ];

					if ( validStraight.integrationCost < integrationCost )
					{
						integrationCost = validStraight.integrationCost;
						cell.flowDirection = Direction.GetDirection( validStraight.index - cell.index );
					}

					if ( GridUtility.ValidateIndex( neighbors[ i + 1 ], Cells ) && GridUtility.ValidateIndex( neighbors[ ( i + 2 ) % neighbors.Count ], Cells ) )
					{
						Cell validDiagonal = Cells[ neighbors[ i + 1 ].x, neighbors[ i + 1 ].y ];

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
