using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public CharacterStats stats;

	private void Awake()
	{
		stats = new CharacterStats();
	}
}
