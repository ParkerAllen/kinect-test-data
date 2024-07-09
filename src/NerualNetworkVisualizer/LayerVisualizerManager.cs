using System.Collections.Generic;
using Godot;

public class LayerVisualizerManager
{
    private readonly PackedScene LayerDisplayPrefab = ResourceLoader.Load("res://Scenes/Prefabs/CNNFramePanel.tscn") as PackedScene;
    
    public Node NetworkContainer { get; init; }
    public PoseEstimationService PoseEstimationService { get; init; }

    // [Layer][Texture][row, col]
    public PixelData[][][,] LayerTextureData { get; init; }
    public int[] LayerIndexes { get; init; }

    public LayerVisualizerManager(Node container)
    {
        NetworkContainer = container;
        PoseEstimationService = new PoseEstimationService();
        LayerTextureData = new PixelData[PoseEstimationService.FilterLayers.Length + 1][][,];
        LayerIndexes = new int[PoseEstimationService.FilterLayers.Length];
    }

    public void SelectFrame(byte[] depthData, byte[] colorData, int width, int height)
    {
        LayerTextureData[0][0] = PoseEstimationService.ConvertToData(
            depthData,
            colorData,
            width,
            height
        );
        UpdateLayer(0);
    }

    public void SelectLayer(int layerIndex, int textureIndex)
    {
        LayerIndexes[layerIndex] = textureIndex;
        UpdateLayer(layerIndex);
    }

    public void UpdateLayer(int indexLayer)
    {
        while (indexLayer < LayerIndexes.Length)
        {
            indexLayer++;
            PixelData[][,] data = new PixelData[1][,];
            data[0] = LayerTextureData[indexLayer][LayerIndexes[indexLayer]];
            LayerTextureData[indexLayer] = PoseEstimationService.FilterLayers[indexLayer].Convolute(data);
        }
    }
}