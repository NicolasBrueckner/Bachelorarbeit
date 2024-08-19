using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
	public LayerMask collisionMask;
	public Rigidbody2D rb2D;
	public Collider2D physicsCollider;


	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( IsInLayerMask( collision.gameObject ) )
			ResolveCollisionOverlap( collision.collider );
	}

	private void ResolveCollisionOverlap( Collider2D collider )
	{
		ColliderDistance2D collisionDistance = physicsCollider.Distance( collider );

		if ( collisionDistance.isOverlapped )
		{
			Vector2 pushDirection = collisionDistance.normal;
			float pushDistance = collisionDistance.distance;

			rb2D.MovePosition( rb2D.position + ( pushDirection * -pushDistance ) );
		}
	}

	private bool IsInLayerMask( GameObject collisionObject )
	{
		return ( collisionMask.value & ( 1 << collisionObject.layer ) ) > 0;
	}
}
