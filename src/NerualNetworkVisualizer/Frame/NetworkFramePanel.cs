using Godot;

public partial class NetworkFramePanel : Button
{
	private NetworkFramePoolManager frameManager;
	private int index;

	public NetworkFramePanel()
	{
	}

	public void Init(NetworkFramePoolManager frameManager, int index)
	{
		this.frameManager = frameManager;
		this.index = index;
		Text = "" + index;
	}

	public void OnPressUp()
	{
		frameManager.SelectFrame(index);
	}
}