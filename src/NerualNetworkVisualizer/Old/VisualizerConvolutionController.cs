using Godot;
using System;
using System.IO;
using Newtonsoft.Json;

public partial class VisualizerConvolutionController : Node
{
    // [Export]
    // public VisualizerController Controller { get; private set; }
    // [Export]
    // public Node Container { get; private set; }

    // public FilterLayer[] FilterLayers { get; private set; }

    // private readonly String FilterDataPath = "";



	// public override void _Ready()
	// {
    //     float[][][,] filterData = Load();
    //     FilterLayers = new FilterLayer[filterData.Length];
    //     Vector2I resolution = new Vector2I(Controller.DepthMode.Width, Controller.DepthMode.Height);
    //     for (int i = 0; i < filterData.Length; i++)
    //     {
    //         FilterLayers[i] = new FilterLayer(filterData[i], resolution);
    //         resolution /= 2;
    //     }
	// }

    // private float[][][,] Load()
    // {
    //     string json = "";
    //     using (StreamReader r = new StreamReader(FilterDataPath))
    //     {
    //         json = r.ReadToEnd();
    //     }
    //     return JsonConvert.DeserializeObject<float[][][,]>(json);
    // }
}