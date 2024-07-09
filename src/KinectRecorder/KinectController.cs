using System;
using System.Threading;
using freenect;

public partial class KinectController 
{
	private static readonly Lazy<KinectController> lazy =
        new Lazy<KinectController>(() => new KinectController());

    public static KinectController Instance { get { return lazy.Value; } }

	public Kinect kinect { get; private set; }
	public readonly KinectVideo KinectVideo = new KinectVideo();
	public readonly KinectDepth KinectDepth = new KinectDepth();
	public bool IsRunning { get; private set; }
	Thread updateThread = null;

    private KinectController()
    {
    }

	public void Connect()
	{
		if(IsRunning)
		{
			Disconnect();
		}
		Logs.Print("Opening Kinect...");
		IsRunning = true;
		kinect = new Kinect(0);
		kinect.Open();
		kinect.LED.Color = LEDColor.Yellow;

		kinect.VideoCamera.DataReceived += HandleKinectVideoCameraDataReceived;
		kinect.VideoCamera.Mode = kinect.VideoCamera.Modes[0];
		KinectVideo.FrameMode = kinect.VideoCamera.Mode;
		kinect.VideoCamera.Start();

		kinect.DepthCamera.DataReceived += HandleKinectDepthCameraDataReceived;
		kinect.DepthCamera.Mode = kinect.DepthCamera.Modes[0];
		KinectDepth.FrameMode = kinect.DepthCamera.Mode;
		kinect.DepthCamera.Start();

		kinect.LED.Color = LEDColor.Green;
		// kinect.Motor.Tilt = .25;

		updateThread = new Thread(Update);

		updateThread.Start();
	}

	public void Disconnect()
	{
		if(!IsRunning)
		{
			return;
		}
		kinect.LED.Color = LEDColor.Yellow;
		Logs.Print("Closing Kinect...");

		IsRunning = false;
		updateThread.Interrupt();
		updateThread.Join();
		updateThread = null;

		kinect.LED.Color = LEDColor.Red;
		kinect.Close();
		kinect = null;
	}

	public void Update()
	{
		while(IsRunning)
		{
			try
			{
				kinect.UpdateStatus();

				Kinect.ProcessEvents();
			}
			catch(ThreadInterruptedException e)
			{
				Logs.Print(e.ToString());
				return;
			}
			catch(Exception ex)
			{
				Logs.Print(ex.ToString());
			}
		}
	}

	private void HandleKinectVideoCameraDataReceived (object sender, VideoCamera.DataReceivedEventArgs e)
	{
		KinectVideo.HandleVideoBufferUpdate();
		kinect.VideoCamera.DataBuffer = KinectVideo.VideoBackBuffer;
	}

	private void HandleKinectDepthCameraDataReceived (object sender, DepthCamera.DataReceivedEventArgs e)
	{
		KinectDepth.HandleDepthBufferUpdate();
		kinect.DepthCamera.DataBuffer = KinectDepth.DepthBackBuffer;
	}

	public void ShutDown()
	{
		if(IsRunning)
		{
			Disconnect();
		}
		else if(kinect == null)
		{
			kinect = new Kinect(0);
			kinect.Open();
			kinect.LED.Color = LEDColor.Yellow;
		}
		kinect.LED.Color = LEDColor.Red;
		kinect.Close();
		kinect = null;
		Kinect.Shutdown();
	}

	public void Tilt(float value)
	{
		if(!IsRunning) {
			return;
		}
		kinect.Motor.Tilt = value;
	}
}
