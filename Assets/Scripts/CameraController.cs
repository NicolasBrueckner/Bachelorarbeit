using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
	public GameObject target;
	public float3 offset = new float3( 0, 0, -10 );

	private void Update()
	{
		FollowTarget();
	}

	private void FollowTarget()
	{
		transform.position = ( float3 )target.transform.position + offset;
	}
}
