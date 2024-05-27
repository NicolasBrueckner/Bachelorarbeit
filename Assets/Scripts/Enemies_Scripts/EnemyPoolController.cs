using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct EnemyPool
{
	public GameObject prefab;
	public int amount;
}

public class EnemyPoolController : MonoBehaviour
{
	public static EnemyPoolController Instance { get; private set; }

	public GameObject player;
	public float spawnRange;
	public float spawnFrequency;
	public List<EnemyPool> serializedEnemies;

	private List<GameObject> _enemyPool;
	private Queue<GameObject> _inactiveEnemies;

	private void Awake()
	{
		InitializeSingleton();
	}

	private void Start()
	{
		InitializeObjectPool();
		StartCoroutine( SpawnEnemy() );
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

	private void InitializeObjectPool()
	{
		_enemyPool = new();
		GameObject temp;

		for ( int i = 0; i < serializedEnemies.Count; i++ )
		{
			for ( int j = 0; j < serializedEnemies[ i ].amount; j++ )
			{
				temp = Instantiate( serializedEnemies[ i ].prefab );
				temp.SetActive( false );
				_enemyPool.Add( temp );
			}
		}

		ShuffleList( _enemyPool );
		_inactiveEnemies = new Queue<GameObject>( _enemyPool );
	}

	private IEnumerator SpawnEnemy()
	{
		while ( true )
		{
			if ( _inactiveEnemies.Count > 0 )
			{
				GameObject enemy = _inactiveEnemies.Dequeue();
				float2 onCircle = Random.insideUnitCircle.normalized * spawnRange;
				enemy.transform.position = player.transform.position + new Vector3( onCircle.x, onCircle.y, 0f );
				enemy.SetActive( true );
			}

			yield return new WaitForSeconds( spawnFrequency );
		}
	}

	public void OnEnemyDefeated( GameObject enemy )
	{
		enemy.SetActive( false );
		_inactiveEnemies.Enqueue( enemy );
	}

	private void ShuffleList( List<GameObject> list )
	{
		int count = list.Count;
		int last = count - 1;
		for ( int i = 0; i <= last; i++ )
		{
			int random = Random.Range( 0, count );
			(list[ random ], list[ i ]) = (list[ i ], list[ random ]);
		}
	}
}
