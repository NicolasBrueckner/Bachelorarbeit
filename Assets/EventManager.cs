using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	public event Action<Stats, List<StatType>, List<float>> OnLevelUp;
	public event Action<Stats, StatType, float> OnUpgradePicked;
	public event Action<WeaponController, bool> OnWeaponToggled;
	public event Action<float> OnHealthChanged;
	public event Action OnDependenciesInjected;
	public event Action OnWorldCreated;
	public event Action OnPlayerDied;
	public event Action OnStartGame;
	public event Action OnEndGame;

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

	public void LevelUp( Stats stats, List<StatType> types, List<float> values ) => OnLevelUp?.Invoke( stats, types, values );
	public void UpgradePicked( Stats stats, StatType type, float value ) => OnUpgradePicked?.Invoke( stats, type, value );
	public void WeaponToggled( WeaponController controller, bool isActive ) => OnWeaponToggled?.Invoke( controller, isActive );
	public void HealthChanged( float health ) => OnHealthChanged?.Invoke( health );
	public void DependenciesInjected() => OnDependenciesInjected?.Invoke();
	public void WorldCreated() => OnWorldCreated?.Invoke();
	public void PlayerDied() => OnPlayerDied?.Invoke();
	public void StartGame() => OnStartGame?.Invoke();
	public void EndGame() => OnEndGame?.Invoke();
}
