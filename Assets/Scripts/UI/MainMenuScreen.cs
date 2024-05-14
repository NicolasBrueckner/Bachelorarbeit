using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MenuScreen
{
	private Button _startButton;
	private Button _quitButton;

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_startButton = _root_.Q<Button>( "Start" );
		_quitButton = _root_.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_startButton.clicked += OnStartButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	private void OnStartButtonClicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.HUD );
	}

	private void OnQuitButtonClicked()
	{

	}
}
