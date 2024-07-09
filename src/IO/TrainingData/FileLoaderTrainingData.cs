using System.IO;
using Newtonsoft.Json;

public partial class FileLoaderTrainingData : FilePath
{
    public TrainingData TrainingData { get; init; }
    
    public FileLoaderTrainingData(string path) : base(path)
    {
        TrainingData = Load();
    }

    protected override string CreatePath(string path)
    {
        string ext = Path.GetExtension(path);
        if(FilePath.TRANING_DATA_EXT.Equals(ext))
        {
            Logs.Print("Loading from: " + path);
            return path;
        }
        throw new InvalidExtensionException(BuildExceptionMessage(FilePath.TRANING_DATA_EXT, ext));
    }

    private TrainingData Load()
    {
        string json = "";
        using (StreamReader r = new StreamReader(path))
        {
            json = r.ReadToEnd();
        }
        return JsonConvert.DeserializeObject<TrainingData>(json);
    }
}
