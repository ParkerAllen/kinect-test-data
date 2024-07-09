using System.IO;
using Newtonsoft.Json;

public class FileSaverTrainingData : FilePath
{
    public FileSaverTrainingData(string path) : base(path)
    {

    }

    protected override string CreatePath(string path)
    {
        if(!path.EndsWith(TRANING_DATA_EXT))
        {
            path += TRANING_DATA_EXT;
        }
		Logs.Print("Recording to: " + path);
        return path;
    }

    public void Save(TrainingData trainingData)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(trainingData, Formatting.Indented));
    }
}
