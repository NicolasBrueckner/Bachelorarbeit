using System.Collections.Generic;
using UnityEngine.UIElements;

public class LevelUpScreen : MenuScreen
{
	private LevelUpItem _item1;
	private LevelUpItem _item2;
	private LevelUpItem _item3;
	private Label _upgradeInstanceLabel;

	protected override void SetDefaultsInternal( UIScreenTypes type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );

		_item1 = new( Root.Q<VisualElement>( "Item_1" ) );
		_item1 = new( Root.Q<VisualElement>( "Item_2" ) );
		_item1 = new( Root.Q<VisualElement>( "Item_3" ) );
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

		_upgradeInstanceLabel = Root.Q<Label>( "InstanceLabel" );
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
		_upgradeInstanceLabel.text = stats.statName + " Upgrade";

		_item1.SetItemValues( stats, types[ 0 ], values[ 0 ] );
		_item2.SetItemValues( stats, types[ 1 ], values[ 1 ] );
		_item3.SetItemValues( stats, types[ 2 ], values[ 2 ] );

		uiScreenController.ShowScreen( UIScreenTypes.LevelUp );
	}
}