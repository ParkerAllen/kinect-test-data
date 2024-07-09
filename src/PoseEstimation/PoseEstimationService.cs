
public class PoseEstimationService
{
    public FilterLayer[] FilterLayers = new FilterLayer[]{
        new FilterLayer(
            new Filter[]{
                new Filter(new float[,,] { 
                    { {1, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 1} },
                    { {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0} },
                    { {-1, -1, -1, -1}, {-1, -1, -1, -1}, {-1, -1, -1, -1} } 
                }),
                new Filter(new float[,,] { 
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {-1, -1, -1, -1} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {-1, -1, -1, -1} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {-1, -1, -1, -1} } 
                }),
                new Filter(new float[,,] { 
                    { {1, 1, 1, 1}, {1, 1, 1, 1}, {0, 0, 0, 0} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {-1, -1, -1, -1} },
                    { {0, 0, 0, 0}, {-1, -1, -1, -1}, {-1, -1, -1, -1} } 
                }),
                new Filter(new float[,,] { 
                    { {1, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 1} },
                    { {1, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 1} },
                    { {1, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 1} } 
                })
            }
        ),
        new FilterLayer(
            new Filter[]{
                new Filter(new float[,,] { 
                    { {-1, -1, -1, -1}, {1, 1, 1, 1}, {-1, -1, -1, -1} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {1, 1, 1, 1} },
                    { {-1, -1, -1, -1}, {1, 1, 1, 1}, {-1, -1, -1, -1} } 
                }),
                new Filter(new float[,,] { 
                    { {0, 0, 0, 0}, {-1, -1, -1, -1}, {1, 1, 1, 1} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {-1, -1, -1, -1} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {-1, -1, -1, -1} } 
                }),
                new Filter(new float[,,] { 
                    { {0, 0, 0, 0}, {1, 1, 1, 1}, {0, 0, 0, 0} },
                    { {1, 1, 1, 1}, {-1, -1, -1, -1}, {1, 1, 1, 1} },
                    { {1, 1, 1, 1}, {-1, -1, -1, -1}, {1, 1, 1, 1} } 
                }),
                new Filter(new float[,,] { 
                    { {-1, -1, -1, -1}, {1, 1, 1, 1}, {1, 1, 1, 1} },
                    { {1, 1, 1, 1}, {0, 0, 0, 0}, {0, 0, 0, 0} },
                    { {0, 0, 0, 0}, {-1, -1, -1, -1}, {-1, -1, -1, -1} } 
                })
            }
        )
    };

    public PixelData[,] ConvertToData(byte[] depth, byte[] rgb, int width, int height)
    {
        PixelData[,] data = new PixelData[width, height];
        for (int y = 0; y < data.Length; y++)
        {
            int i = y * width;
            for (int x = 0; x < data.Length; x++)
            {
                data[x, y] = new PixelData(
                    depth[i],
                    depth[i + 1],
                    rgb[i],
                    rgb[i + 1],
                    rgb[i + 2]
                );
                i++;
            }
        }
        return data;
    }
}