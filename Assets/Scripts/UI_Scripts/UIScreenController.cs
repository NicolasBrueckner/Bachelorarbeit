using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum UIScreenTypes
{
	Main,
	HUD,
	Pause,
	GameOver,
	LevelUp,
}

public class UIScreenController : MonoBehaviour
{
	public UIDocument rootDocument;
	public SerializedDictionary<UIScreenTypes, MenuScreen> _screensByType = new();

	private VisualElement _currentScreen;
	private VisualElement _root;

	private InputActions _actions;
	private InputAction _pauseAction;
	private Action<InputAction.CallbackContext> _onPauseContext;

	private void Awake()
	{
		_root = rootDocument.rootVisualElement;
		_actions = new();
		_onPauseContext = ctx => ShowScreen( UIScreenTypes.Pause );

		InitializeScreens();
		AddScreensToRoot();
		ShowScreen( UIScreenTypes.Main );
	}

	private void OnEnable()
	{
		_pauseAction = _actions.Player.Pause;
		_pauseAction.Enable();

		_pauseAction.performed += _onPauseContext;
	}

	private void OnDisable()
	{
		_pauseAction.performed -= _onPauseContext;

		_pauseAction.Disable();
	}

	private void InitializeScreens()
	{
		foreach ( UIScreenTypes type in _screensByType.Keys )
			_screensByType[ type ].SetDefaults( type, this );
	}

	private void AddScreensToRoot()
	{
		VisualElement currentScreen;

		foreach ( UIScreenTypes type in _screensByType.Keys )
		{
			currentScreen = _screensByType[ type ].Root;
			currentScreen.style.display = DisplayStyle.None;
			currentScreen.AddToClassList( "ui-screen" );
			_root.Add( currentScreen );
		}
	}

	public void ShowScreen( UIScreenTypes screenType )
	{
		if ( !_screensByType.ContainsKey( screenType ) )
			return;

		if ( _currentScreen != null )
			_currentScreen.style.display = DisplayStyle.None;

		_currentScreen = _screensByType[ screenType ].Root;
		_currentScreen.style.display = DisplayStyle.Flex;
	}

	public MenuScreen GetScreenByType( UIScreenTypes screenType )
	{
		return _screensByType[ screenType ];
	}
}
