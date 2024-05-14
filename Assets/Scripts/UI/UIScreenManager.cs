using System.Collections;
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
	public MenuScreen[] screens;

	private Dictionary<UIScreenTypes, VisualElement> screensByType = new();
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
		InitiateScreens();
	}

	private void InitiateScreens()
	{
		_root = rootDocument.rootVisualElement;

		foreach ( MenuScreen screen in screens )
		{
			VisualElement tempScreen = _root.Q( screen.type.ToString() );
			screensByType[ screen.type ] = tempScreen;
		}

		foreach ( UIScreenTypes screenType in screensByType.Keys )
			screensByType[ screenType ].style.display = DisplayStyle.None;

		ShowScreen( UIScreenTypes.Main );
	}

	public void ShowScreen( UIScreenTypes screenType )
	{
		if ( screensByType.ContainsKey( screenType ) )
			return;

		if ( _currentScreen != null )
			_currentScreen.style.display = DisplayStyle.None;

		_currentScreen = screensByType[ screenType ];
		_currentScreen.style.display = DisplayStyle.Flex;
	}

	public VisualElement GetScreenByType( UIScreenTypes screenType )
	{
		return screensByType[ screenType ];
	}
}
