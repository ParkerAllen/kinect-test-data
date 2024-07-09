using System;

public abstract class FilePath
{
	public const String TRANING_DATA_EXT = ".tdat";
	public const String KINECT_DATA_EXT = ".kdat";
	public const String ARMATURE_EXT = ".arm";
    
    public readonly string path;

    public FilePath(string path)
    {
        this.path = CreatePath(path);
    }

    protected abstract string CreatePath(string path);

    protected string BuildExceptionMessage(string expected, string actual)
    {
        return "Expecting File Extension: " + expected + " Actual: " + actual;
    }
}
