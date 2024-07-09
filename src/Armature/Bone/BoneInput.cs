using Godot;
using System;

public partial class BoneInput : InputBase<BoneInputPanel, int>
{
    public static readonly PackedScene packedScene = ResourceLoader.Load("res://Scenes/Prefabs/BoneInputPanel.tscn") as PackedScene;
    
    public string Title { get; init; }
    public ArmatureManager ArmatureManager { get; init; }

    public BoneInput(String title, ArmatureManager armatureManager, Action onUpdate) : base(onUpdate)
    {
        Title = title;
        ArmatureManager = armatureManager;
    }

    public BoneInput(Action onUpdate) : base(onUpdate)
    {
        Title = "";
    }

    protected override PackedScene Prefab()
    {
        return BoneInput.packedScene;
    }

    protected override void Init()
    {
        Element.Init(this);
    }
}
