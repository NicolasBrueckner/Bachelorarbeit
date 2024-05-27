using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static GridUtility;

public class FlowFieldController : MonoBehaviour
{
	public static FlowFieldController Instance { get; private set; }

	public Camera mainCamera;
	public DebugGizmos debugGizmos;
	public GameObject player;

	public int2 WorldGridSize { get; private set; }
	public Sector[,] Sectors { get; set; }
	public FlowField FlowField { get; private set; }

	private int2 _mainSectorIndex;

	private void Awake()
	{
		InitializeSingleton();
	}

	private void Start()
	{
		WorldGridSize = new int2( Sectors.GetLength( 0 ), Sectors.GetLength( 1 ) );
		StartCoroutine( FlowFieldCoroutine() );
	}

	private void InitializeSingleton()
	{
		if ( Instance == null )
		{
			Instance = this;
			DontDestroyOnLoad( gameObject );
		}
		else
			Destroy( gameObject );
	}

	IEnumerator FlowFieldCoroutine()
	{
		while ( true )
		{
			BuildFlowField( player.transform.position );

			yield return new WaitForSeconds( 0.2f );
		}
	}

	public void BuildFlowField( float3 position )
	{
		int2 sectorIndex = GetIndexFromPosition( position, transform.position, WorldGridSize, Sector.sectorSize );

		if ( math.any( sectorIndex != _mainSectorIndex ) )
		{
			FlowField = new FlowField( GetActiveSectors( sectorIndex ) );

			FlowField.InitializeFlowField();
			debugGizmos.SetFlowField( FlowField );
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
				activeSectors[ currentIndex_temp.x, currentIndex_temp.y ] = Sectors[ currentIndex.x, currentIndex.y ];
		}

		return activeSectors;
	}

	private (Sector[,], int2 mainIndex) CreateActiveSectorArray( int2 mainIndex )
	{
		int2 currentDimensions = new( 3, 3 );
		int2 adjustedMainIndex = new( 1, 1 );

		if ( !ValidateIndex( mainIndex + Direction.N, WorldGridSize ) )
		{
			currentDimensions.y--;
		}
		if ( !ValidateIndex( mainIndex + Direction.E, WorldGridSize ) )
		{
			currentDimensions.x--;
		}
		if ( !ValidateIndex( mainIndex + Direction.S, WorldGridSize ) )
		{
			currentDimensions.y--;
			adjustedMainIndex.y--;
		}
		if ( !ValidateIndex( mainIndex + Direction.W, WorldGridSize ) )
		{
			currentDimensions.x--;
			adjustedMainIndex.x--;
		}

		return (new Sector[ currentDimensions.x, currentDimensions.y ], adjustedMainIndex);
	}
}
