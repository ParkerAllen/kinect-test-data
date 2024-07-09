using Godot;

public record BoneSettings
{
    public string Name { get; init; }
    public Color Color { get; init; }
    public Vector2 Position { get; init; }

    public BoneSettings()
    {
        Name = "Root";
        Color = Colors.White;
        Position = new Vector2(1, 1);
    }

    public BoneSettings(string name, Color color, Vector2 position)
    {
        Name = name;
        Color = color;
        Position = position;
    }

    public BoneSettings(BoneSettings bonesettings)
    {
        Name = bonesettings.Name;
        Color = bonesettings.Color;
        Position = bonesettings.Position;
    }
}
