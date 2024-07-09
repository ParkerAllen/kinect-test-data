using Godot;

public partial class ConLayerController : Control
{

	public LayerVisualizerManager LayerVisualizerManager { get; private set; }
	public int LayerIndex { get; private set; }
	
	public override void _Ready()
	{
	}

	public void Init(LayerVisualizerManager layerVisualizerManager, PixelData[][,] textures)
	{
		LayerVisualizerManager = layerVisualizerManager;
		for (int i = 0; i < textures.Length; i++)
		{
			TextureButton textureButton = new TextureButton();
			AddChild(textureButton);
			textureButton.ButtonUp += () => LayerVisualizerManager.SelectLayer(LayerIndex, i);
		}
	}

	private void BuildTextures(TextureButton textureButton, PixelData[,] texture)
	{

	}
}
