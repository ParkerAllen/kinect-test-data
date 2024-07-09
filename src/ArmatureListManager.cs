using System.Collections.Generic;

public class ArmatureListManager
{
    private Armature[] Armatures { get; set; }
    private int current;

    public ArmatureListManager(int numFrames)
    {
        Armatures = new Armature[numFrames];
    }

    public ArmatureListManager(Armature armature, TrainingArmature[] data)
    {
        Armatures = new Armature[data.Length];
        for(int i = 0; i < data.Length; i++)
        {
            Armatures[i] = new Armature(armature, data[i].Positions);
        }
    }

    public Armature Get(int index)
    {
        current = index;
        if(Armatures[index] == null)
        {
            return new Armature();
        }
        return Armatures[index];
    }

    public List<int> GetAll()
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i <  Armatures.Length; i++)
        {
            if (IsNotEmpty(i))
            {
                indexList.Add(i);
            }
        }
        return indexList;
    }

    public void Set(Armature armature)
    {
        Armatures[current] = new Armature(armature);
    }

    public void Set(Armature armature, TrainingArmature[] data)
    {
        for(int i = 0; i < data.Length; i++)
        {
            Armatures[data[i].Index] = new Armature(armature, data[i].Positions);
        }
    }

    public bool IsNotEmpty(int index)
    {
        return Armatures[index] != null;
    }

    public void Copy(int from, int to)
    {
        if(from < 0 || from >= Armatures.Length)
        {
            return;
        }
        Armatures[to] = new Armature(Armatures[from]);
    }

    public void Remove(int index)
    {
        Armatures[index] = null;
    }
}
