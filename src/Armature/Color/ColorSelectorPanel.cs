using Godot;

public partial class ColorSelectorPanel : InputPanelBase<Color>
{
    [Export]
    public Label TitleLabel { get; private set; }
    [Export]
    public Button Button { get; private set; }
    [Export]
    public ColorRect ColorRect { get; private set; }

    public ColorSelector Manager { get; protected set; }

    public void Init(ColorSelector manager)
    {
        Manager = manager;
        TitleLabel.Text = Manager.Title;
    }

    private void OnUpdate(Color color)
    {
        Value = color;
        Manager.OnUpdate();
    }

    public override void Display(Color color)
    {
        // OnUpdate(color);
        Value = color;
        ColorRect.Color = Value;
    }
    
    private void OnClick()
    {
        Manager.ColorPicker.Show(ColorRect.Color, OnUpdate);
    }
}
