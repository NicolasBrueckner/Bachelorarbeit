using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MenuScreen
{
	private Button _resumeButton;
	private Button _quitButton;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_resumeButton = _root_.Q<Button>( "Resume" );
		_quitButton = _root_.Q<Button>( "rawr" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_resumeButton.clicked += OnResumeButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	private void OnResumeButtonClicked()
	{
		Debug.Log( "ogbenroubg" );
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.HUD );
	}

	private void OnQuitButtonClicked()
	{
		Debug.Log( "ogbenroubg" );
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.GameOver );
	}
}
