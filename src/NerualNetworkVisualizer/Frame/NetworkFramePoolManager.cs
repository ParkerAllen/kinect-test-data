using Godot;

public partial class NetworkFramePoolManager : PoolManager<NetworkFramePanel>
{
    private readonly PackedScene frameButtonPrefab = ResourceLoader.Load("res://Scenes/Prefabs/CNNFramePanel.tscn") as PackedScene;
    private NetworkFrameController NetworkFrameController { get; init; }

    public NetworkFramePoolManager(NetworkFrameController networkFrameController, Node container) : base(container)
    {
        NetworkFrameController = networkFrameController;
    }

    public override void InitElement(int index)
    {
        Pool[index].Init(this, index);
    }

    public override void SelectFrame(int index)
    {
        NetworkFrameController.DisplayFrame(index);
    }

    protected override PackedScene Prefab()
    {
        return frameButtonPrefab;
    }
}