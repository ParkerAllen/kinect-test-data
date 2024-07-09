using Godot;
using System;
using freenect;

public partial class ConvolutionVisualizationController : Control
{
	// [Export]
	// public TextureRect FilterTexture { get; protected set; }
	// // [Export]
	// // public CNNFrameController CNNFrameController { get; protected set; }
	// [Export]
	// public Node[] ConvolutionContainers { get; protected set; }

	// public FilterDisplayHelper FilterDisplayHelper { get; init; }
	// public ConvolutionController[] ConvolutionControllers { get; protected set; }

	// public FileLoaderKinectData FileLoaderKinectData { get; protected set; }
	// public FileLoaderTrainingData FileLoaderTrainingData { get; protected set; }

	// public FilterPool filterPool1 = new FilterPool(new float[][,] {
	// 	new float[,] { 
	// 		{1, 1, 1}, 
	// 		{0, 0, 0}, 
	// 		{-1, -1, -1} },
	// 	new float[,] { 
	// 		{1, 0, -1}, 
	// 		{1, 0, -1}, 
	// 		{1, 0, -1} },
	// 	new float[,] { 
	// 		{1, 1, 1}, 
	// 		{1, 1, 1}, 
	// 		{1, 1, 1} },
	// 	new float[,] { 
	// 		{-1, -1, -1}, 
	// 		{-1, -1, -1}, 
	// 		{-1, -1, -1} },
	// 	new float[,] { 
	// 		{0, 0, 0}, 
	// 		{0, 1, 0}, 
	// 		{0, 0, 0} },
	// 	new float[,] { 
	// 		{1, 0, -1}, 
	// 		{0, 1, 0}, 
	// 		{-1, 0, 1} },
	// });

	// public VideoFrameMode VideoMode { get; init; }
	// public DepthFrameMode DepthMode { get; init; }


	// public ConvolutionVisualizationController()
	// {
	// 	DepthMode = DepthFrameMode.Find(DepthFormat.Depth10Bit, Resolution.Medium);
	// 	VideoMode = VideoFrameMode.Find(VideoFormat.RGB, Resolution.Medium);
	// 	FilterDisplayHelper = new FilterDisplayHelper();
	// }

	// public override void _Ready()
	// {
	// 	InitFilters();
	// 	ConvolutionControllers = new ConvolutionController[ConvolutionContainers.Length];
	// 	for(int i = 0; i < ConvolutionControllers.Length; i++)
	// 	{
	// 		ConvolutionControllers[i] = new ConvolutionController(ConvolutionContainers[i], filterPool1, new Vector2I(DepthMode.Width, DepthMode.Height));
	// 	}
	// }

	// public void InitFilters()
	// {
	// 	FilterTexture.Texture = FilterDisplayHelper.Display(filterPool1);
	// }

	// public void SelectFrame()
	// {
	// 	// for(int i = 0; i < ConvolutionControllers.Length; i++)
	// 	// {
	// 	// 	ConvolutionControllers[i].SelectFrame(FileLoaderKinectData.LoadDepthRAW(CNNFrameController.CurrentFrame));
	// 	// }
	// }

	// public void LoadTrainingData(string path)
	// {
	// 	FileLoaderTrainingData = new FileLoaderTrainingData(path);
	// 	FileLoaderKinectData = new FileLoaderKinectData(FileLoaderTrainingData.TrainingData.KinectDataPath, VideoMode.Size, DepthMode.Size);
	// }
}
