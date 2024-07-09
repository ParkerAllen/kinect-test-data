using System.IO;
using Newtonsoft.Json;

public class FileLoaderArmature : FilePath 
{
    public Armature Armature { get; init; }

    public FileLoaderArmature(string path) : base(path)
    {
        Armature = Load();
    }

    protected override string CreatePath(string path)
    {
        string ext = Path.GetExtension(path);
        if(FilePath.ARMATURE_EXT.Equals(ext))
        {
            Logs.Print("Loading from: " + path);
            return path;
        }
        throw new InvalidExtensionException(BuildExceptionMessage(FilePath.ARMATURE_EXT, ext));
    }

    private Armature Load()
    {
        string json = "";
        using (StreamReader r = new StreamReader(path))
        {
            json = r.ReadToEnd();
        }
        return JsonConvert.DeserializeObject<Armature>(json);
    }
}
