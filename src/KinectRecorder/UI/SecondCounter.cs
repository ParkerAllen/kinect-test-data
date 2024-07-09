using Godot;

public partial class SecondCounter : Node
{
	
	private Timer timer;
	public int Counter { get; private set; }

	public delegate void OnCountEventHandler(int remaining);

	public event OnCountEventHandler OnCount;

	public delegate void OnFinishEventHandler();

	public event OnFinishEventHandler OnFinish;

	public SecondCounter(Node parent)
	{
		timer = new Timer();
		timer.WaitTime = 1;
		timer.OneShot = false;
		AddChild(timer);
		timer.Timeout += CountDown;
		parent.AddChild(this);
	}

	public void Start(int duration)
	{
		Counter = duration;
		timer.Start();
	}

	public void Stop()
	{
		timer.Stop();
		ResetEvents();
	}

	private void CountDown()
	{
		Counter--;
		if( Counter == 0)
		{
			timer.Stop();
			OnFinish();
			return;
		}
		OnCount(Counter);
	}

	public void ResetEvents()
	{
		OnCount = delegate{};
		OnFinish = delegate{};
	}
}
