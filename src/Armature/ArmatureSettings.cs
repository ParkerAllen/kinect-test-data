using Godot;
using System;

public partial class ArmatureSettings : Resource
{
	public String SaveLocation { get; private set; }

    private static readonly String PATH = "user://armaturesettings.tres";

    public ArmatureSettings() : this(null) {}

    public ArmatureSettings(String saveLocation)
    {
        SaveLocation = saveLocation;
    }

    public void SaveSettings()
    {
		ResourceSaver.Save(this, PATH);
    }

    public static ArmatureSettings LoadSettings()
    {
        if(ResourceLoader.Exists(ArmatureSettings.PATH))
        {
            return (ArmatureSettings)ResourceLoader.Load(ArmatureSettings.PATH);
        }
        return new ArmatureSettings();
    }
}
