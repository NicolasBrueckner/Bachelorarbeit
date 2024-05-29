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
		allSectorData = new Sector[ sectorGrid.GetLength( 0 ), sectorGrid.GetLength( 1 ) ];

		CreateWorld();
		flowFieldController.Sectors = allSectorData;
	}

	private void CreateWorld()
	{
		for ( int x = 0; x < sectorGrid.GetLength( 0 ); x++ )
		{
			for ( int y = 0; y < sectorGrid.GetLength( 1 ); y++ )
				CreateSector( x, y );
		}
	}

	private void CreateSector( int x, int y )
	{
		float3 position = new( Sector.sectorSize.x * x, Sector.sectorSize.y * y, 0 );

		GameObject sectorObject = Instantiate( sectors[ sectorGrid[ x, y ] ], position, Quaternion.identity, transform );
		SectorView sectorView = sectorObject.GetComponent<SectorView>();
		sectorView.InitilizeValues( new( x, y ) );

		sectorView.InitializeSector();
		allSectorData[ x, y ] = sectorView.Sector;
	}
}
