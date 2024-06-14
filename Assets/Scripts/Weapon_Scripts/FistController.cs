using UnityEngine;

public class FistController : WeaponController
{
	private GameObject _fistObject;
	private Fist _fist;

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

		_fist.ScaleFist();
	}

	protected override void InitializeWeapon()
	{
		base.InitializeWeapon();

		_fistObject = Instantiate( weaponObject, transform );
		_fist = _fistObject.GetComponent<Fist>();

		InitializeWeaponObject( _fistObject, _fist );
	}

	protected override void InternalToggleWeapon()
	{
		base.InternalToggleWeapon();

		_fistObject.SetActive( _isActive_ );
	}
}
