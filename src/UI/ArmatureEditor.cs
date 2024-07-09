using Godot;
using System.Collections.Generic;

public partial class ArmatureEditor
{
    private readonly PackedScene jointPrefab = ResourceLoader.Load("res://Scenes/Prefabs/JointButton.tscn") as PackedScene;

    public ArmatureManager ArmatureManager { get; init; }
    public Button ArmatureDisplay { get; init; }

    public Vector2 oldMousePos;
	public Vector2 MouseVelocity { get; private set; }
	public Vector2 MousePivot { get; private set; }

	private List<JointButton> jointPool = new List<JointButton>();

	private readonly int ArrowWidth = 6;
	private readonly int ArrowLength = 14;
	private readonly int ArrowOffset = 8;
	private readonly int ArrowThickness = 1;

	public bool IsSelectable { get; private set; }
	public bool EditChildren { get; private set; }

    public ArmatureEditor(ArmatureManager armatureManager, Button armatureRotateButton, bool isSelectable = true)
    {
		Logs.Print("Loading ArmatureEditor...");
        ArmatureManager = armatureManager;
        ArmatureDisplay = armatureRotateButton;
        ArmatureManager.BoneUpdateEvent += ReinitializeJoints;
		ArmatureDisplay.Draw += Draw;
		IsSelectable = isSelectable;
		EditChildren = true;
    }

    public void SelectJoint(int index, bool editChildren = true)
    {
		if(!IsSelectable)
		{
			return;
		}
		// Logs.Print("editChildren: " + editChildren);
		EditChildren = editChildren;
        ArmatureManager.SelectBone(index);
    }

    public void UpdateMouse(Vector2 mousePosition)
	{
		// Logs.Print("mousePosition: " + mousePosition);
		MouseVelocity = mousePosition - oldMousePos;
		if(ArmatureManager.Index < jointPool.Count && jointPool[ArmatureManager.Index].ButtonPressed)
		{
			MoveNodes(ClampVelocity(ArmatureManager.Target.BoneSettings.Position, MouseVelocity));
		}
		else if(ArmatureDisplay.ButtonPressed)
		{
			RotateNodes(mousePosition);
		}
		oldMousePos = mousePosition;
	}

	private void MoveNodes(Vector2 velocity)
	{
		if(EditChildren)
		{
			List<int> children = ArmatureManager.GetChildren(ArmatureManager.Index);
			foreach(int index in children)
			{
				ArmatureManager.Move(index, velocity);
			}
			ArmatureManager.UpdateBone();
			return;
		}
		ArmatureManager.Move(velocity);
	}

	private void RotateNodes(Vector2 mousePosition)
	{
		Vector2 origin = jointPool[ArmatureManager.Index].Position;
		Vector2 a = oldMousePos - origin;
		Vector2 b = mousePosition - origin;
		float angle = a.AngleTo(b);
		float s = Mathf.Sin(angle);
		float c = Mathf.Cos(angle);
		Logs.Print("Angle: " + angle);
		Logs.Print("b: " + b);
		
		List<int> children = ArmatureManager.GetChildren(ArmatureManager.Index);
		foreach(int index in children)
		{
			Vector2 nodePos = jointPool[index].Position;
			Vector2 pos = nodePos - origin;
			Vector2 velocity = new Vector2(
				c * pos.X - s * pos.Y + origin.X,
				s * pos.X + c * pos.Y + origin.Y
			);
			ArmatureManager.Move(index, velocity - nodePos);
		}

		ArmatureManager.UpdateBone();
	}

    private void ReinitializeJoints()
	{
		// Logs.Print("Reinitialize Joints...");
		int numbones = ArmatureManager.Get().Count;

		while(jointPool.Count > numbones)
		{
			Logs.Print("Freeing Joints...");
			jointPool[0].QueueFree();
			jointPool.RemoveAt(0);
		}
		while(jointPool.Count < numbones)
		{
			Logs.Print("Adding Joints...");
			JointButton temp = jointPrefab.Instantiate() as JointButton;
        	ArmatureDisplay.AddChild(temp);
			jointPool.Add(temp);
		}
		for(int i = 0; i < numbones; i++)
		{
			jointPool[i].Init(this, i, ArmatureManager.Get(i).BoneSettings);
		}
		ArmatureDisplay.QueueRedraw();
	}

    public void Draw()
    {
		// Logs.Print("Drawing Joints Connections...");
        foreach (Bone current in ArmatureManager.Get())
		{
			foreach (int extended in current.BoneNode.Extended)
			{
				Color color = ArmatureManager.Get(extended).BoneSettings.Color;
				color.A = .25f;
				DrawArrow(current.BoneSettings.Position, ArmatureManager.Get(extended).BoneSettings.Position, color);
			}
			Bone next = ArmatureManager.Get(current.BoneNode.Next);
			if(next == null)
			{
				continue;
			}
			DrawArrow(current.BoneSettings.Position, next.BoneSettings.Position, next.BoneSettings.Color);
		}
    }

    private void DrawArrow(Vector2 from, Vector2 to, Color color)
	{
		Vector2 dir = new Vector2(from.X - to.X, from.Y - to.Y).Normalized();
		Vector2 toOffset = new Vector2(to.X + dir.X * ArrowOffset, to.Y + dir.Y * ArrowOffset);
		Vector2 Length = new Vector2(dir.X * ArrowLength, dir.Y * ArrowLength);
		Vector2 side1 = new Vector2(-dir.Y * ArrowWidth, dir.X * ArrowWidth);
		Vector2 side2 = new Vector2(dir.Y * ArrowWidth, -dir.X * ArrowWidth);

		ArmatureDisplay.DrawLine(from, toOffset, color, ArrowThickness);
		ArmatureDisplay.DrawLine(toOffset, side1 + Length + toOffset, color, ArrowThickness);
		ArmatureDisplay.DrawLine(toOffset, side2 + Length + toOffset, color, ArrowThickness);
	}

	private Vector2 ClampVelocity(Vector2 position, Vector2 velocity)
	{
		return (position + velocity).Clamp(Vector2.Zero, ArmatureDisplay.Size) - position;
	}
}
