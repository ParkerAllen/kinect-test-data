using Godot;

public abstract partial class InputPanelBase<V> : PanelContainer
{
    public V Value { get; protected set; }
    public abstract void Display(V value);
}
