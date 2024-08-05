using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static GridUtility;
using stats = SectorStats;

public enum WorldMap
{
	WorldMap_01,
}

public class WorldController : MonoBehaviour
{
	public SerializedDictionary<WorldMap, GameObject> worldMaps;
	public WorldMap activeWorldMap;

	public FlowFieldController flowFieldController { get; private set; }

	private int2 _worldSize;
	private Sector[,] _allSectorData;
	private GameObject _worldObjectCopy;
	private Dictionary<int2, Transform> _sectorsByIndex;

	private void Awake()
	{
		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnDependenciesInjected -= OnDependenciesInjected;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();

		_worldObjectCopy = Instantiate( worldMaps[ activeWorldMap ], transform );

		SetSectorIndices();
		SetWorldDimensions();

		CreateWorld();
		flowFieldController.sectors = _allSectorData;
		EventManager.Instance.WorldCreated();
	}

	private void GetDependencies()
	{
		flowFieldController = RuntimeManager.Instance.flowFieldController;
	}

	private void CreateWorld()
	{
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

		foreach ( Transform sectorObject in _worldObjectCopy.transform )
		{
			if ( sectorObject.GetComponent<SectorView>() )
			{
				int2 index = GetUnsafeIndexFromPosition( sectorObject.position, stats.sectorSize );
				_sectorsByIndex[ index ] = sectorObject;
			}
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
