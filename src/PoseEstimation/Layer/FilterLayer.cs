using Godot;
using System;

public class FilterLayer
{
    public int FilterSize { get; init; }
    public Filter[] Filters { get; init; }

    int[] offsets;

    public FilterLayer(Filter[] filters)
    {
        Filters = filters;
        FilterSize = filters[0].Values.GetLength(0);
        offsets = new int[FilterSize];

        int filterHalf = FilterSize / 2;
        for(int i = 0; i < FilterSize; i++)
		{
			int j = i - filterHalf;
			offsets[i] = j;
		}
    }

    // data -> [Texture][rows/y, col/x]
    public PixelData[][,] Convolute(PixelData[][,] data)
	{
		// short min = Int16.MaxValue; // 32767
		// short max = Int16.MinValue; // -32767
		PixelData[][,] result = new PixelData[Filters.Length * data.GetLength(0)][,];
        for (int textureIndex = 0; textureIndex < data.GetLength(0); textureIndex++)
        {
            for (int filterIndex = 0; filterIndex < Filters.Length; filterIndex++)
            {
                PixelData[,] texture = new PixelData[data.GetLength(1), data.GetLength(2)];
                for (int pixelY = 0; pixelY < data.GetLength(1); pixelY++)
                {
                    for (int pixelX = 0; pixelX < data.GetLength(2); pixelX++)
                    {
                        float[] dot = new float[4];
                        for (int filterY = 0; filterY < FilterSize; filterY++)
                        {
                            int y = Math.Clamp(pixelY + offsets[filterY], 0, data.GetLength(1));
                            for (int filterX = 0; filterX < FilterSize; filterX++)
                            {
                                int x = Math.Clamp(pixelX + offsets[filterX], 0, data.GetLength(2));
                                dot[0] += data[textureIndex][y, x].d * Filters[filterIndex].Values[filterY, filterX][0];
                                dot[1] += data[textureIndex][y, x].r * Filters[filterIndex].Values[filterY, filterX][1];
                                dot[2] += data[textureIndex][y, x].g * Filters[filterIndex].Values[filterY, filterX][2];
                                dot[3] += data[textureIndex][y, x].b * Filters[filterIndex].Values[filterY, filterX][3];
                            }
                        }
                        // Normalize
                        for (int i = 0; i < dot.Length; i++)
                        {
                            dot[i] = (dot[i] + Filters[filterIndex].Offset[i]) / Filters[filterIndex].Normal[i];
                        }
        
                        // min = (short)Mathf.Min(min, dot);
                        // max = (short)Mathf.Max(max, dot);
                        texture[pixelY, pixelX] = new PixelData(dot);
                    }
                }
                result[textureIndex * filterIndex + filterIndex] = texture;
            }
        }
		// Logs.Print("Min: " + min + ", Max: " + max);
		return result;
	}
}