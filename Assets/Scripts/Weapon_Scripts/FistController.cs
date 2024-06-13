using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistController : WeaponController
{
	private Fist _fist;

	protected override void Start()
	{
		base.Start();

		_fist = weapon as Fist;
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void Attack()
	{
		base.Attack();

		_fist.ScaleWeapon( Frequency );
	}

	protected override void InitializeWeapon()
	{
		base.InitializeWeapon();

		_weaponObjectCopy_ = Instantiate( weaponObject );
		_fist = _weaponObjectCopy_.GetComponent<Fist>();

		_fist.controller = this;
		_weaponObjectCopy_.SetActive( false );
	}
}
