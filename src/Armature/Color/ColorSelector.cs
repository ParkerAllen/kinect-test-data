using Godot;
using System;

public class ColorSelector : InputBase<ColorSelectorPanel, Color>
{
    public static readonly PackedScene packedScene = ResourceLoader.Load("res://Scenes/Prefabs/ColorSelectorPanel.tscn") as PackedScene;
    
    public string Title { get; init; }
    public ColorPickerManager ColorPicker { get; init; }

    public ColorSelector(String title, ColorPickerManager colorPicker, Action onUpdate) : base(onUpdate)
    {
        Title = title;
        ColorPicker = colorPicker;
    }

    protected override PackedScene Prefab()
    {
        return ColorSelector.packedScene;
    }

    protected override void Init()
    {
        Element.Init(this);
    }
}
