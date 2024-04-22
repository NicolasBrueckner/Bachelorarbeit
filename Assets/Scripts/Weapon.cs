using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject weaponObject;
	public float atk;
	public float atk_spd;
	public float spd;
	public int pierce;

	private float _currentCooldown;

	protected virtual void Start()
	{
		_currentCooldown = atk_spd;
	}

	protected virtual void Update()
	{
		_currentCooldown -= Time.deltaTime;
		if ( _currentCooldown <= 0f )
			Attack();
	}

	private void Attack()
	{
		_currentCooldown = atk_spd;
	}
}
