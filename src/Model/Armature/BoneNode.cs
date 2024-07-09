
public record BoneNode
{
    public int Next { get; init; }
    public int Previous { get; init; }
    public int[] Extended { get; init; }
    
    public BoneNode()
    {
        Next = -1;
        Previous = -1;
        Extended = new int[0];
    }

    public BoneNode(int next, int previous, int[] extended)
    {
        Next = next;
        Previous = previous;
        Extended = extended;
    }

    public BoneNode(BoneNode boneNode)
    {
        Next = boneNode.Next;
        Previous = boneNode.Previous;
        Extended = new int[boneNode.Extended.Length];
        for(int i = 0; i < boneNode.Extended.Length; i++)
        {
            Extended[i] = boneNode.Extended[i];
        }
    }
}
