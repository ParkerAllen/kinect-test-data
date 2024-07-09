using Godot;
using System;
using System.Collections.Generic;

public partial class BoneListInput : InputBase<BoneListInputPanel, List<int>>
{
    public static readonly PackedScene packedScene = ResourceLoader.Load("res://Scenes/Prefabs/ExtendedBonesInputPanel.tscn") as PackedScene;
    
    public string Title { get; init; }

    public ArmatureManager ArmatureManager { get; init; }
    public int RemovedBone { get; set; }

    public BoneListInput(String title, ArmatureManager armatureManager, Action onUpdate) : base(onUpdate)
    {
        Title = title;
        ArmatureManager = armatureManager;
        RemovedBone = -1;
    }

    protected override PackedScene Prefab()
    {
        return BoneListInput.packedScene;
    }

    protected override void Init()
    {
        Element.Init(this);
    }
    
}
