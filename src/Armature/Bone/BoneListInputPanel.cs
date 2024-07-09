using Godot;
using System.Collections.Generic;

public partial class BoneListInputPanel : InputPanelBase<List<int>>
{
	private static readonly PackedScene packedScene = ResourceLoader.Load("res://Scenes/Prefabs/BoneButton.tscn") as PackedScene;
	[Export]
	public Label TitleLabel { get; private set; }
	[Export]
	public Button AddButton { get; private set; }
    private Node container;

	private List<Button> boneButtons = new List<Button>();
    public BoneListInput Manager { get; protected set; }

    public override void _Ready() 
    {
        container = GetChild(0);
	}

	public void Init(BoneListInput manager)
	{
		Manager = manager;
		if(TitleLabel != null)
		{
			TitleLabel.Text = Manager.Title;
		}
	}

    public void OnUpdate()
    {
        Manager.OnUpdate();
    }

	public override void Display(List<int> bones)
	{
		Value = bones;
		Display();
	}

	private void Display()
	{
		while (Value.Count > boneButtons.Count)
		{
			InitializeBone();
		}
		while (Value.Count < boneButtons.Count)
		{
			DeinitializeBone();
		}
		for (int i = 0; i < Value.Count; i++)
		{
			DisplayButton(i, Manager.ArmatureManager.Get(Value[i]));
		}
		Show();
	}
	
	private void InitializeBone()
	{
		Button element = BoneListInputPanel.packedScene.Instantiate() as Button;
		boneButtons.Add(element);
		element.ButtonUp += () => RemoveBone(boneButtons.Count - 1);
		container.AddChild(element);
	}

	private void DeinitializeBone()
	{
		boneButtons[boneButtons.Count - 1].QueueFree();
		boneButtons.RemoveAt(boneButtons.Count - 1);
	}

	private void DisplayButton(int index, Bone bone)
	{
		boneButtons[index].Text =  bone.BoneSettings.Name;
		boneButtons[index].Modulate = bone.BoneSettings.Color;
	}

	private void RemoveBone(int bone)
	{
		Logs.Print("Removing Extended Bone...");
		Manager.RemovedBone = Value[bone];
		Value.RemoveAt(bone);
		Display();
		Manager.OnUpdate();
	}

	private void AddBone()
	{
		Manager.OnUpdate();
	}
}
