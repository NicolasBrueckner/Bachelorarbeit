using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDMenuScreen : MenuScreen
{
	private Button _pauseButton;

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_pauseButton = _root_.Q<Button>( "Return" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_pauseButton.clicked += OnPauseButtonCLicked;
	}

	private void OnPauseButtonCLicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.Pause );
	}
}
