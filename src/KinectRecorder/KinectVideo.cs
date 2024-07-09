using Godot;
using System;
using freenect;

public partial class KinectVideo
{
	private SwapBufferCollection<byte> dataBuffer;
	public int FPSCounter { get; private set; }
	public bool DataPending { get; private set; }
	private Image cachedImage;
	public ImageTexture CachedImageTexture { get; private set; }
	public VideoFrameMode FrameMode
	{
		get { return FrameMode; }
		set {
			if(value == FrameMode)
			{
				return;
			}
			FrameMode = value;


		dataBuffer = new SwapBufferCollection<byte>(2, FrameMode.Size);
		cachedImage = Image.Create(FrameMode.Width, FrameMode.Height, false, KinectImageFormat.VIDEO_FORMAT);
		CachedImageTexture = ImageTexture.CreateFromImage(cachedImage);
		}
	}

	public void HandleVideoBufferUpdate()
	{
		dataBuffer.Swap(0,1);
		FPSCounter++;
		DataPending = true;
	}

	public void ResetFPSCounter()
	{
		FPSCounter = 0;
	}

	public IntPtr VideoBackBuffer
	{	
		get
		{
			return this.dataBuffer.GetHandle(1);
		}
	}

	public void Render()
	{
		if(!DataPending)
		{
			return;
		}
		cachedImage.SetData(FrameMode.Width, FrameMode.Height, false, KinectImageFormat.VIDEO_FORMAT, KinectImageFormat.Instance.CalibrateRGB(dataBuffer[0]));
		CachedImageTexture.Update(cachedImage);

		DataPending = false;
	}

	public byte[] GetMid()
	{
		return dataBuffer[0];
	}
}
