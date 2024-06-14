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
	public GameObject weaponObject;
	public WeaponBaseStats baseStats;
	public WeaponStats currentStats;

	public float Frequency => currentStats.atk_spd * characterController.currenStats.atk_spd;
	public Vector2 Direction => characterController.AimDirection;

	protected bool _isActive_ = false;

	private float _currentCooldown;

	protected virtual void Awake()
	{
		_currentCooldown = 0f;
		currentStats = new( baseStats );
		InitializeWeapon();
		ToggleWeapon();
	}

	protected virtual void Update()
	{
		_currentCooldown += Time.deltaTime;
		if ( _isActive_ && _currentCooldown >= Frequency )
			Attack();
	}

	protected virtual void Attack()
	{
		_currentCooldown = 0f;
	}


	protected virtual void InitializeWeapon()
	{

	}

	protected void InitializeWeaponObject( GameObject weaponObject, Weapon weapon )
	{
		weapon.controller = this;
		weapon.currentStats = currentStats;
		weapon.ScaleToSize();
		weaponObject.SetActive( false );
	}

	public void ToggleWeapon()
	{
		InternalToggleWeapon();
	}

	protected virtual void InternalToggleWeapon()
	{
		_isActive_ = !_isActive_;
	}
}
