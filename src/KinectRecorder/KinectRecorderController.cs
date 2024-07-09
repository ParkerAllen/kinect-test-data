using Godot;

public partial class KinectRecorderController : Control
{
	[Export]
	public TextureRect VideoCameraDisplay { get; private set; }
	[Export]
	public TextureRect DepthCameraDisplay { get; private set; }
	[Export]
	public Label VideoFPSLabel { get; private set; }
	[Export]
	public BaseButton VideoToggle { get; private set; }
	[Export]
	public BaseButton RecordingToggle { get; private set; }
	[Export]
	public Button CameraModeToggle { get; private set; }
	[Export]
	public NumberLineEdit CountDownInput { get; private set; }
	[Export]
	public NumberLineEdit DurationInput { get; private set; }
	[Export]
	public NumberLineEdit FPSCapInput { get; private set; }
	[Export]
	public Button PathSelector { get; private set; }
	[Export]
	public Label DelayTimeLabel { get; private set; }
	[Export]
	public Label RecordingTimeLabel { get; private set; }
	[Export]
	public Timer FrameRecordTimer { get; private set; }

	private KinectVideo kinectVideo;
	private KinectDepth kinectDepth;
	private SecondCounter secondCounter;
	private FileSaverKinectData fileSaverKinectData;

	private RecorderSettings recordersettings;

	public override void _Ready()
	{
		kinectVideo = KinectController.Instance.KinectVideo;
		kinectDepth = KinectController.Instance.KinectDepth;
		secondCounter = new SecondCounter(this);
		DelayTimeLabel.Hide();
		RecordingTimeLabel.Hide();
		LoadSettings();
	}

	public override void _Process(double delta)
	{
		kinectVideo.Render();
		kinectDepth.Render();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			SaveSettings();
			ShutDown();
			GetTree().Quit();
		}
	}

	public void ToggleCamera()
	{
		if(VideoToggle.ButtonPressed || RecordingToggle.ButtonPressed)
		{
			KinectController.Instance.Connect();
			ToggleCameraMode(CameraModeToggle.ButtonPressed);
			return;
		}
		KinectController.Instance.Disconnect();
	}

	public void ToggleCameraMode(bool mode)
	{
		if(mode)
		{
			VideoCameraDisplay.Texture = kinectDepth.CachedImageTexture;
			return;
		}
		VideoCameraDisplay.Texture = kinectVideo.CachedImageTexture;
	}

	public void ToggleRecording(bool toggle)
	{
		if(toggle)
		{
			StartDelayCounter();
			return;
		}
		StopRecording();
	}

	private void UpdateFPS()
	{
		if (!KinectController.Instance.IsRunning)
		{
			return;
		}
		// VideoFPSLabel.Text = "FPS: " + kinectVideo.FPSCounter;
		VideoFPSLabel.Text = "FPS: " + kinectDepth.FPSCounter;
		// kinectVideo.ResetFPSCounter();
		kinectDepth.ResetFPSCounter();
	}

	public void StartDelayCounter()
	{
		Logs.Print("Starting Delay Counter...");
		int duration = CountDownInput.Input();
		secondCounter.OnFinish += StartRecordCounter;
		secondCounter.OnCount += UpdateDelayCounter;
		secondCounter.OnFinish += EndDelayCounter;
		secondCounter.Start(duration);
		DelayTimeLabel.Show();
		DelayTimeLabel.Text = duration + "";
		ToggleCamera();
	}

	private void UpdateDelayCounter(int remaining)
	{
		// Logs.Print(remaining + "...");
		DelayTimeLabel.Text = remaining + "";
	}

	private void EndDelayCounter()
	{
		Logs.Print("Finished Delay Counter!");
		DelayTimeLabel.Hide();
		secondCounter.OnCount -= UpdateDelayCounter;
		secondCounter.OnFinish -= EndDelayCounter;
		secondCounter.OnFinish -= StartRecordCounter;
	}

	public void StartRecordCounter()
	{
		Logs.Print("Starting Record Counter...");
		int duration = DurationInput.Input();
		secondCounter.OnCount += UpdateRecordCounter;
		secondCounter.OnFinish += EndRecordCounter;
		secondCounter.Start(duration);
		RecordingTimeLabel.Show();
		RecordingTimeLabel.Text = duration + "";
		FrameRecordTimer.WaitTime = 1.0 / FPSCapInput.Input();
		Logs.Print("Fps Cap: " + FrameRecordTimer.WaitTime);
		FrameRecordTimer.Start();
		fileSaverKinectData = new FileSaverKinectData(PathSelector.Text);
	}

	private void UpdateRecordCounter(int remaining)
	{
		// Logs.Print(remaining + "...");
		RecordingTimeLabel.Text = remaining + "";
	}
	
	public void RecordFrame()
	{
		fileSaverKinectData.RecordFrame(kinectVideo.GetMid(), kinectDepth.GetMid());
	}

	private void EndRecordCounter()
	{
		Logs.Print("Finished Record Counter!");
		RecordingTimeLabel.Hide();
		secondCounter.OnCount -= UpdateRecordCounter;
		secondCounter.OnFinish -= EndRecordCounter;
		RecordingToggle.ButtonPressed = false;
		ToggleCamera();
	}

	private void StopRecording()
	{
		Logs.Print("Stopping Recording");
		FrameRecordTimer.Stop();
		fileSaverKinectData.StopRecording();
		secondCounter.Stop();
		EndDelayCounter();
		EndRecordCounter();
	}

	private void LoadSettings()
	{
		Logs.Print("Loading Record Settings...");
		recordersettings = RecorderSettings.LoadSettings();
		CountDownInput.PlaceholderText = recordersettings.CountDownTimer + "";
		DurationInput.PlaceholderText = recordersettings.RecordDuration + "";
		FPSCapInput.PlaceholderText = recordersettings.FPSCap + "";
		PathSelector.Text = recordersettings.SaveLocation;
	}

	private void SaveSettings()
	{
		Logs.Print("Saving Record Settings...");
		recordersettings.CountDownTimer = CountDownInput.Input();
		recordersettings.RecordDuration = DurationInput.Input();
		recordersettings.FPSCap = FPSCapInput.Input();
		recordersettings.SaveLocation = PathSelector.Text;
		recordersettings.SaveSettings();
	}

	public void ShutDown()
	{
		Logs.Print("Shutting Down Kinect...");
		KinectController.Instance.ShutDown();
	}

	public void OnTiltKinect(float value) {
		KinectController.Instance.Tilt(value / 20);
	}
}
