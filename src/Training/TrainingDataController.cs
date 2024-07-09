using Godot;
using freenect;

public abstract partial class TrainingDataController : Node
{
    [Export]
	public FileDialog FileDialog { get; protected set; }
	// [Export]
	// public Panel ArmatureDisplayPanel { get; protected set; }
	[Export]
	public Button ArmatureDisplay { get; protected set; }
    [Export]
	public TextureRect VideoDisplayTexture { get; protected set; }
	[Export]
	public TextureRect DepthDisplayTexture { get; protected set; }
    [Export]
	public Label FrameLabel { get; protected set; }
    [Export]
    public Slider DepthOpacitySlider { get; protected set; }
    [Export]
    public Node FrameContainer{ get; protected set; }

    public ArmatureManager ArmatureManager { get; protected set; }
    public ArmatureListManager ArmatureListManager { get; protected set; }
    public ArmatureEditor ArmatureEditor { get; protected set; }
    public TrainingFrameManager FrameManager { get; protected set; }
    public KinectTextureDisplay VideoTextureDisplay { get; protected set; }
    public KinectTextureDisplay DepthTextureDisplay { get; protected set; }

    public FileLoaderArmature FileLoaderArmature { get; protected set; }
    public FileLoaderKinectData FileLoaderKinectData { get; protected set; }
    public FileLoaderTrainingData FileLoaderTrainingData { get; protected set; }

    public int CurrentFrame { get; protected set; }
    protected int frameSize = 0;

	public KinectFrameMode VideoMode { get; init; }
    public KinectFrameMode DepthMode { get; init; }


    protected FileAction fileAction = FileAction.None;
    protected enum FileAction
    {
        Load,
        TrainingDataSave,
        None
    }

    protected TrainingDataController()
    {
		DepthMode = KinectImageFormat.DEPTH_MODE;
		VideoMode = KinectImageFormat.VIDEO_MODE;
    }
    
    public override void _Ready()
	{
        CurrentFrame = -1;
        FrameManager = new TrainingFrameManager(this, FrameContainer);
        ArmatureManager = new ArmatureManager();
        VideoTextureDisplay = new KinectTextureDisplay(VideoDisplayTexture, new Vector2I(VideoMode.width, VideoMode.height), KinectImageFormat.VIDEO_FORMAT);
        DepthTextureDisplay = new KinectTextureDisplay(DepthDisplayTexture, new Vector2I(DepthMode.width, DepthMode.height), KinectImageFormat.DEPTH_FORMAT);

        ArmatureEditor = new ArmatureEditor(ArmatureManager, ArmatureDisplay);

        FileDialog.CurrentPath = "/home/parkerallen/Documents/Godot/KinectLinux/KData/";
	}

    public abstract void DisplayFrame(int index);
    protected abstract void DisplayDepth(float v = 0);
    protected abstract void SelectFile(FileAction fileAction);
    public abstract void FileSelected();
}
