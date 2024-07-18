using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using static GridUtility;
using stats = SectorStats;

public class FlowFieldController : MonoBehaviour
{
	public Sector[,] sectors;

	public FlowField FlowField { get; private set; }

	private int2 _worldGridSize;
	private int2 _mainSectorIndex = new( -1, -1 );
	private Transform _playerTransform;
	private DebugController _debugController;

	private void Awake()
	{
		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
		EventManager.Instance.OnWorldCreated += OnWorldCreated;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnDependenciesInjected -= OnDependenciesInjected;
		EventManager.Instance.OnWorldCreated -= OnWorldCreated;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();
	}

	private void GetDependencies()
	{
		_debugController = RuntimeManager.Instance.debugController;
		_playerTransform = RuntimeManager.Instance.playerCharacterController.gameObject.transform;
	}

	private void OnWorldCreated()
	{
		_worldGridSize = new int2( sectors.GetLength( 0 ), sectors.GetLength( 1 ) );
		StartCoroutine( FlowFieldCoroutine() );

	}

	IEnumerator FlowFieldCoroutine()
	{
		while ( true )
		{
			BuildFlowField( _playerTransform.position );

			yield return new WaitForSeconds( 0.2f );
		}
	}

	public void BuildFlowField( float3 position )
	{
		int2 sectorIndex = GetIndexFromPosition( position, transform.position, _worldGridSize, stats.sectorSize );

		if ( math.any( sectorIndex != _mainSectorIndex ) )
		{
			FlowField = new FlowField( GetActiveSectors( sectorIndex ) );

			FlowField.InitializeFlowField();
			_debugController.SetFlowField( FlowField );
			_mainSectorIndex = sectorIndex;
		}

		FlowField.SetDestinationCell( position );
		FlowField.CreateIntegrationField();
		FlowField.CreateFlowField();
	}

	private Sector[,] GetActiveSectors( int2 mainIndex )
	{
		(Sector[,] activeSectors, int2 mainIndex_temp) = CreateActiveSectorArray( mainIndex );
		int2 activeSectorsDimensions = new( activeSectors.GetLength( 0 ), activeSectors.GetLength( 1 ) );

		foreach ( Direction direction in Direction.allDirections )
		{
			int2 currentIndex = mainIndex + direction;
			int2 currentIndex_temp = mainIndex_temp + direction;

			if ( ValidateIndex( currentIndex_temp, activeSectorsDimensions ) )
				activeSectors[ currentIndex_temp.x, currentIndex_temp.y ] = sectors[ currentIndex.x, currentIndex.y ];
		}

		return activeSectors;
	}

	private (Sector[,], int2 mainIndex) CreateActiveSectorArray( int2 mainIndex )
	{
		int2 currentDimensions = new( 3, 3 );
		int2 adjustedMainIndex = new( 1, 1 );

		if ( !ValidateIndex( mainIndex + Direction.N, _worldGridSize ) )
		{
			currentDimensions.y--;
		}
		if ( !ValidateIndex( mainIndex + Direction.E, _worldGridSize ) )
		{
			currentDimensions.x--;
		}
		if ( !ValidateIndex( mainIndex + Direction.S, _worldGridSize ) )
		{
			currentDimensions.y--;
			adjustedMainIndex.y--;
		}
		if ( !ValidateIndex( mainIndex + Direction.W, _worldGridSize ) )
		{
			currentDimensions.x--;
			adjustedMainIndex.x--;
		}

		return (new Sector[ currentDimensions.x, currentDimensions.y ], adjustedMainIndex);
	}
}
