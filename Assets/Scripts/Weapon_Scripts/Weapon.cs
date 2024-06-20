using EditorAttributes;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[ReadOnly]
	public WeaponController controller;
	[ReadOnly]
	public WeaponStats currentStats;

	public Vector2 Direction => controller ? controller.Direction : Vector2.zero;
	public Vector3 Size => Vector3.one * currentStats.size;

	protected int _enemyLayer_;
	protected Transform _transform_;
	protected Collider2D _collider_;

	public virtual void SetDefaults()
	{
		_enemyLayer_ = LayerMask.NameToLayer( "Enemy" );
		_transform_ = transform;
		_collider_ = GetComponent<Collider2D>();
		ScaleToSize();
	}

	public void StartAttack()
	{
		StartAttackInternal();
	}

	protected virtual void StartAttackInternal()
	{

	}

	public void ScaleToSize()
	{
		_transform_.localScale = Size;
	}

	protected void DoDamage()
	{

	}

	protected void RotateToDirection()
	{
		_transform_.rotation = Quaternion.LookRotation( Vector3.forward, Direction );
	}

	protected void DestroyWeaponObject()
	{
		StopAllCoroutines();
		controller.EnqueueWeaponObject( gameObject );
	}
}
