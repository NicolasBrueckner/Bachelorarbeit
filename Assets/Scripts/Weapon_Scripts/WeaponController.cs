using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponController : MonoBehaviour
{
	public PlayerCharacterController characterController;

	[Header( "Weapon Stats" )]
	public GameObject weaponObject;
	public float atk_spd;

	public Vector2 Direction
	{
		get => characterController.AimDirection;
	}

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

	protected virtual void Attack()
	{
		_currentCooldown = atk_spd;
	}
}
