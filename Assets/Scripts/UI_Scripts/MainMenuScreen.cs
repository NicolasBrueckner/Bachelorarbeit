using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MenuScreen
{
	private Label _logLocationLabel;
	private Toggle _logToggle;
	private Toggle _invincibilityToggle;
	private SliderInt _amountSlider;
	private Slider _frequencySlider;
	private SliderInt _granularitySlider;

	private Button _startButton;
	private Button _quitButton;

	protected override void SetDefaultsInternal( UIScreenType type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );

		DebugUI.logLocationLabel = _logLocationLabel;
		DebugUI.logToggle = _logToggle;
		DebugUI.invincibilityToggle = _invincibilityToggle;
		DebugUI.amountSlider = _amountSlider;
		DebugUI.frequencySlider = _frequencySlider;
		DebugUI.granularitySlider = _granularitySlider;
	}

	protected override void OnActivationInternal()
	{
		base.OnActivationInternal();
	}

	protected override void OnDeactivationInternal()
	{
		base.OnDeactivationInternal();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_logLocationLabel = Root.Q<Label>( "LogLocationLabel" );
		_logToggle = Root.Q<Toggle>( "LogToggle" );
		_invincibilityToggle = Root.Q<Toggle>( "InvincibilityToggle" );
		_amountSlider = Root.Q<SliderInt>( "EnemyAmountSlider" );
		_frequencySlider = Root.Q<Slider>( "SpawnFrequencySlider" );
		_granularitySlider = Root.Q<SliderInt>( "GridGranularitySlider" );

		_startButton = Root.Q<Button>( "Start" );
		_quitButton = Root.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_startButton.clicked += OnStartButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();
	}

	private void OnStartButtonClicked()
	{
		EventManager.Instance.StartGame();
		uiScreenController.ToggleScreen( UIScreenType.HUD );
	}

	private void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}
