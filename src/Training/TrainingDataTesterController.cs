using Godot;

public partial class TrainerController : TrainingDataController
{

    public override void DisplayFrame(int index)
    {
        CurrentFrame = index;
        FrameLabel.Text = "Frame: " + CurrentFrame;
        VideoTextureDisplay.UpdateTexture(FileLoaderKinectData.LoadVideo(CurrentFrame));
        DepthTextureDisplay.UpdateTexture(FileLoaderKinectData.LoadDepth(CurrentFrame, (byte) DepthOpacitySlider.Value));
        ArmatureManager.Load(ArmatureListManager.Get(CurrentFrame));
    }

    protected override void DisplayDepth(float v = 0)
    {
        DepthTextureDisplay.UpdateTexture(FileLoaderKinectData.LoadDepth(CurrentFrame, (byte) DepthOpacitySlider.Value));
    }

    protected override void SelectFile(FileAction fileAction)
    {
        this.fileAction = fileAction;
        switch (fileAction)
        {
            case FileAction.Load:
            {
                FileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
                break;
            }
            default: return;
        }
		FileDialog.Show();
    }

    public override void FileSelected()
    {
        switch (fileAction)
        {
            case FileAction.Load:
            {
                break;
            }
            default: return;
        }
    }
}
