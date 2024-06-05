using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.UIElements;
using static GridUtility;
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
	private Dictionary<int2, Transform> _sectorsByIndex;

	private void Awake()
	{
		SetSectorIndices();
		SetWorldDimensions();

		CreateWorld();
		flowFieldController.Sectors = _allSectorData;
	}

	private void CreateWorld()
	{
		Instantiate( worldMaps[ activeWorldMap ] );
		_allSectorData = new Sector[ _worldSize.x, _worldSize.y ];

		foreach ( int2 index in _sectorsByIndex.Keys )
		{
			SectorView sectorView = _sectorsByIndex[ index ].GetComponent<SectorView>();

			sectorView.InitilizeSectorView( index );

			_allSectorData[ index.x, index.y ] = sectorView.Sector;
		}
	}

	private void SetSectorIndices()
	{
		_sectorsByIndex = new Dictionary<int2, Transform>();

		foreach ( Transform sectorObject in worldMaps[ activeWorldMap ].transform )
		{
			int2 index = GetUnsafeIndexFromPosition( sectorObject.position, stats.sectorSize );
			_sectorsByIndex[ index ] = sectorObject;
		}
	}

	private void SetWorldDimensions()
	{
		if ( _sectorsByIndex.Count == 0 )
			return;

		int xMax = _sectorsByIndex.Keys.Max( index => index.x );
		int yMax = _sectorsByIndex.Keys.Max( index => index.y );

		_worldSize = new( xMax + 1, yMax + 1 );
	}
}
