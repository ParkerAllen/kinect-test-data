
public record KinectFrameMode (int formatSize, int width, int height, int resolution, int size)
{
    public KinectFrameMode (int formatSize, int width, int height) : 
    this(formatSize, width, height, width * height, width * height* formatSize)
    {
    }
}