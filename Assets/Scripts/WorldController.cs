using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class WorldController : MonoBehaviour
{
	public Sector[] sectors;
	public SectorMap mapType;

	internal Sector[] activeSectors;

	private byte[,] sectorGrid;


	private void Awake()
	{
		sectorGrid = SectorData.Instance.sectors[ mapType ];
		CreateWorld();
	}

	private void CreateWorld()
	{
		int x = 0;
		int y = 0;

		foreach ( byte sectorIndex in sectorGrid )
		{
			float3 sectorPosition = new float3( Sector.sectorSize.x * x, Sector.sectorSize.y * y, 0 );
			Instantiate( sectors[ sectorIndex ], sectorPosition, Quaternion.identity, transform );

			y++;
			if ( y >= sectorGrid.GetLength( 1 ) )
			{
				y = 0;
				x++;
			}
		}
	}
}
