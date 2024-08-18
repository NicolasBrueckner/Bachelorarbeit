using AYellowpaper.SerializedCollections;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum UIScreenType
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
	public DebugBuildUIFunctionality debugUI;

	private SerializedDictionary<UIScreenType, MenuScreen> _screensByType = new();
	private MenuScreen _currentScreen;
	private VisualElement _root;

	private InputActions _actions;
	private InputAction _pauseAction;
	private Action<InputAction.CallbackContext> _onPauseContext;

	private void Start()
	{
		_root = rootDocument.rootVisualElement;
		_actions = new();
		_onPauseContext = ctx =>
		{
			if ( _currentScreen?.Type == UIScreenType.HUD )
				ToggleScreen( UIScreenType.Pause );
		};

		_pauseAction = _actions.Player.Pause;
		_pauseAction.Enable();

		_pauseAction.performed += _onPauseContext;

		InitializeScreens();
		AddScreensToRoot();
		ToggleScreen( UIScreenType.Main );
	}

	private void OnDisable()
	{
		_pauseAction.performed -= _onPauseContext;

		_pauseAction.Disable();
	}

	private void InitializeScreens()
	{
		MenuScreen menuScreen;

		foreach ( UIScreenType type in _screensByType.Keys.ToList() )
		{
			menuScreen = UIScreenFactory.CreateScreen( type );
			menuScreen.screenAsset = _screensByType[ type ].screenAsset;
			_screensByType[ type ] = menuScreen;

			_screensByType[ type ].SetDefaults( type, this );
		}
	}

	private void AddScreensToRoot()
	{
		VisualElement currentScreenElement;

		foreach ( UIScreenType type in _screensByType.Keys.ToList() )
		{
			currentScreenElement = _screensByType[ type ].Root;
			currentScreenElement.style.display = DisplayStyle.None;
			currentScreenElement.AddToClassList( "ui-screen" );
			_root.Add( currentScreenElement );
		}
	}

	public void ToggleScreen( UIScreenType screenType )
	{
		if ( !_screensByType.ContainsKey( screenType ) )
			return;

		if ( _currentScreen != null )
		{
			_currentScreen.Root.style.display = DisplayStyle.None;
			_currentScreen.OnDeactivation();
		}

		_currentScreen = _screensByType[ screenType ];
		_currentScreen.OnActivation();
		_currentScreen.Root.style.display = DisplayStyle.Flex;
	}

	public MenuScreen GetScreenByType( UIScreenType screenType )
	{
		return _screensByType[ screenType ];
	}
}
