using System.Collections.Generic;
using UnityEngine.UIElements;
using static StatNames;

public class LevelUpScreen : MenuScreen
{
	private (Button, Label, Label) choice1;
	private (Button, Label, Label) choice2;
	private (Button, Label, Label) choice3;
	private Dictionary<Button, (StatType, float)> _valuesByElement = new();

	protected override void SetDefaultsInternal( UIScreenTypes type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );

		_valuesByElement[ choice1.Item1 ] = (StatType.none, 0f);
		_valuesByElement[ choice2.Item1 ] = (StatType.none, 0f);
		_valuesByElement[ choice3.Item1 ] = (StatType.none, 0f);
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

		choice1.Item1 = Root.Q<Button>( "Item_1" );
		choice2.Item1 = Root.Q<Button>( "Item_2" );
		choice3.Item1 = Root.Q<Button>( "Item_3" );
		choice1.Item2 = Root.Q<Label>( "Name_1" );
		choice2.Item2 = Root.Q<Label>( "Name_2" );
		choice3.Item2 = Root.Q<Label>( "Name_3" );
		choice1.Item3 = Root.Q<Label>( "Value_1" );
		choice2.Item3 = Root.Q<Label>( "Value_2" );
		choice3.Item3 = Root.Q<Label>( "Value_3" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		choice1.Item1.clicked += UpgradeClicked;
		choice2.Item1.clicked += UpgradeClicked;
		choice3.Item1.clicked += UpgradeClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		EventManager.Instance.OnLevelUp += OnLevelUp;
	}

	private void OnLevelUp( List<(StatType type, float value)> itemValues )
	{
		_valuesByElement[ choice1.Item1 ] = (itemValues[ 0 ].type, itemValues[ 0 ].value);
		_valuesByElement[ choice2.Item1 ] = (itemValues[ 0 ].type, itemValues[ 0 ].value);
		_valuesByElement[ choice3.Item1 ] = (itemValues[ 0 ].type, itemValues[ 0 ].value);

		choice1.Item2.text = statNames[ _valuesByElement[ choice1.Item1 ].Item1 ];
		choice2.Item2.text = statNames[ _valuesByElement[ choice2.Item1 ].Item1 ];
		choice3.Item2.text = statNames[ _valuesByElement[ choice3.Item1 ].Item1 ];

		choice1.Item3.text = ( _valuesByElement[ choice1.Item1 ].Item2 * 100 ).ToString() + "%";
		choice2.Item3.text = ( _valuesByElement[ choice2.Item1 ].Item2 * 100 ).ToString() + "%";
		choice3.Item3.text = ( _valuesByElement[ choice3.Item1 ].Item2 * 100 ).ToString() + "%";

		uiScreenController.ShowScreen( UIScreenTypes.LevelUp );
	}

	private void UpgradeClicked()
	{

	}
}