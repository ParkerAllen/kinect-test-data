using Godot;

public partial class PathSelector : Button
{
	[Export]
	public FileDialog FileDialog { get; private set; }

	public override void _Ready()
	{
		UpdateText();
	}

	public void OpenFileDialog()
	{
		FileDialog.Show();
	}

	public void UpdateText()
	{
		Text = FileDialog.CurrentPath;
	}
}
