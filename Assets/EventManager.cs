using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	public event Action<float> OnHealthChanged;
	public event Action OnPlayerDied;
	public event Action OnLevelUp;

	private void Awake()
	{
		if ( Instance == null )
		{
			Instance = this;
			DontDestroyOnLoad( gameObject );
		}
		else
			Destroy( gameObject );
	}

	public void HealthChanged( float health )
	{
		OnHealthChanged?.Invoke( health );
	}

	public void PlayerDied()
	{
		OnPlayerDied?.Invoke();
	}

	public void LevelUp()
	{
		OnLevelUp?.Invoke();
	}

	public void UpgradeSelected()
	{

	}
}
