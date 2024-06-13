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

	public float Frequency => weapon.currentStats.atk_spd * characterController.currenStats.atk_spd;
	public Vector2 Direction => characterController.AimDirection;


	protected GameObject _weaponObjectCopy_;
	protected Weapon weapon;

	private float _currentCooldown;

	protected virtual void Start()
	{
		_currentCooldown = 0f;
		InitializeWeapon();
		weapon = weaponObject.GetComponent<Weapon>();
	}

	protected virtual void Update()
	{
		_currentCooldown += Time.deltaTime;
		if ( _currentCooldown >= Frequency )
			Attack();
	}

	protected virtual void Attack()
	{
		Debug.Log( $"weapon atk spd: {weapon.currentStats.atk_spd}" );
		Debug.Log( $"player atk spd: {characterController.currenStats.atk_spd}" );
		Debug.Log( $"Frequency: {Frequency}" );
		_currentCooldown = 0f;
	}

	protected virtual void InitializeWeapon()
	{
	}
}
