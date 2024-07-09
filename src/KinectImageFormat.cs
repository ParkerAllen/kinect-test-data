using Godot;
using System;
using freenect;

// Kinect Calibration
// http://rgbdemo.org/index.php/Documentation/KinectCalibrationTheory

public sealed class KinectImageFormat
{
    public const Image.Format VIDEO_FORMAT = Image.Format.Rgb8;
    public const Image.Format DEPTH_FORMAT = Image.Format.Rgba8;
    // public const Image.Format DEPTH_FORMAT = Image.Format.La8;
    // public const Image.Format DEPTH_FORMAT = Image.Format.Rg8;

    // 640x480 : RGB
	public static readonly KinectFrameMode VIDEO_MODE = new KinectFrameMode(3, 640, 480);
    // 640x480 : Depth11Bit
    public static readonly KinectFrameMode DEPTH_MODE = new KinectFrameMode(2, 640, 480);

	private byte[] DepthFormatBuffer { get; init; }
    private int[] rgbCalibrationBuffer { get; set; }

    // Calibrations
    private readonly float fx_d = 594.21434211923247f;
    private readonly float fy_d = 591.04053696870778f;
    private readonly float cx_d = 339.30780975300314f;
    private readonly float cy_d = 242.73913761751615f;
    private readonly float fx_rgb = 529.21508098293293f;
    private readonly float fy_rgb = 525.56393630057437f;
    private readonly float cx_rgb = 328.94272028759258f;
    private readonly float cy_rgb = 267.48068171871557f;

    // A2
    // private readonly float fx_d = 594.25464969100040f;
    // private readonly float fy_d = 592.48479436384002f;
    // private readonly float cx_d = 339.78729959351779f;
    // private readonly float cy_d = 242.50301427866111f;
    // private readonly float fx_rgb = 530.09194943536181f;
    // private readonly float fy_rgb = 526.35860167133876f;
    // private readonly float cx_rgb = 328.21930715948992f;
    // private readonly float cy_rgb = 268.72781351282777f;

    // A` - From Article
    private readonly Vector3[] R = {
        new Vector3(0.99984628826577793f, -0.0014779096108364480f, 0.017470421412464927f),
        new Vector3(0.0012635359098409581f, 0.99992385683542895f, 0.012275341476520762f),
        new Vector3(-0.017487233004436643f, -0.012251380107679535f, 0.99977202419716948f)
    };

    // A2` - From Article
    // private readonly Vector3[] R = {
    //     new Vector3(0.99977321644139494f, -0.0020032487074002391f, 0.021201478274968936f),
    //     new Vector3(0.0017292658422779497f, 0.99991486643051353f, 0.012933272242365573f),
    //     new Vector3(-0.021225581878346968f, -0.012893676196675344f, 0.99969156632836553f)
    // };

    // B - From StackOverflow
    // private readonly Vector3[] R = {
    //     new Vector3(0.999985794494467f, -0.003429138557773f, 0.00408066391266f),
    //     new Vector3(0.003420377768765f, 0.999991835033557f, 0.002151948451469f),
    //     new Vector3(-0.004088009930192f, -0.002137960469802f, 0.999989358593300f)
    // };

    private readonly Vector3 T = new Vector3(0.019985242312092553f, -0.00074423738761617583f, -0.010916736334336222f);

    // A2
    // private readonly Vector3 T = new Vector3(0.021354778990792557f, 0.0025073334719943473f, -0.012922411623995907f);

    
	private static readonly Lazy<KinectImageFormat> lazy =
        new Lazy<KinectImageFormat>(() => new KinectImageFormat());

    public static KinectImageFormat Instance { get { return lazy.Value; } }

    // public static KinectImageFormat Instance { get { return Nested.instance; } }
    // private class Nested
    // {
    //     static Nested()
    //     {
    //     }

    //     internal static readonly KinectImageFormat instance = mat();
    // }


    private KinectImageFormat()
    {
		Logs.Print("Depth Mode Settings: " + DEPTH_MODE.ToString());
		Logs.Print("Depth Image Format: " + DEPTH_FORMAT.ToString());
		Logs.Print("Video Mode Settings: " + VIDEO_MODE.ToString());
		Logs.Print("Video Image Format: " + VIDEO_FORMAT.ToString());
        Logs.Print("Initializing Depth Format Buffer");
        switch (DEPTH_FORMAT)
        {
            case Image.Format.Rgba8:
            {
                DepthFormatBuffer = BuildRGBFormatBuffer();
                break;
            }
            default:
            {
                DepthFormatBuffer = BuildLAFormatBuffer();
                break;
            }
        }
        rgbCalibrationBuffer = new int[DEPTH_MODE.width * DEPTH_MODE.height];
    }

