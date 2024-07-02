using UnityEngine.UIElements;

public class LevelUpItem
{
	public StatType upgradeType;
	public float upgradeValue;

	public VisualElement root;
	public Button button;
	public Label name;
	public Label value;

	public LevelUpItem( VisualElement root )
	{
		this.root = root;

		GetElements();
		BindElements();
	}

	public void GetElements()
	{
		button = root.Q<Button>( "ItemButton" );
		name = root.Q<Label>( "ItemName" );
		value = root.Q<Label>( "ItemValue" );
	}

	public void BindElements()
	{
		button.clicked += OnItemClicked;
	}

	private void OnItemClicked()
	{

	}
}
