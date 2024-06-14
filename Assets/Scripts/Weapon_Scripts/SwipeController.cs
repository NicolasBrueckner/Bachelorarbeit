using UnityEngine;

public class SwipeController : WeaponController
{
	private GameObject _swipeObject;
	private Swipe _swipe;

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

		_swipe.ScaleSwipe();
	}

	protected override void InitializeWeapon()
	{
		base.InitializeWeapon();

		_swipeObject = Instantiate( weaponObject, transform );
		_swipe = _swipeObject.GetComponent<Swipe>();

		InitializeWeaponObject( _swipeObject, _swipe );
	}

	protected override void InternalToggleWeapon()
	{
		base.InternalToggleWeapon();

		_swipeObject.SetActive( _isActive_ );
	}
}
