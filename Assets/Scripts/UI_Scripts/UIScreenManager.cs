using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum UIScreenTypes
{
	Main,
	HUD,
	Pause,
	GameOver,
}

public class UIScreenManager : MonoBehaviour
{
	public static UIScreenManager Instance { get; private set; }

	public UIDocument rootDocument;

	private Dictionary<UIScreenTypes, VisualElement> _screensByType = new();
	private VisualElement _currentScreen;
	private VisualElement _root;

	private void Awake()
	{
		if ( Instance != null && Instance != this )
		{
			Destroy( gameObject );
			return;
		}

		Instance = this;
		DontDestroyOnLoad( gameObject );
	}

	private void Start()
	{
		_root = rootDocument.rootVisualElement;

		InitializeScreens();
		ShowScreen( UIScreenTypes.Main );
	}

	private void InitializeScreens()
	{
		VisualElement currentScreen;

		foreach ( UIScreenTypes type in _screensByType.Keys )
		{
			currentScreen = _screensByType[ type ];
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

		_currentScreen = _screensByType[ screenType ];
		_currentScreen.style.display = DisplayStyle.Flex;
	}

	public void AddScreen( UIScreenTypes type, VisualElement screen )
	{
		if ( !_screensByType.ContainsKey( type ) )
			_screensByType[ type ] = screen;
	}

	public VisualElement GetScreenByType( UIScreenTypes screenType )
	{
		return _screensByType[ screenType ];
	}
}
