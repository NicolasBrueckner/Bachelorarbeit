using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;
using stats = SectorStats;

public enum WorldMap
{
	WorldMap_01,
}
public class WorldController : MonoBehaviour
{
	public FlowFieldController flowFieldController;
	public GameObject[] sectors;
	public SerializedDictionary<WorldMap, GameObject> worldMaps;
	public WorldMap activeWorldMap;

	private int2 _worldSize;
	private Sector[,] _allSectorData;

	private void Awake()
	{
		_worldSize = GetWorldDimensions();
		_allSectorData = new Sector[ _worldSize.x, _worldSize.y ];

		CreateWorld();
		flowFieldController.Sectors = _allSectorData;
	}

	private void CreateWorld()
	{
		GameObject activeWorld = Instantiate( worldMaps[ activeWorldMap ] );

		foreach ( Transform sectorObject in activeWorld.transform )
		{
			SectorView sectorView = sectorObject.GetComponent<SectorView>();
			sectorView.InitilizeSectorView();
			_allSectorData[ sectorView.Index.x, sectorView.Index.y ] = sectorView.Sector;
		}
	}

	private int2 GetWorldDimensions()
	{
		var indeces = worldMaps[ activeWorldMap ].transform
			.Cast<Transform>()
			.Select( sector => sector.GetComponent<SectorView>() )
			.Select( sectorView => sectorView.Index );

		int xMax = indeces.Max( index => index.x ) + 1;
		int yMax = indeces.Max( index => index.y ) + 1;

		return new( xMax, yMax );
	}
}
