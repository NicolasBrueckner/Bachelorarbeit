using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
	[Header( "Weapon Stats" )]
	public GameObject weaponObject;
	public float atk;
	public float atk_spd;
	public float spd;
	public int pierce;

	[Header( "Weapon Base Stats" )]
	public WeaponBaseStats baseStats;

	protected float2 _direction_;

	private PlayerCharacterController _characterController;
	private float _currentCooldown;


	protected virtual void Start()
	{
		_currentCooldown = atk_spd;
		_characterController = transform.parent.GetComponent<PlayerCharacterController>();
	}

	protected virtual void Update()
	{
		_currentCooldown -= Time.deltaTime;
		if ( _currentCooldown <= 0f )
			Attack();
	}

	protected virtual void Attack()
	{
		_currentCooldown = atk_spd;
	}

	protected void GetDirection()
	{
		_direction_ = _characterController.AimDirection;
	}
}
