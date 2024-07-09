using Godot;
using freenect;

public partial class NetworkVisualizerController : Control
{
	[Export]
	public Node FrameMangerContainer { get; private set; }
	[Export]
	public Node NetworkContainer { get; private set; }

	// public VisualizerConvolutionManager VisualizerConvolutionManager { get; init; }

	[Export]
	public NetworkFrameController NetworkFrameController { get; private set; }

	public VideoFrameMode VideoMode { get; init; }
	public DepthFrameMode DepthMode { get; init; }

	public FileLoaderKinectData FileLoaderKinectData { get; protected set; }
	public FileLoaderTrainingData FileLoaderTrainingData { get; protected set; }

	public NetworkVisualizerController()
	{
		DepthMode = DepthFrameMode.Find(DepthFormat.Depth11Bit, Resolution.Medium);
		VideoMode = VideoFrameMode.Find(VideoFormat.RGB, Resolution.Medium);

		// VisualizerConvolutionManager = new VisualizerConvolutionManager(this);
	}

	public override void _Ready()
	{
		NetworkFrameController.Init(this);
	}

	public void LoadTrainingData(string path)
	{
		FileLoaderTrainingData = new FileLoaderTrainingData(path);
		FileLoaderKinectData = new FileLoaderKinectData(FileLoaderTrainingData.TrainingData.KinectDataPath, VideoMode.Size, DepthMode.Size);
	}

	public void SelectFrame()
	{
		
	}
}