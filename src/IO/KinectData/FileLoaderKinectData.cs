using System.IO;

public class FileLoaderKinectData : FilePath
{
    public int FrameSize { get; init; }
    public int VideoSize { get; init; }
    public int DepthSize { get; init; }
    public int NumOfFrames { get; init; }

    public FileLoaderKinectData(string path, int videoSize, int depthSize) : base(path)
    {
        VideoSize = videoSize;
        DepthSize = depthSize;
        FrameSize = videoSize + depthSize;
        using(FileStream source = File.OpenRead(path))
        {
            NumOfFrames = (int)(source.Length / FrameSize);
        }
        Logs.Print("Number of Frames: " + NumOfFrames);
    }

    protected override string CreatePath(string path)
    {
        string ext = Path.GetExtension(path);
        if(FilePath.KINECT_DATA_EXT.Equals(ext))
        {
            Logs.Print("Loading from: " + path);
            return path;
        }
        throw new InvalidExtensionException(BuildExceptionMessage(FilePath.KINECT_DATA_EXT, ext));
    }

    public byte[] LoadVideo(int frameIndex)
    {
        byte[] buffer = new byte[VideoSize];
        using(Stream source = File.OpenRead(path))
        {
            source.Seek(frameIndex * FrameSize, SeekOrigin.Begin);
            source.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }

    public byte[] LoadDepthRAW(int frameIndex)
    {
        byte[] buffer = new byte[DepthSize];
        using(Stream source = File.OpenRead(path))
        {
            source.Seek(frameIndex * FrameSize + VideoSize, SeekOrigin.Begin);
            source.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
    
    public byte[] LoadDepth(int frameIndex, byte alpha = 255)
    {
        return KinectImageFormat.Instance.DepthToImage(LoadDepthRAW(frameIndex), alpha);
    }

    public byte[] LoadDepth(byte[] data, byte alpha = 255)
    {
        return KinectImageFormat.Instance.DepthToImage(data, alpha);
    }
}
