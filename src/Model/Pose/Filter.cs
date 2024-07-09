using System;

public record Filter
{
    public float[,][] Values { get; init; }
    public float[] Normal { get; init; }
    public int[] Offset { get; init; }

    public Filter(float[,][] values)
    {
        Normal = new float[values.GetLength(2)];
        Offset = new int[values.GetLength(2)];
        Values = values;
        for (int y = 0; y < values.GetLength(0); y++)
        {
            for (int x = 0; x < values.GetLength(1); x++)
            {
                for (int w = 0; w < values[y,x].Length; w++)
                {
                    Normal[w] += Math.Abs(values[y,x][w]);
                    if (values[y,x][w] < 0)
                    {
                        Offset[w]++;
                    }
                }
            }
        }
        for (int w = 0; w < values.GetLength(2); w++)
        {
            Offset[w] *= 1023;
        }
    }

    public Filter(float[,,] values)
    {
        Normal = new float[values.GetLength(2)];
        Offset = new int[values.GetLength(2)];
        Values = new float[values.GetLength(0), values.GetLength(1)][];
        for (int y = 0; y < values.GetLength(0); y++)
        {
            for (int x = 0; x < values.GetLength(1); x++)
            {
                for (int w = 0; w < values.GetLength(2); w++)
                {
                    Values[y,x][w] = values[y,x,w];
                    Normal[w] += Math.Abs(values[y,x,w]);
                    if (values[y,x,w] < 0)
                    {
                        Offset[w]++;
                    }
                }
            }
        }
        for (int w = 0; w < values.GetLength(2); w++)
        {
            Offset[w] *= 1023;
        }
    }
}