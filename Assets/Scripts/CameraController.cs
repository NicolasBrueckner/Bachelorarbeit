using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float3 target;

	private void Update()
	{
		transform.position = target;
	}
}
