using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using mathRandom = Unity.Mathematics.Random;
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

	public float spawnFrequency;
	public List<EnemyPool> serializedEnemies;

	private mathRandom _random;
	private Coroutine _spawnCoroutine;
	private List<GameObject> _enemyPool;
	private Queue<GameObject> _inactiveEnemies;

	private void Awake()
	{
		InitializeSingleton();
		_random = new mathRandom( ( uint )DateTime.Now.Ticks );
	}

	private void Start()
	{
		InitializeObjectPool();
		_spawnCoroutine = StartCoroutine( SpawnEnemy() );
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
				_inactiveEnemies.Dequeue().SetActive( true );

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
			GameObject tempObject = list[ i ];
			list[ i ] = list[ random ];
			list[ random ] = tempObject;
		}
	}
}
