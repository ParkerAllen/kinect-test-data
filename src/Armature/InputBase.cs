using Godot;
using System;

public abstract class InputBase<T, V> where T: InputPanelBase<V>
{

    public readonly Action UpdateEvent;
    public T Element { get; protected set; }
    public V Input
    {
        get { return Element.Value; }
        set { Element.Display(value); }
    }

    protected InputBase(Action updateEvent)
    {
        UpdateEvent = updateEvent;
    }

    public void OnUpdate()
    {
        UpdateEvent?.Invoke();
    }

    public void Instance(Node parent, V value)
    {
        Element = Prefab().Instantiate() as T;
        Init();
        Element.Display(value);
        parent.AddChild(Element);
    }

    public void Free()
    {
        Element.QueueFree();
        Element = null;
    }

    protected abstract PackedScene Prefab();
    protected abstract void Init();
}
