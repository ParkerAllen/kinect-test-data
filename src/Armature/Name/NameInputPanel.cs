using Godot;

public partial class NameInputPanel : InputPanelBase<string>
{
    [Export]
    public Label TitleLabel { get; private set; }
	[Export]
	public LineEdit LineEdit { get; private set; }

    public NameInput Manager { get; protected set; }

    public void Init(NameInput manager)
    {
        Manager = manager;
        TitleLabel.Text = Manager.Title;
    }

    public void OnUpdate()
    {
        Value = LineEdit.Text;
        Manager.OnUpdate();
    }

    public override void Display(string value)
    {
        Value = value;
        LineEdit.Text = Value;
    }
}
