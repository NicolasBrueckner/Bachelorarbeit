using UnityEngine.UIElements;
using static StatNames;

public class LevelUpItem
{
	public Stats stats;
	public StatType type;
	public float value;

	public VisualElement root;
	public Button itemButton;
	public Label nameLabel;
	public Label valueLabel;

	public LevelUpItem( VisualElement root )
	{
		this.root = root;

		GetElements();
		BindElements();
	}

	private void GetElements()
	{
		itemButton = root.Q<Button>( "ItemButton" );
		nameLabel = root.Q<Label>( "ItemName" );
		valueLabel = root.Q<Label>( "ItemValue" );
	}

	private void BindElements()
	{
		itemButton.clicked += OnItemClicked;
	}

	public void SetItemValues( Stats stats, StatType type, float value )
	{
		this.stats = stats;
		this.type = type;
		this.value = value;
		nameLabel.text = statNames[ type ];
		valueLabel.text = "+" + ( value * 100 ).ToString() + "%";
	}

	private void OnItemClicked()
	{
		EventManager.Instance.UpgradePicked( stats, type, value );
	}
}
