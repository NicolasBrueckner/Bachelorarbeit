using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using static GridUtility;
using stats = SectorStats;

public class FlowField
{
	public Sector[,] Sectors { get; private set; }
	public Cell[,] Cells { get; private set; }
	public float3 GridOrigin { get; private set; }
	public int2 GridSize { get; private set; }

	private Cell _destinationCell;
	private readonly float _cellRadius;
	private readonly float _cellDiameter;


	public FlowField( Sector[,] sectors )
	{
		Sectors = sectors;

		GridOrigin = Sectors[ 0, 0 ].Position;
		GridSize = stats.gridSize * new int2( Sectors.GetLength( 0 ), Sectors.GetLength( 1 ) );
		_cellRadius = stats.cellRadius;
		_cellDiameter = stats.cellDiameter;
	}

	public void InitializeFlowField()
	{
		Cells = new Cell[ GridSize.x, GridSize.y ];
		int2 sectorGridSize = stats.gridSize;

		for ( int x = 0; x < GridSize.x; x++ )
		{
			for ( int y = 0; y < GridSize.y; y++ )
			{
				float3 position = new(
					GridOrigin.x + ( _cellDiameter * x ) + _cellRadius,
					GridOrigin.y + ( _cellDiameter * y ) + _cellRadius, 0 );
				int2 sectorIndex = new( x / sectorGridSize.x, y / sectorGridSize.y );
				int2 localIndex = new( x % sectorGridSize.x, y % sectorGridSize.y );
				byte cost = Sectors[ sectorIndex.x, sectorIndex.y ].Costs[ localIndex.x, localIndex.y ];

				Cells[ x, y ] = new Cell( position, new int2( x, y ), cost );
			}
		}
	}

	public void SetDestinationCell( float3 position )
	{
		RestoreCellsToDefault();

		int2 destinationIndex = GetIndexFromPosition( position, GridOrigin, GridSize, stats.cellDiameter );
		_destinationCell = Cells[ destinationIndex.x, destinationIndex.y ];

		_destinationCell.cost = 0;
		_destinationCell.integrationCost = 0;
	}

	public void CreateIntegrationField()
	{
		Queue<Cell> cells = new();
		cells.Enqueue( _destinationCell );

		while ( cells.Count > 0 )
		{
			Cell currentCell = cells.Dequeue();
			List<int2> neighbors = GetUnsafeIndexes( currentCell.index, Direction.cardinals );

			foreach ( int2 neighbor in neighbors )
			{
				int x = neighbor.x;
				int y = neighbor.y;

				if ( ValidateIndex( neighbor, GridSize ) && Cells[ x, y ].cost < byte.MaxValue )
				{
					short combinedIntegrationCost = ( short )( Cells[ x, y ].cost + currentCell.integrationCost );
					if ( combinedIntegrationCost < Cells[ x, y ].integrationCost )
					{
						Cells[ x, y ].integrationCost = combinedIntegrationCost;
						cells.Enqueue( Cells[ x, y ] );
					}
				}
			}
		}
	}

	public void CreateFlowField()
	{
		foreach ( Cell cell in Cells )
		{
			if ( cell.cost == byte.MaxValue )
			{
				cell.Direction = new float2( _destinationCell.position.x - cell.position.x, _destinationCell.position.y - cell.position.y );
				continue;
			}

			List<int2> neighbors = GetUnsafeIndexes( cell.index, Direction.trueDirections );
			short integrationCost = cell.integrationCost;
			Direction flowDirection = Direction.None;

			for ( int i = 0; i < neighbors.Count; i++ )
			{
				int2 n1 = neighbors[ i ];

				if ( ValidateCell( n1, Cells, out Cell n1Cell ) )
				{
					int2 n2 = neighbors[ i + 1 ];
					int2 n3 = neighbors[ ( i + 2 ) % neighbors.Count ];

					UpdateIntegrationCost( cell, n1Cell, ref integrationCost, ref flowDirection );

					if ( ValidateCell( n2, Cells, out Cell n2Cell ) && ValidateCell( n3, Cells, out _ ) )
						UpdateIntegrationCost( cell, n2Cell, ref integrationCost, ref flowDirection );
				}
				i++;
			}
			cell.Direction = flowDirection;
		}
	}

	private void UpdateIntegrationCost( Cell cell, Cell neighbor, ref short integrationCost, ref Direction flowDirection )
	{
		if ( neighbor.integrationCost < integrationCost )
		{
			integrationCost = neighbor.integrationCost;
			flowDirection = Direction.GetDirection( neighbor.index - cell.index );
		}
	}

	private void RestoreCellsToDefault()
	{
		if ( Cells != null )
		{
			foreach ( Cell cell in Cells )
				cell.RestoreDefault();
		}
	}
}
