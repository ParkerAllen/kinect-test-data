using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

public class FileSaverKinectData : FilePath
{

	private readonly List<byte[]> videoRecordingBuffer;
	private readonly List<byte[]> depthRecordingBuffer;
    private readonly CancellationTokenSource cancelToken;
	private readonly Thread recordingthread;

    public FileSaverKinectData(string path) : base(path)
    {
	    videoRecordingBuffer = new List<byte[]>();
	    depthRecordingBuffer = new List<byte[]>();

        cancelToken = new();
		recordingthread = new Thread(
            () => ProcessRecordingBuffer(cancelToken)
        );
		recordingthread.Start();
    }

    protected override string CreatePath(string path)
    {
		Logs.Print("Recording to: " + path);
        return path + "/data" + Logs.GetDateTime() + KINECT_DATA_EXT;
    }

    public void RecordFrame(byte[] videoFrame, byte[] depthFrame)
    {
        videoRecordingBuffer.Add(videoFrame);
        depthRecordingBuffer.Add(depthFrame);
    }

    public void StopRecording()
    {
        cancelToken.Cancel();
        recordingthread.Join();
        cancelToken.Dispose();
    }

	private void ProcessRecordingBuffer(CancellationTokenSource token)
	{
		int numFrames = 0;
		while(!token.IsCancellationRequested)
		{
			// Logs.Print("Processing Video Recording Buffer...");
			int count = Math.Min(videoRecordingBuffer.Count, depthRecordingBuffer.Count);
            if(count == 0)
            {
                continue;
            }
			numFrames += count;

			List<byte[]> videoBuffer = new List<byte[]>();
			List<byte[]> depthBuffer = new List<byte[]>();

            for(int i = 0; i < count; i++)
            {
                videoBuffer.Add(videoRecordingBuffer[0]);
                depthBuffer.Add(depthRecordingBuffer[0]);
                videoRecordingBuffer.RemoveAt(0);
                depthRecordingBuffer.RemoveAt(0);
            }

			using (var stream = new FileStream(path, FileMode.Append))
			{
				for(int i = 0; i < count; i++)
				{
					stream.Write(videoBuffer[i], 0, videoBuffer[i].Length);
					stream.Write(depthBuffer[i], 0, depthBuffer[i].Length);
				}
			}
		}
		Logs.Print(numFrames + " Frames Recorded!!!");
	}
}
