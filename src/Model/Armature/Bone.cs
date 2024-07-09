
public record Bone
{
    public BoneNode BoneNode { get; init; }
    public BoneSettings BoneSettings { get; init; }

    public Bone()
    {
        BoneNode = new BoneNode();
        BoneSettings = new BoneSettings();
    }

    public Bone(BoneNode boneNode, BoneSettings boneSettings)
    {
        BoneNode = boneNode;
        BoneSettings = boneSettings;
    }

    public Bone(Bone bone)
    {
        BoneNode = new BoneNode(bone.BoneNode);
        BoneSettings = new BoneSettings(bone.BoneSettings);
    }
}
