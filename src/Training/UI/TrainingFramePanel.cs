using Godot;

public partial class TrainingFramePanel : Panel
{
	[Export]
	public Label FrameIndex { get; private set; }
	[Export]
	public Button Button { get; private set; }
	[Export]
	public Label EditedLabel { get; private set; }

	private TrainingFrameManager frameManager;
	private int index;
	private StyleBoxFlat Normal { get; init; }
	private StyleBoxFlat Pressed { get; init; }

	public TrainingFramePanel()
	{
		Normal = new StyleBoxFlat();
		Normal.BgColor = new Color(0.14f, 0.14f, 0.15f);
		Pressed = new StyleBoxFlat();
		Pressed.BgColor = new Color(0.34f, 0.34f, 0.42f);
	}

	public void Init(TrainingFrameManager frameManager, int index, bool canDisplayFrame)
	{
		this.frameManager = frameManager;
		this.index = index;
		EditedLabel.Hide();
		if(canDisplayFrame)
		{
			FrameIndex.Text = "" + index;
		}
		else
		{
			FrameIndex.Hide();
		}
	}

	public void OnPressUp()
	{
		Highlight();
		frameManager.SelectFrame(index);
	}

	public void Highlight(bool isSelected = true)
	{
		if(isSelected)
		{
			Button.AddThemeStyleboxOverride("normal", Pressed);
			return;
		}
		Button.AddThemeStyleboxOverride("normal", Normal);
	}

	public void IsEdited(bool isEdited)
	{
		EditedLabel.Visible = isEdited;
	}
}
