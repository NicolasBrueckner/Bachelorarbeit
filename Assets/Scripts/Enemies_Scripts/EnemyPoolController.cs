using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct EnemyPool
{
	public GameObject enemyObject;
	public BaseStats baseStats;
	public int amount;
}

public class EnemyPoolController : MonoBehaviour
{
	public float spawnFrequency;
	public Vector2 spawnRange;
	public GameObject targetObject;
	public List<EnemyPool> serializedEnemies;
	public FlowFieldController flowFieldController;

	private Queue<GameObject> _enemyObjectQueue;

	private void Start()
	{
		InitializeEnemyObjectPool();
		StartCoroutine( SpawnEnemy() );
	}

	private void InitializeEnemyObjectPool()
	{
		List<GameObject> enemyShuffleList = new();

		for ( int i = 0; i < serializedEnemies.Count; i++ )
		{
			for ( int j = 0; j < serializedEnemies[ i ].amount; j++ )
			{
				GameObject enemyCopyObject = Instantiate( serializedEnemies[ i ].enemyObject );
				Enemy enemy = enemyCopyObject.GetComponent<Enemy>();

				InitializeEnemyInstance( enemy, serializedEnemies[ i ].baseStats );

				enemyCopyObject.SetActive( false );
				enemyShuffleList.Add( enemyCopyObject );
			}
		}

		ShuffleList( enemyShuffleList );
		_enemyObjectQueue = new Queue<GameObject>( enemyShuffleList );
	}

	private void InitializeEnemyInstance( Enemy enemy, BaseStats baseStats )
	{
		enemy.flowFieldController ??= flowFieldController;
		enemy.enemyPoolController = this;
		enemy.currentStats = new( baseStats );
	}

	private IEnumerator SpawnEnemy()
	{
		while ( gameObject.activeInHierarchy )
		{
			if ( _enemyObjectQueue.Count > 0 )
			{
				GameObject enemy = _enemyObjectQueue.Dequeue();
				Vector2 onCircle = Random.insideUnitCircle.normalized * spawnRange;

				enemy.transform.position = targetObject.transform.position + new Vector3( onCircle.x, onCircle.y, 0f );
				enemy.SetActive( true );
				enemy.GetComponent<Enemy>().StartAttack();
			}

			yield return new WaitForSeconds( spawnFrequency );
		}
	}

	public void EnqueueEnemyObject( GameObject enemyObject )
	{
		enemyObject.SetActive( false );
		_enemyObjectQueue.Enqueue( enemyObject );
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
