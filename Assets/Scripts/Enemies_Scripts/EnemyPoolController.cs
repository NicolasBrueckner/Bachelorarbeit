using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyPool
{
	public GameObject enemyObject;
	public BaseStats baseStats;
	public Stats currentStats;
	public int amount;
}

public class EnemyPoolController : MonoBehaviour
{
	public List<EnemyPool> serializedEnemies;
	public float spawnFrequency;
	public Vector2 spawnRange;

	private Transform _targetTransform;
	private UpgradeController _upgradeController;
	private FlowFieldController _flowFieldController;

	private Queue<GameObject> _enemyObjectQueue;

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

		InitializeEnemyObjectPool();
		StartCoroutine( SpawnEnemy() );
	}

	private void GetDependencies()
	{
		_targetTransform = RuntimeManager.Instance.playerCharacterController.gameObject.transform;
		_upgradeController = RuntimeManager.Instance.upgradeController;
		_flowFieldController = RuntimeManager.Instance.flowFieldController;
	}

	private void InitializeEnemyObjectPool()
	{
		List<GameObject> enemyShuffleList = new();

		for ( int i = 0; i < serializedEnemies.Count; i++ )
		{
			serializedEnemies[ i ].currentStats = new( serializedEnemies[ i ].baseStats );
			_upgradeController.AddEnemyStats( serializedEnemies[ i ].currentStats, true );

			for ( int j = 0; j < serializedEnemies[ i ].amount; j++ )
			{
				GameObject enemyCopyObject = Instantiate( serializedEnemies[ i ].enemyObject, transform );
				Enemy enemy = enemyCopyObject.GetComponent<Enemy>();

				InitializeEnemyInstance( enemy, serializedEnemies[ i ].currentStats );

				enemyCopyObject.SetActive( false );
				enemyShuffleList.Add( enemyCopyObject );
			}
		}

		ShuffleList( enemyShuffleList );
		_enemyObjectQueue = new Queue<GameObject>( enemyShuffleList );
	}

	private void InitializeEnemyInstance( Enemy enemy, Stats currentStats )
	{
		enemy.currentStats = currentStats;
		enemy.currentHP = currentStats[ StatType.max_hp ];
		enemy.targetTransform = _targetTransform;
		enemy.flowFieldController = _flowFieldController;
		enemy.enemyPoolController = this;
	}

	private IEnumerator SpawnEnemy()
	{
		while ( gameObject.activeInHierarchy )
		{
			if ( _enemyObjectQueue.Count > 0 )
			{
				GameObject enemy = _enemyObjectQueue.Dequeue();
				Vector2 onCircle = Random.insideUnitCircle.normalized * spawnRange;

				enemy.transform.position = _targetTransform.transform.position + ( Vector3 )onCircle;
				enemy.SetActive( true );
				enemy.GetComponent<Enemy>().StartAttack();
			}

			yield return new WaitForSeconds( spawnFrequency );
		}
	}

	public void EnqueueEnemyObject( GameObject enemyObject, bool wasKilled )
	{
		enemyObject.SetActive( false );
		_enemyObjectQueue.Enqueue( enemyObject );

		if ( wasKilled )
			_upgradeController.AddExperience( 1 );
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
