public readonly struct PixelData
{
    public short d { get; init; }
    public byte r { get; init; }
    public byte g { get; init; }
    public byte b { get; init; }

    public PixelData(byte d1, byte d2, byte r, byte g, byte b)
    {
        this.d = (short)((d1 << 8) + d2);
        this.r = r;
        this.g = g; 
        this.b = b;
    }

    public PixelData(float[] data)
    {
        this.d = (short)data[0];
        this.r = (byte)data[1];
        this.g = (byte)data[2]; 
        this.b = (byte)data[3];
    }
}