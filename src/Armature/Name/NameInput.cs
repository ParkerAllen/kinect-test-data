using Godot;
using System;

public class NameInput : InputBase<NameInputPanel, string>
{
    public static readonly PackedScene packedScene = ResourceLoader.Load("res://Scenes/Prefabs/NameInputPanel.tscn") as PackedScene;
    
    public string Title { get; init; }

    public NameInput(String title, Action updateEvent) : base(updateEvent)
    {
        Title = title;
    }

    protected override PackedScene Prefab()
    {
        return NameInput.packedScene;
    }

    protected override void Init()
    {
        Element.Init(this);
    }
}
