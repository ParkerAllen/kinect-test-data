using Godot;
using System;

public partial class VectorInput : InputBase<VectorInputPanel, Vector2>
{
    public static readonly PackedScene packedScene = ResourceLoader.Load("res://Scenes/Prefabs/VectorInputPanel.tscn") as PackedScene;

    public VectorInput(Action onUpdate) : base(onUpdate)
    {
    }

    protected override PackedScene Prefab()
    {
        return VectorInput.packedScene;
    }

    protected override void Init()
    {
        Element.Init(this);
    }
}
