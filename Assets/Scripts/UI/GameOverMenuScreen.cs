using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenuScreen : MenuScreen
{
	private Button _restartButton;
	private Button _returnButton;

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_restartButton = _root_.Q<Button>( "Restart" );
		_returnButton = _root_.Q<Button>( "Return" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_restartButton.clicked += OnRestartButtonClicked;
		_returnButton.clicked += OnReturnButtonCLicked;
	}

	private void OnRestartButtonClicked()
	{

	}

	private void OnReturnButtonCLicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.Main );
	}
}
