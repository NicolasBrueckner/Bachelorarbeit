using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MenuScreen
{
	private Button _resumeButton;
	private Button _quitButton;

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_resumeButton = _root_.Q<Button>( "Resume" );
		_quitButton = _root_.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_resumeButton.clicked += OnResumeButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	private void OnResumeButtonClicked()
	{

	}

	private void OnQuitButtonClicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.GameOver );
	}
}
