using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MenuScreen
{
	private Button _startButton;
	private Button _quitButton;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void GetElements()
	{
		base.GetElements();

		Debug.Log( "entered GetElements in MainMenuScreen" );
		_startButton = _root_.Q<Button>( "Start" );
		_quitButton = _root_.Q<Button>( "Quit" );

		if ( _startButton != null )
			Debug.Log( $"StartButton is not null, text on it: {_startButton.text}" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		if ( _startButton != null && _quitButton != null )
		{
			Debug.Log( "entered BindElements in MainMenuScreen" );
			_startButton.clicked += OnStartButtonClicked;
			_quitButton.clicked += OnQuitButtonClicked;
		}
	}

	private void OnStartButtonClicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.HUD );
		Debug.LogWarning( "entered OnStartButtonClicked in MainMenuScreen" );
	}

	private void OnQuitButtonClicked()
	{
		Debug.LogWarning( "entered OnStartButtonClicked in MainMenuScreen" );
	}
}
