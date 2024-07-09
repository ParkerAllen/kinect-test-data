using Godot;

public record TrainingArmature
{
    public int Index { get; init; }
    public Vector2[] Positions { get; init; }

    public TrainingArmature()
    {
        Index = -1;
        Positions = new Vector2[0];
    }

    public TrainingArmature(int index, Armature armature)
    {
        Index = index;
        Positions = new Vector2[armature.Bones.Count];
        for (int i = 0; i < Positions.Length; i++)
        {
            Positions[i] = armature.Bones[i].BoneSettings.Position;
        }
    }

}
