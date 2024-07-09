using Godot;

public class ConvolutionHelper
{
    // public byte[] Convolute(byte[] data, int texWidth, float[,] filter, float normal, float normalOffset)
	// {
	// 	byte[] result = new byte[data.Length];
	// 	short[] d = DepthDataToShort(data);
    //     int[] xOffsets = new int[filter.GetLength(0)];
    //     int[] yOffsets = new int[filter.GetLength(0)];
        
    //     int filterHalf = filter.GetLength(0) / 2;
	// 	// short min = Int16.MaxValue; // 32767
	// 	// short max = Int16.MinValue; // -32767

	// 	for(int i = 0; i < filter.GetLength(0); i++)
	// 	{
	// 		int j = i - filterHalf;
	// 		yOffsets[i] = j * texWidth;
	// 		xOffsets[i] = j;
	// 	}

	// 	for(int i = 0; i < d.Length; i++)
	// 	{
	// 		int index = i;
	// 		short r = 0;
	// 		for(int y = 0; y < filter.GetLength(0); y++)
	// 		{
	// 			int y2 = i + yOffsets[y];
	// 			if (y2 >= 0 && y2 < d.Length)
	// 			{
	// 				index += yOffsets[y];
	// 			}
	// 			for(int x = 0; x < filter.GetLength(1); x++)
	// 			{
	// 				int x2 = i % texWidth + xOffsets[x];
	// 				if (x2 >= 0 && x2 < texWidth)
	// 				{
	// 					index += xOffsets[x];
	// 				}
	// 				r += (short)(filter[x,y] * d[index]);
	// 			}
	// 		}
    //         // Normalize
    //         r = (short)((r + normalOffset) / normal);

	// 		result[i * 2] = (byte)(r & 65535);
	// 		result[i * 2 + 1] = (byte)(r >> 8);
			
	// 		// min = (short)Mathf.Min(min, r);
	// 		// max = (short)Mathf.Max(max, r);
	// 	}
	// 	// Logs.Print("Min: " + min + ", Max: " + max);
	// 	return result;
	// }


	// private short[] DepthDataToShort(byte[] data)
	// {
	// 	short[] d = new short[data.Length / 2];
	// 	for(int i = 0; i < data.Length; i+=2)
	// 	{
	// 		d[i / 2] = (short)((data[i + 1] << 8) + data[i]);
	// 	}
	// 	return d;
	// }

    // public byte[] MaxPooling2x2(byte[] data, Vector2I basseResolution)
    // {
    //     int realX = basseResolution.X * 2;
    //     int[] offsets2x2 = new int[]{0, 1, 2, 3, realX, realX + 1, realX + 2, realX + 3};
    //     byte[] pooling = new byte[data.Length / 4];
    //     int i = 0;
    //     for (int y = 0; y < basseResolution.Y; y+=2)
    //     {
    //         int index = y * basseResolution.X;
    //         for (int x = 0; x < basseResolution.X; x+=2)
    //         {
    //             int j = (index + x) * 2;
    //             short max = Max(
    //                 data[j + offsets2x2[0]], data[j + offsets2x2[1]],
    //                 data[j + offsets2x2[2]], data[j + offsets2x2[3]],
    //                 data[j + offsets2x2[4]], data[j + offsets2x2[5]],
    //                 data[j + offsets2x2[6]], data[j + offsets2x2[7]]
    //             );
    //             pooling[i] = (byte)(max & 65535);
    //             pooling[i + 1] = (byte)(max >> 8);
    //             i+=2;
    //         }
    //     }
    //     return pooling;
    // }

    // private short Max(params byte[] values)
    // {
    //     short max = (short)((values[1] << 8) + values[0]);
    //     for (int i = 2; i < values.Length; i+=2)
    //     {
    //         max = (short)Mathf.Max(max, (short)((values[i + 1] << 8) + values[i]));
    //     }
    //     return max;
    // }
}
