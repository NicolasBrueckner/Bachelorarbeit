using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject target;

	private void Update()
	{
		transform.position = target.transform.position;
	}
}
