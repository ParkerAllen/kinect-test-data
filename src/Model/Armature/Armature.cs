using System.Collections.Generic;
using Godot;

public class Armature
{
    public List<Bone> Bones { get; init; }

    public Armature ()
    {
        Bones = new List<Bone>();
    }

    public Armature (Armature armature)
    {
        Bones = new List<Bone>();
        foreach(Bone bone in armature.Bones)
        {
            Bones.Add(new Bone(bone));
        }
    }

    public Armature (Armature armature, Vector2[] positions)
    {
        Bones = new List<Bone>();
        for(int i = 0; i < armature.Bones.Count; i++)
        {
            Bones.Add(new Bone(
                armature.Bones[i].BoneNode,
                new BoneSettings(
                    armature.Bones[i].BoneSettings.Name,
                    armature.Bones[i].BoneSettings.Color,
                    positions[i]
                )
            ));
        }
    }
}
