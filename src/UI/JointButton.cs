using Godot;

public partial class JointButton : Button
{
	private ArmatureEditor ArmatureEditor { get; set; }
	private Vector2 Offset { get; set; }
	public int Index { get; private set; }

	public void Init(ArmatureEditor armatureEditor, int index, BoneSettings boneSettings)
	{
		ArmatureEditor = armatureEditor;
		Index = index;
		Offset = this.Size * this.Scale / 2;
		this.Position = boneSettings.Position - Offset;
		this.Modulate = boneSettings.Color;
	}

	public void Select()
	{
		ArmatureEditor.SelectJoint(Index, !Input.IsActionPressed("ctrl_key"));
	}

}
