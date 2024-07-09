using Godot;

public partial class VectorInputPanel : InputPanelBase<Vector2>
{
    [Export]
    public SpinBox XInput { get; private set; }
	[Export]
    public SpinBox YInput { get; private set; }

    public VectorInput Manager { get; protected set; }

    public void Init(VectorInput manager)
    {
        Manager = manager;
    }

    private void OnUpdate(float value = 0)
    {
        Value = new Vector2((float)XInput.Value, (float)YInput.Value);
        Manager.OnUpdate();
    }

    public override void Display(Vector2 vector2)
    {
        Value = vector2;
        XInput.Value = Value.X;
        YInput.Value = Value.Y;
    }
}
