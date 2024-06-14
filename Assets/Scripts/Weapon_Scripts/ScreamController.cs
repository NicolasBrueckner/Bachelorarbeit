using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamController : WeaponController
{
	private GameObject _screamObject;
	private Scream _scream;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void Attack()
	{
		base.Attack();
	}

	protected override void InitializeWeapon()
	{
		base.InitializeWeapon();

		_screamObject = Instantiate( weaponObject, transform );
		_scream = _screamObject.GetComponent<Scream>();

		InitializeWeaponObject( _screamObject, _scream );
	}

	protected override void InternalToggleWeapon()
	{
		base.InternalToggleWeapon();

		_screamObject.SetActive( _isActive_ );
	}
}
