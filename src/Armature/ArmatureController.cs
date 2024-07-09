using Godot;

public partial class ArmatureController : Node
{
	[Export]
	private FileDialog fileDialog;
	[Export]
	private Node BoneEditorContainer;
	[Export]
	private ColorPickerManager ColorPickerManager;
	[Export]
	public Button ArmatureDisplay { get; protected set; }

    public BoneEditor BoneEditor { get; private set; }
    public ArmatureEditor ArmatureEditor { get; private set; }
    public ArmatureManager ArmatureManager { get; private set; }

	private ArmatureSettings armaturesettings;
    private bool isLoading;

    public override void _Ready()
	{
		LoadSettings();
        ArmatureManager = new ArmatureManager();
        BoneEditor = new BoneEditor(ArmatureManager, BoneEditorContainer, ColorPickerManager);
        ArmatureEditor = new ArmatureEditor(ArmatureManager, ArmatureDisplay);
        BoneEditor.Instance();

        ArmatureManager.Add(
            new Bone(
                new BoneNode(),
                new BoneSettings("Root", Colors.Red, ArmatureDisplay.Size / 2)
            )
        );
	}
    
    public override void _Process(double delta)
	{
		ArmatureEditor.UpdateMouse(GetViewport().GetMousePosition());
	}

    public void AddBone()
    {
        ArmatureManager.Add(
            new Bone(
                new BoneNode(),
                new BoneSettings("Bone", Colors.White, ArmatureDisplay.Size / 2)
            )
        );
    }

    public void RemoveBone()
    {
        ArmatureManager.Remove();
    }

    public void SelectFile(bool isLoading)
    {
        this.isLoading = isLoading;
        if(isLoading)
        {
            fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        }
        else
        {
            fileDialog.FileMode = FileDialog.FileModeEnum.SaveFile;
        }
		fileDialog.Show();
    }

    public void FileSelected()
    {
        if(isLoading)
        {
            LoadArmature();
            return;
        }
        SaveArmature();
    }

    private void SaveArmature()
    {
        ArmatureManager.Save(fileDialog.CurrentPath);
    }

    private void LoadArmature()
    {
        ArmatureManager.Load(fileDialog.CurrentPath);
    }

    private void LoadSettings()
	{
		Logs.Print("Loading Armature Settings...");
		armaturesettings = ArmatureSettings.LoadSettings();
	}

	private void SaveSettings()
	{
		Logs.Print("Saving Armature Settings...");
		armaturesettings.SaveSettings();
	}
}
