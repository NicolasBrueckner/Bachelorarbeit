using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelUpMenuScreen : MenuScreen
{
	private LevelUpItem _item1;
	private LevelUpItem _item2;
	private LevelUpItem _item3;
	private Label _upgradeInstanceLabel;

	protected override void SetDefaultsInternal( UIScreenType type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );

		_item1 = new( this, Root.Q<VisualElement>( "Item_1" ) );
		_item2 = new( this, Root.Q<VisualElement>( "Item_2" ) );
		_item3 = new( this, Root.Q<VisualElement>( "Item_3" ) );
	}

	protected override void OnActivationInternal()
	{
		base.OnActivationInternal();

		Time.timeScale = 0;
	}

	protected override void OnDeactivationInternal()
	{
		base.OnDeactivationInternal();

		Time.timeScale = 1;
	}

	protected override void GetElements()
	{
		base.GetElements();

		_upgradeInstanceLabel = Root.Q<Label>( "Instance" );
	}

	protected override void BindElements()
	{
		base.BindElements();
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		EventManager.Instance.OnLevelUp += OnLevelUp;
	}

	private void OnLevelUp( Stats stats, List<StatType> types, List<float> values )
	{
		uiScreenController.ToggleScreen( Type );

		_upgradeInstanceLabel.text = stats.statName + " Upgrade";

		_item1.SetItemValues( stats, types[ 0 ], values[ 0 ] );
		_item2.SetItemValues( stats, types[ 1 ], values[ 1 ] );
		_item3.SetItemValues( stats, types[ 2 ], values[ 2 ] );

		uiScreenController.ToggleScreen( UIScreenType.LevelUp );
	}

	public void OnUpgradePicked( Stats stats, StatType type, float value )
	{
		EventManager.Instance.UpgradePicked( stats, type, value );
		uiScreenController.ToggleScreen( UIScreenType.HUD );
	}
}