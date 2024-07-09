using Godot;

public class KinectTextureDisplay
{
	public Vector2I Resolution { get; init; }
	public Image.Format ImageFormat { get; init; }

	private ImageTexture CachedTexture { get; init; }
	private Image CachedImage { get; init; }

	public KinectTextureDisplay(TextureRect displayTexture, Vector2I resolution, Image.Format imageFormat)
	{
		Resolution = resolution;
		ImageFormat = imageFormat;
		CachedImage = Image.Create(Resolution.X, Resolution.Y, false, ImageFormat);
		CachedTexture = ImageTexture.CreateFromImage(CachedImage);
		displayTexture.Texture = CachedTexture;
		Logs.Print("Resolution: " + Resolution.X + "x" + Resolution.Y);
	}

	public void UpdateTexture(byte[] buffer)
	{
		// Logs.Print("Updating Tex");
		CachedImage.SetData(Resolution.X, Resolution.Y, false, ImageFormat, buffer);
		CachedTexture.Update(CachedImage);
	}
}
