using Godot;

public partial class NetworkFrameController : Node
{
    [Export]
	public FileDialog FileDialog { get; private set; }
    [Export]
    public Node FrameContainer { get; private set; }
	[Export]
	public TextureRect DepthTexture { get; private set; }
    [Export]
	public Label FrameLabel { get; private set; }

    public NetworkVisualizerController Controller { get; private set; }
    public KinectTextureDisplay DepthTextureDisplay { get; private set; }
    public NetworkFramePoolManager NetworkFramePoolManager { get; private set; }

    public int CurrentFrame { get; private set; }

    protected FileAction fileAction = FileAction.None;
    protected enum FileAction
    {
        ArmatureLoad,
        TrainingDataSave,
        TrainingDataLoad,
        KinectDataLoad,
        None
    }

    public void Init(NetworkVisualizerController controller)
    {
        Controller = controller;
        CurrentFrame = -1;
        NetworkFramePoolManager = new NetworkFramePoolManager(this, FrameContainer);

        // VideoTextureDisplay = new KinectTextureDisplay(VideoDisplayTexture, new Vector2I(videoMode.Width, videoMode.Height), videoMode.Size, VideoImageFormat);
        DepthTextureDisplay = new KinectTextureDisplay(DepthTexture, new Vector2I(Controller.DepthMode.Width, Controller.DepthMode.Height), KinectImageFormat.DEPTH_FORMAT);

        FileDialog.CurrentPath = "/home/parkerallen/Documents/Godot/KinectLinux/KData/";

    }
	
    public void DisplayFrame(int index)
    {
        CurrentFrame = Controller.FileLoaderTrainingData.TrainingData.TrainingArmatures[index].Index;
        Logs.Print("Current frame: " + CurrentFrame);
        FrameLabel.Text = "Frame: " + CurrentFrame;
        // VideoTextureDisplay.UpdateTexture(FileLoaderKinectData.LoadVideo(CurrentFrame));
        DepthTextureDisplay.UpdateTexture(Controller.FileLoaderKinectData.LoadDepth(CurrentFrame, 255));
        Controller.SelectFrame();
    }

	protected void SelectFile(FileAction fileAction)
    {
        this.fileAction = fileAction;
        switch (fileAction)
        {
            case FileAction.TrainingDataLoad:
            {
                FileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
		        FileDialog.Show();
                break;
            }
            default: return;
        }
    }

    public void FileSelected()
    {
        switch (fileAction)
        {
            case FileAction.TrainingDataLoad:
            {
                LoadTrainingData();
                break;
            }
            default: return;
        }
    }

    private void LoadTrainingData()
    {
        Controller.LoadTrainingData(FileDialog.CurrentPath);
        NetworkFramePoolManager.Init(Controller.FileLoaderTrainingData.TrainingData.TrainingArmatures.Length);
        DisplayFrame(0);
    }
}