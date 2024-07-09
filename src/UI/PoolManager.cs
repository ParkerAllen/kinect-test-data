using Godot;
using System.Collections.Generic;

public abstract class PoolManager<T> where T: Node
{
    protected List<T> Pool { get; init; }
    protected Node Container { get; init; }

    public PoolManager(Node container)
    {
        Container = container;
        Pool = new List<T>();
    }

    public void Init(int numOfFrames)
    {
        while (Pool.Count > numOfFrames)
        {
			Pool[0].QueueFree();
			Pool.RemoveAt(0);
        }
        while (Pool.Count < numOfFrames)
        {
            T element = Prefab().Instantiate() as T;
            Container.AddChild(element);
            Pool.Add(element);
        }
        for ( int i = 0; i < numOfFrames; i++)
        {
            InitElement(i);
        }
    }

    protected abstract PackedScene Prefab();
    public abstract void InitElement(int index);
    public abstract void SelectFrame(int index);
}
