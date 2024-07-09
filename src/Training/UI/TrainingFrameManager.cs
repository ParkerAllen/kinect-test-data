using Godot;
using System.Collections.Generic;

public class TrainingFrameManager : PoolManager<TrainingFramePanel>
{
    private readonly PackedScene frameButtonPrefab = ResourceLoader.Load("res://Scenes/Prefabs/TrainingFramePanel.tscn") as PackedScene;

    private TrainingDataController TrainingDataController { get; init; }
    public HashSet<int> Selected { get; init; }

    public TrainingFrameManager(TrainingDataController trainingDataController, Node frameContainer) : base(frameContainer)
    {
        TrainingDataController = trainingDataController;
        Selected = new HashSet<int>();
    }

    public override void InitElement(int index)
    {
        Pool[index].Init(this, index, CanShowFrameNumber(index));
    }

    private bool CanShowFrameNumber(int index)
    {
        return index % 5 == 0;
    }

    public void FrameEdited(int index, bool isEdited = true)
    {
        Pool[index].IsEdited(isEdited);
    }

    public override void SelectFrame(int index)
    {
        HandleSelected(index);
        TrainingDataController.DisplayFrame(index);
    }

    private void HandleSelected(int index)
    {
        if(!Input.IsActionPressed("ctrl_key"))
        {
            foreach(TrainingFramePanel fp in Pool)
            {
                fp.Highlight(false);
            }
            Selected.Clear();
        }
        if(Input.IsActionPressed("shift_key"))
        {
            int min = Mathf.Min(index, TrainingDataController.CurrentFrame);
            int max = Mathf.Max(index, TrainingDataController.CurrentFrame);
            while (min <= max)
            {
                Pool[min].Highlight();
                Selected.Add(min);
                min++;
            }
            return;
        }
        if(Selected.Contains(index))
        {
            Pool[index].Highlight(false);
            Selected.Remove(index);
            return;
        }
        Pool[index].Highlight();
        Selected.Add(index);
    }

    protected override PackedScene Prefab()
    {
        return frameButtonPrefab;
    }
}
