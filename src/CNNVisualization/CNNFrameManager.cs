using Godot;
using System;

public partial class CNNFrameManager : Node
{
    // [Export]
    // public ConvolutionVisualizationController Controller { get; protected set; }
    // [Export]
	// public FileDialog FileDialog { get; protected set; }
    // // [Export]
	// // public TextureRect VideoDisplayTexture { get; protected set; }
	// [Export]
	// public TextureRect DepthDisplayTexture { get; protected set; }
    // [Export]
	// public Label FrameLabel { get; protected set; }
    // [Export]
    // public Node FrameContainer { get; protected set; }

    // // public KinectTextureDisplay VideoTextureDisplay { get; protected set; }
    // public KinectTextureDisplay DepthTextureDisplay { get; protected set; }
    // public CNNFrameManager FrameManager { get; protected set; }


    // public int CurrentFrame { get; protected set; }
    // protected int frameSize = 0;
    // protected int videoSize = 0;
	// public readonly Image.Format VideoImageFormat = Image.Format.Rgb8;
	// public readonly Image.Format DepthImageFormat = Image.Format.La8;

    // protected FileAction fileAction = FileAction.None;
    // protected enum FileAction
    // {
    //     ArmatureLoad,
    //     TrainingDataSave,
    //     TrainingDataLoad,
    //     KinectDataLoad,
    //     None
    // }

    // public override void _Ready()
    // {
    //     CurrentFrame = -1;
    //     FrameManager = new CNNFrameManager(this, FrameContainer);

    //     // VideoTextureDisplay = new KinectTextureDisplay(VideoDisplayTexture, new Vector2I(videoMode.Width, videoMode.Height), videoMode.Size, VideoImageFormat);
    //     DepthTextureDisplay = new KinectTextureDisplay(DepthDisplayTexture, new Vector2I(Controller.DepthMode.Width, Controller.DepthMode.Height), DepthImageFormat);

    //     FileDialog.CurrentPath = "/home/parkerallen/Documents/Godot/KinectLinux/KData/";
    // }
	
    // public void DisplayFrame(int index)
    // {
    //     CurrentFrame = Controller.FileLoaderTrainingData.TrainingData.TrainingArmatures[index].Index;
    //     Logs.Print("Current frame: " + CurrentFrame);
    //     FrameLabel.Text = "Frame: " + CurrentFrame;
    //     // VideoTextureDisplay.UpdateTexture(FileLoaderKinectData.LoadVideo(CurrentFrame));
    //     DepthTextureDisplay.UpdateTexture(Controller.FileLoaderKinectData.LoadDepth_LA8(CurrentFrame, 255));
    //     Controller.SelectFrame();
    // }

	// protected void SelectFile(FileAction fileAction)
    // {
    //     this.fileAction = fileAction;
    //     switch (fileAction)
    //     {
    //         case FileAction.TrainingDataLoad:
    //         {
    //             FileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
	// 	        FileDialog.Show();
    //             break;
    //         }
    //         default: return;
    //     }
    // }

    // public void FileSelected()
    // {
    //     switch (fileAction)
    //     {
    //         case FileAction.TrainingDataLoad:
    //         {
    //             LoadTrainingData();
    //             break;
    //         }
    //         default: return;
    //     }
    // }

    // private void LoadTrainingData()
    // {
    //     Controller.LoadTrainingData(FileDialog.CurrentPath);
    //     FrameManager.Init(Controller.FileLoaderTrainingData.TrainingData.TrainingArmatures.Length);
    //     DisplayFrame(0);
    // }
}
