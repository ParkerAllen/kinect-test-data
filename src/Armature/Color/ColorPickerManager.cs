using Godot;
using System;

public partial class ColorPickerManager : Window
{
    private ColorPicker ColorPicker;

    public Action<Color> target;

    public override void _Ready()
    {
        ColorPicker = GetChild<ColorPicker>(0);
    }

    public void Show(Color color, Action<Color> action)
    {
        Show();
        ColorPicker.Color = color;
        target = action;
    }

    public void OnUpdate(Color color)
    {
        target?.Invoke(color);
    }
}
