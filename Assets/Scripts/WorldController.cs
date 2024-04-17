using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldController : MonoBehaviour
{
	public FlowFieldController flowFieldController;
	public GameObject[] sectors;
	public WorldMap worldMap;

	private Sector[,] allSectorData;
	private byte[,] sectorGrid;

	private void Awake()
	{
		sectorGrid = WorldData.sectorsByMap[ worldMap ];
		if ( sectorGrid == null )
			Debug.LogError( "sector grid is null" );
		allSectorData = new Sector[ sectorGrid.GetLength( 0 ), sectorGrid.GetLength( 1 ) ];

		CreateWorld();
		flowFieldController.sectors = allSectorData;
	}

	private void CreateWorld()
	{
		for ( uint x = 0; x < sectorGrid.GetLength( 0 ); x++ )
		{
			for ( uint y = 0; y < sectorGrid.GetLength( 1 ); y++ )
				CreateSector( x, y );
		}
	}

	private void CreateSector( uint x, uint y )
	{
		float3 position = new float3( Sector.sectorSize.x * x, Sector.sectorSize.y * y, 0 );

		GameObject sectorObject = Instantiate( sectors[ sectorGrid[ x, y ] ], position, Quaternion.identity, transform );
		SectorView sectorView = sectorObject.GetComponent<SectorView>();
		Sector newSector = new Sector( position, new uint2( x, y ), sectorView.costMap );

		sectorView.sector = newSector;
		allSectorData[ x, y ] = newSector;
	}
}
