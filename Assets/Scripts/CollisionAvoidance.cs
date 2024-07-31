using Unity.Mathematics;
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
		Vector2 direction = ( transform.position - collider.transform.position ).normalized;
		float distance = math.max( physicsCollider.bounds.extents.x, physicsCollider.bounds.extents.y );

		rb2D.MovePosition( rb2D.position + ( direction * distance ) );
	}

	private bool IsInLayerMask( GameObject collisionObject )
	{
		return ( collisionMask.value & ( 1 << collisionObject.layer ) ) > 0;
	}
}
