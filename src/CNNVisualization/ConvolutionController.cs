using Godot;
using System;

public class ConvolutionController
{
	// public ConvolutionHelper ConvolutionHelper { get; init;}
	// public PoolingHelper PoolingHelper { get; init; }
    // public KinectTextureDisplay[] DepthTextureDisplays { get; init; }

	// public FilterPool FilterPool { get; init; }
	// public Vector2I BaseResolution { get; init; }
	// public readonly Image.Format DepthImageFormat = Image.Format.La8;

	// public ConvolutionController(Node container, FilterPool filters, Vector2I resolution)
	// {
	// 	ConvolutionHelper = new ConvolutionHelper();
	// 	PoolingHelper = new PoolingHelper();
	// 	FilterPool = filters;
	// 	BaseResolution = resolution;

	// 	DepthTextureDisplays = new KinectTextureDisplay[filters.Filters.Length];
	// 	for (int i = 0; i < filters.Filters.Length; i++)
	// 	{
	// 		TextureRect tex = new TextureRect();
	// 		container.AddChild(tex);
	// 		DepthTextureDisplays[i] = new KinectTextureDisplay(tex, BaseResolution / 2, DepthImageFormat);
	// 	}
	// }

	// public void SelectFrame(byte[] frame)
	// {
	// 	for (int i = 0; i < DepthTextureDisplays.Length; i++)
	// 	{
	// 		DepthTextureDisplays[i].UpdateTexture(
	// 			DepthDataConverter.Instance.DepthToLA8(
	// 				PoolingHelper.MaxPooling2x2(ConvolutionHelper.Convolute(frame, BaseResolution.X, FilterPool.Filters[i], FilterPool.Normal[i], FilterPool.Offset[i]), BaseResolution)
	// 			)
	// 		);
	// 	}
	// }
}
