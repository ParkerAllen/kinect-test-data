using Godot;
using System;

public class FilterDisplayHelper
{
	// private const int PIXEL_SIZE = 16;
	// private const int SEPERATOR_SIZE = 4;
	// public const Image.Format FilterImageFormat = Image.Format.La8;

	// public Texture2D Display(FilterPool filterPool)
	// {
	// 	int filterArea = PIXEL_SIZE * PIXEL_SIZE * filterPool.Dimension * filterPool.Dimension;
	// 	int speratorArea = SEPERATOR_SIZE * PIXEL_SIZE * filterPool.Dimension;

	// 	byte[] texture = new byte[(filterArea + speratorArea) * filterPool.Filters.Length * 2];

	// 	for (int i = 0; i < filterPool.Filters.Length; i++)
	// 	{
	// 		int start = (filterArea + speratorArea) * i * 2;
	// 		RenderFilter(filterPool.Filters[i], filterPool.Dimension, filterArea).CopyTo(texture, start);
	// 	}
		
	// 	return ImageTexture.CreateFromImage(
	// 		Image.CreateFromData(
	// 			PIXEL_SIZE * filterPool.Dimension,
	// 			(PIXEL_SIZE * filterPool.Dimension + SEPERATOR_SIZE) * filterPool.Filters.Length,
	// 			false,
	// 			FilterImageFormat,
	// 			texture
	// 		)
	// 	);
	// }

	// private byte[] RenderFilter(float[,] filter, int dimesion, int filterArea)
	// {
	// 	byte[] tex = new byte[filterArea * 2];
	// 	int index = 0;
	// 	for(int y = 0; y < dimesion; y++)
	// 	{
	// 		for(int i = 0; i < PIXEL_SIZE; i++)
	// 		{
	// 			for(int x = 0; x < dimesion; x++)
	// 			{
	// 				for(int j = 0; j < PIXEL_SIZE; j++)
	// 				{
	// 					tex[index] = (byte)((filter[y, x] + 1) * 255 / 2);
	// 					tex[index + 1] = 255;
	// 					index += 2;
	// 				}
	// 			}
	// 		}
	// 	}
	// 	return tex;
	// }
}
