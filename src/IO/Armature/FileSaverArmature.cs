using System.IO;
using Newtonsoft.Json;

public class FileSaverArmature : FilePath
{
    public FileSaverArmature(string path) : base(path)
    {

    }

    protected override string CreatePath(string path)
    {
        if(!path.EndsWith(ARMATURE_EXT))
        {
            path += ARMATURE_EXT;
        }
		Logs.Print("Recording to: " + path);
        return path;
    }

    public void Save(Armature armature)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(armature, Formatting.Indented));
    }
}