    private byte[] BuildRGBFormatBuffer() {
        byte[] buffer = new byte[2048*3];
        for(int i = 0; i < 2048; i++)
		{
            int lb = i & 0xff;
            int index = i * 3;
            switch (i >> 8)
            {
                case 0:
                    buffer[index] = 255;
                    buffer[index + 1] = (byte) (255 - lb);
                    buffer[index + 2] = (byte) (255 - lb);
                    break;
                case 1:
                    buffer[index] = 255;
                    buffer[index + 1] = (byte)lb;
                    buffer[index + 2] = 0;
                    break;
                case 2:
                    buffer[index] = (byte) (255 - lb);
                    buffer[index + 1] = 255;
                    buffer[index + 2] = 0;
                    break;
                case 3:
                    buffer[index] = 0;
                    buffer[index + 1] = 255;
                    buffer[index + 2] = (byte)lb;
                    break;
                case 4:
                    buffer[index] = 0;
                    buffer[index + 1] = (byte) (255 - lb);
                    buffer[index + 2] = 255;
                    break;
                case 5:
                    buffer[index] = 0;
                    buffer[index + 1] = 0;
                    buffer[index + 2] = (byte) (255 - lb);
                    break;
                default:
                    buffer[index] = 0;
                    buffer[index + 1] = 0;
                    buffer[index + 2] = 0;
                    break;
            }
		}
        return buffer;
    }

    private byte[] BuildLAFormatBuffer() {
        byte[] buffer = new byte[1024];
        for(int i = 0; i < 1024; i++)
		{
			buffer[i] = (byte)((1 - Math.Pow(i / 1023.0, 3)) * 255);
		}
        return buffer;
    }

    public byte[] DepthToImage(byte[] original, byte alpha = 255)
    {
        switch (DEPTH_FORMAT)
        {
            case Image.Format.Rgba8:
            {
                return DepthToRGBA(original, alpha);
            }
            default:
            {
                return DepthToLA8(original, alpha);
            }
        }
    }

    private byte[] DepthToRGBA(byte[] original, byte alpha)
    {
        int len = original.Length / 2;
        byte[] buffer = new byte[original.Length + len + len];
        for(int i = 0; i < len; i++)
        {
            int formatIndex = (original[i * 2 + 1] << 8 | original[i * 2]) * 3;
            int bufferIndex = i  * 4;
            buffer[bufferIndex] = DepthFormatBuffer[formatIndex];
            buffer[bufferIndex + 1] = DepthFormatBuffer[formatIndex + 1];
            buffer[bufferIndex + 2] = DepthFormatBuffer[formatIndex + 2];
            buffer[bufferIndex + 3] = alpha;
        }
        return buffer;
    }

    private byte[] DepthToLA8(byte[] original, byte alpha)
    {
        byte[] buffer = new byte[original.Length];
        for(int i = 0; i < original.Length; i+=2)
        {
            buffer[i] = DepthFormatBuffer[Mathf.Min(original[i+1] << 8 | original[i], 1023)];
			buffer[i+1] = alpha;
        }
        return buffer;
    }

    public byte[] CalibrateRGB(byte[] original)
    {
        byte[] buffer = new byte[original.Length];
        for(int i = 0; i < original.Length; i+=3)
        {
            int calIndex = Mathf.Min(307199, rgbCalibrationBuffer[i / 3]) * 3;
            buffer[i] = original[calIndex];
            buffer[i + 1] = original[calIndex + 1];
            buffer[i + 2] = original[calIndex + 2];
            // byte c = (byte)(calIndex / 921600f * 255f);
            // buffer[i] = c;
            // buffer[i + 1] = c;
            // buffer[i + 2] = c;
        }
        return buffer;
    }

    public void BuildCalibration(byte[] depthBuffer)
    {
        // int min = int.MaxValue;
        // int max = int.MinValue;
        int[] temp = new int[rgbCalibrationBuffer.Length];
        for(int i = 0; i < depthBuffer.Length; i+=2)
        {
            int index = i / 2;
            int v = CalcCalibration(index, depthBuffer[i+1] << 8 | depthBuffer[i]);
            temp[index] = v;
            // min = Mathf.Min(min, v);
            // max = Mathf.Max(max, v);
        }
        rgbCalibrationBuffer = temp;
		// Logs.Print("RGB Calibration - Min: " + min + " Max: " + max);
    }

    private int CalcCalibration(int depthIndex, int depth)
    {
        int xIndex = depthIndex % DEPTH_MODE.width;
        int yIndex = depthIndex / DEPTH_MODE.width;

        float z = depthToMeters(depth);

        Vector3 t = new Vector3(
            (xIndex - cx_d) * z / fx_d,
            (yIndex - cy_d) * z / fy_d,
            z
        );

        Vector3 dot = Dot(R, t) + T;

        int calx = (int)(dot[0] * fx_rgb / dot[2] + cx_rgb);
        int caly = (int)(dot[1] * fy_rgb / dot[2] + cy_rgb);

        return calx + caly * DEPTH_MODE.width;
    }

    private float depthToMeters(int depth)
    {
        if (depth < 2047)
        {
            return 1.0f / (depth * -0.0030711016f + 3.3309495161f);
        }
        return 0;
    }

    private Vector3 Dot(Vector3[] matrix, Vector3 vector)
    {
        return new Vector3(
            matrix[0].Dot(vector),
            matrix[1].Dot(vector),
            matrix[2].Dot(vector)
        );
    }
}
