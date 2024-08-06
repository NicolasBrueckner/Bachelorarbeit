using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugBuildUIFunctionality : MonoBehaviour
{
	public SliderInt amountSlider
	{
		get => _amountSlider;
		set
		{
			_amountSlider = value;
			_amountSlider.RegisterValueChangedCallback( evt => ChangeEnemyAmount( evt.newValue ) );
			ChangeEnemyAmount( _amountSlider.value );
		}
	}
	public Slider frequencySlider
	{
		get => frequencySlider;
		set
		{
			_frequencySlider = value;
			_frequencySlider.RegisterValueChangedCallback( evt => ChangeSpawnFrequency( evt.newValue ) );
			ChangeSpawnFrequency( _frequencySlider.value );
		}
	}
	public SliderInt granularitySlider
	{
		get => granularitySlider;
		set
		{
			_granularitySlider = value;
			_granularitySlider.RegisterValueChangedCallback( evt => ChangeGridGranularity( evt.newValue ) );
			ChangeGridGranularity( _granularitySlider.value );
		}
	}

	public Toggle logToggle;
	public Toggle invincibilityToggle;

	public Label logLocationLabel
	{
		get => _logLocationLabel;
		set
		{
			_logLocationLabel = value;
			DisplayLogLocation();
		}
	}
	public Label timeLabel;
	public Label fpsLabel;
	public Label msLabel;

	private Label _logLocationLabel;
	private SliderInt _amountSlider;
	private Slider _frequencySlider;
	private SliderInt _granularitySlider;

	private StreamWriter _writer;
	private float _enabledTime;
	private float _deltaTime;
	private float _logTimer;

	private float _totalFPS;
	private float _totalTPF;
	private float _entryCount;

	private void Start()
	{
		EventManager.Instance.OnStartGame += ResetEnabledTime;
	}

	private void Update()
	{
		UpdateFPS();
	}

	private void OnDestroy()
	{
		FinalizeLogging();
	}

	private void UpdateFPS()
	{
		_deltaTime += ( Time.unscaledDeltaTime - _deltaTime ) * 0.1f;
		_logTimer += Time.unscaledDeltaTime;

		if ( _logTimer > 0.2f )
		{
			float time = Time.time - _enabledTime;
			float fps = Mathf.Ceil( 1.0f / _deltaTime );
			float tpf = _deltaTime * 1000.0f;

			if ( timeLabel != null )
				timeLabel.text = $"Time: {time:F2}";
			if ( fpsLabel != null )
				fpsLabel.text = $"FPS: {fps}";
			if ( msLabel != null )
				msLabel.text = $"TPF: {tpf:F2} ms";

			_logTimer = 0;

			//Debug.Log( $"Time: {time}" );
			if ( time <= 60.0f )
				LogPerformance( time, fps, tpf );
			else
				FinalizeLogging();
		}
	}

	private void ResetEnabledTime()
	{
		_enabledTime = Time.time;
		DebugBuildStaticValues.isInvincible = invincibilityToggle.value;
		SetupLogFile();
	}

	private void SetupLogFile()
	{
		if ( logToggle.value )
		{
			string filepath = Path.Combine( Application.persistentDataPath, $"performance_log_{DateTime.Now:ddMMyyy_HHmmss}.csv" );
			_writer = new StreamWriter( filepath, true );
			_writer.WriteLine( "Amount,Frequency,Granularity,Time,FPS,TPF (ms)" );

			_totalFPS = 0;
			_totalTPF = 0;
			_entryCount = 0;
		}
	}

	private void LogPerformance( float time, float fps, float tpf )
	{
		if ( logToggle.value )
		{
			_writer?.WriteLine( $"{_amountSlider.value},{_frequencySlider.value:F2},{_granularitySlider.value},{time:F2},{fps},{tpf:F2}" );

			_totalFPS += fps;
			_totalTPF += tpf;
			_entryCount++;
		}
	}

	private void FinalizeLogging()
	{
		if ( _writer != null )
		{
			float averageFPS = _totalFPS / _entryCount;
			float averageTPF = _totalTPF / _entryCount;
			_writer.WriteLine( $",,,,{averageFPS},{averageTPF}" );

			_writer.Flush();
			_writer.Close();
			_writer = null;
		}
	}

	public void DisplayLogLocation()
	{
		_logLocationLabel.text = Application.persistentDataPath.ToString();
	}

	private void ChangeEnemyAmount( int value )
	{
		DebugBuildStaticValues.enemyAmount = value;
	}

	private void ChangeSpawnFrequency( float value )
	{
		DebugBuildStaticValues.spawnFrequency = value;
	}

	private void ChangeGridGranularity( int value )
	{
		SectorStats.cellRadius = DebugBuildStaticValues.granularityByValue[ value ].Item1;
		SectorStats.gridScalar = DebugBuildStaticValues.granularityByValue[ value ].Item2;
	}
}