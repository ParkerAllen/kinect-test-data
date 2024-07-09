using Godot;

public partial class BoneInputPanel : InputPanelBase<int>
{
    [Export]
    public Label TitleLabel { get; private set; }
    [Export]
    public Button Button { get; private set; }

    public BoneInput Manager { get; protected set; }

    public void Init(BoneInput manager)
    {
        Manager = manager;
        if(TitleLabel != null)
        {
            TitleLabel.Text = Manager.Title;
        }
    }

    private void OnClick()
    {
        Manager.OnUpdate();
    }

    public override void Display(int bone)
    {
        Value = bone;
        Display(Manager.ArmatureManager.Get(Value));
    }

    private void Display(Bone bone)
    {
        if(bone == null)
        {
            Button.Text = "<empty>";
            Button.Modulate = Colors.White;
            return;
        }
        Button.Text = bone.BoneSettings.Name;
		Button.Modulate = bone.BoneSettings.Color;
    }
}
