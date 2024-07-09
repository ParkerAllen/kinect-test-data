using Godot;
using System;

public partial class RecorderSettings : Resource
{
    [Export]
	public int CountDownTimer { get; set; }
    [Export]
	public int RecordDuration { get; set; }
    [Export]
	public int FPSCap { get; set; }
    [Export]
	public String SaveLocation { get; set; }

    private static readonly String PATH = "user://recordersettings.tres";

    public RecorderSettings() : this(3, 60, 30, null) {}

    public RecorderSettings(int countDownTimer, int recordDuration, int fpsCap, String saveLocation)
    {
        CountDownTimer = countDownTimer;
        RecordDuration = recordDuration;
        FPSCap = fpsCap;
        SaveLocation = saveLocation;
    }

    public void SaveSettings()
    {
		ResourceSaver.Save(this, PATH);
    }

    public static RecorderSettings LoadSettings()
    {
        if(ResourceLoader.Exists(RecorderSettings.PATH))
        {
            return (RecorderSettings)ResourceLoader.Load(RecorderSettings.PATH);
        }
        return new RecorderSettings();
    }
}
