using Godot;
using System;

public partial class TrainerSettings : Resource
{
    public String SaveLocation { get; private set; }

    private static readonly String PATH = "user://trainersettings.tres";

    public TrainerSettings() : this(null) {}

    public TrainerSettings(String saveLocation)
    {
        SaveLocation = saveLocation;
    }

    public void SaveSettings()
    {
		ResourceSaver.Save(this, PATH);
    }

    public static TrainerSettings LoadSettings()
    {
        if(ResourceLoader.Exists(TrainerSettings.PATH))
        {
            return (TrainerSettings)ResourceLoader.Load(TrainerSettings.PATH);
        }
        return new TrainerSettings();
    }
}
