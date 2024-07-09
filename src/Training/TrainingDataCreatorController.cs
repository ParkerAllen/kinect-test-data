using Godot;
using System;
using System.Linq;

public partial class TrainingDataCreatorController : TrainingDataController
{
    [Export]
    public Panel SavePanel{ get; private set; }
    [Export]
    public OptionButton SaveOptionButton{ get; private set; }
    [Export]
    public Button SaveLocationButton{ get; private set; }

    public override void _Process(double delta)
	{
        ArmatureEditor.UpdateMouse(ArmatureDisplay.GetLocalMousePosition());
	}

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
            case FileAction.TrainingDataSave:
            {
                FileDialog.FileMode = FileDialog.FileModeEnum.SaveFile;
                SaveLocationButton.Text = FileDialog.CurrentPath;
                SavePanel.Visible = true;
                break;
            }
            case FileAction.Load:
            {
                FileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
                SavePanel.Visible = false;
		        FileDialog.Show();
                break;
            }
            default: return;
        }
    }

    public override void FileSelected()
    {
        if (fileAction == FileAction.TrainingDataSave)
        {
            SaveLocationButton.Text = FileDialog.CurrentPath;
            return;
        }
        if (fileAction == FileAction.Load)
        {
            String ext = "." + FileDialog.CurrentPath.Split('.').Last();
            switch(ext)
            {
                case FilePath.TRANING_DATA_EXT:
                    LoadTrainingData(FileDialog.CurrentPath);
                    break;
                case FilePath.ARMATURE_EXT:
                    LoadArmature(FileDialog.CurrentPath);
                    break;
                case FilePath.KINECT_DATA_EXT:
                    LoadKinectData(FileDialog.CurrentPath);
                    break;
                default : 
		            Logs.Print("ERROR - Unkown File Extension: " + ext);
                    return;
            }
        }
    }

    public void SaveTrainingData()
    {
        Logs.Print("Saving Training data to " + FileDialog.CurrentPath + "...");
        Logs.Print("Save Type: " + SaveOptionButton.Selected + "...");
        TrainingData trainingData;
        switch (SaveOptionButton.Selected)
        {
            case 0: // ALL
                trainingData = new TrainingData(FileLoaderArmature.path, FileLoaderKinectData.path, ArmatureListManager);
                break;
            case 1: // Only Selected
                trainingData = new TrainingData(FileLoaderArmature.path, FileLoaderKinectData.path, FrameManager.Selected, ArmatureListManager);
                break;
            default: return;
        }
        Logs.Print("Saving " + trainingData.TrainingArmatures.Length + " Frames...");
        new FileSaverTrainingData(FileDialog.CurrentPath).Save(trainingData);
        SavePanel.Visible = false;
    }

    private void LoadTrainingData(String path)
    {
        FileLoaderTrainingData = new FileLoaderTrainingData(FileDialog.CurrentPath);
        LoadKinectData(FileLoaderTrainingData.TrainingData.KinectDataPath);
        FileLoaderArmature = new FileLoaderArmature(FileLoaderTrainingData.TrainingData.ArmaturePath);
        ArmatureListManager.Set(FileLoaderArmature.Armature, FileLoaderTrainingData.TrainingData.TrainingArmatures);
        DisplayFrame(0);
    }

    private void LoadArmature(String path)
    {
        FileLoaderArmature = new FileLoaderArmature(path);
        if(CurrentFrame == -1)
        {
            return;
        }
        DuplicateParentArmature();
    }

    private void LoadKinectData(String path)
    {
        FileLoaderKinectData = new FileLoaderKinectData(path, VideoMode.size, DepthMode.size);
        FrameManager.Init(FileLoaderKinectData.NumOfFrames);
        ArmatureListManager = new ArmatureListManager(FileLoaderKinectData.NumOfFrames);
        DisplayFrame(0);
    }

    public void DuplicateParentArmature()
    {
        ArmatureListManager.Set(FileLoaderArmature.Armature);
        ArmatureManager.Load(ArmatureListManager.Get(CurrentFrame));
        FrameManager.FrameEdited(CurrentFrame);
    }

    public void DuplicatePreviousArmature()
    {
        ArmatureListManager.Copy(CurrentFrame - 1, CurrentFrame);
        ArmatureManager.Load(ArmatureListManager.Get(CurrentFrame));
        FrameManager.FrameEdited(CurrentFrame);
    }

    public void ClearFrameArmature()
    {
        ArmatureListManager.Remove(CurrentFrame);
        FrameManager.FrameEdited(CurrentFrame, false);
    }

    private void ToggleSavePanel()
    {
        SavePanel.Visible = !SavePanel.Visible;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if(!eventKey.Pressed)
            {
                return;
            }
            switch(eventKey.Keycode)
            {
                case Key.Up:
                    if(CurrentFrame > 1) 
                    {
                        DisplayFrame(CurrentFrame - 1);
                    }
                    break;
                case Key.Down:
                    if(CurrentFrame < FileLoaderKinectData.NumOfFrames - 1) 
                    {
                        DisplayFrame(CurrentFrame + 1);
                    }
                    break;
            }
        }
    }
}
