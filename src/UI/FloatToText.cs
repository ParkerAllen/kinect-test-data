using Godot;

public partial class FloatToText : Label
{
	public void SetText(float value)
	{
		Text = (int) value + "";
	}
}