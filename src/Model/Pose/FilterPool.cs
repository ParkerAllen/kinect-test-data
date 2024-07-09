using System;

public record FilterPool
{
    public int Dimension { get; init; }
    public float[][,] Filters { get; init; }
    public float[] Normal { get; init; }
    public float[] Offset { get; init; }

    public FilterPool(float[][,] filters)
    {
        Dimension = filters[0].GetLength(0);
        Filters = filters;
        Normal = new float[filters.Length];
        Offset = new float[filters.Length];
        for (int i = 0; i < filters.Length; i++)
        {
            for (int y = 0; y < filters[i].GetLength(0); y++)
            {
                for (int x = 0; x < filters[i].GetLength(1); x++)
                {
                    Normal[i] += Math.Abs(filters[i][y,x]);
                    if (filters[i][y,x] < 0)
                    {
                        Offset[i]++;
                    }
                }
            }
            Offset[i] *= 1023;
        }
    }
}
