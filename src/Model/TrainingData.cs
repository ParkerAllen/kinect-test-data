using System.Collections.Generic;

public record TrainingData
{
    public string ArmaturePath { get; set; }
    public string KinectDataPath { get; set; }
    public TrainingArmature[] TrainingArmatures { get; init; }

    public TrainingData()
    {
        ArmaturePath = "";
        KinectDataPath = "";
        TrainingArmatures = new TrainingArmature[0];
    }

    public TrainingData(string armaturePath, string kinectDataPath, TrainingArmature[] trainingArmatures)
    {
        ArmaturePath = armaturePath;
        KinectDataPath = kinectDataPath;
        TrainingArmatures = trainingArmatures;
    }

    public TrainingData(string armaturePath, string kinectDataPath, ArmatureListManager armatureListManager)
    {
        ArmaturePath = armaturePath;
        KinectDataPath = kinectDataPath;
        List<int> armatureList = armatureListManager.GetAll();
        TrainingArmatures = new TrainingArmature[armatureList.Count];
        int i = 0;
        foreach (int index in armatureList)
        {
            TrainingArmatures[i] = new TrainingArmature(index, armatureListManager.Get(index));
            i++;
        }
    }

    public TrainingData(string armaturePath, string kinectDataPath, HashSet<int> selected, ArmatureListManager armatureListManager)
    {
        ArmaturePath = armaturePath;
        KinectDataPath = kinectDataPath;
        selected.RemoveWhere(armatureListManager.IsNotEmpty);
        TrainingArmatures = new TrainingArmature[selected.Count];
        int i = 0;
        foreach (int index in selected)
        {
            TrainingArmatures[i] = new TrainingArmature(index, armatureListManager.Get(index));
            i++;
        }
    }
}
