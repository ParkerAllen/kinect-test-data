using Godot;
using System.Collections.Generic;

public class BoneEditor
{
    private ArmatureManager ArmatureManager { get; init; }
    private Node Container { get; init; }

    private NameInput NameInput { get; init; }
    private ColorSelector ColorSelector { get; init; }
    private VectorInput VectorInput { get; init; }
    private BoneInput NextBoneInput { get; init; }
    private BoneInput PreviousBoneInput { get; init; }
    private BoneListInput ExtendedBonesInput { get; init; }

    private bool locked;

    public BoneEditor(ArmatureManager armaturManager, Node container, ColorPickerManager colorPickerManager)
    {
		Logs.Print("Loading BoneEditor...");
        ArmatureManager = armaturManager;
        Container = container;

        NameInput = new NameInput(" Name:", UpdateSettings);
        ColorSelector = new ColorSelector(" Color:", colorPickerManager, UpdateSettings);
        VectorInput = new VectorInput(UpdateSettings);
        NextBoneInput = new BoneInput(" Next:", ArmatureManager, UpdateNext);
        PreviousBoneInput = new BoneInput(" Previous:", ArmatureManager, UpdatePrevious);
        ExtendedBonesInput = new BoneListInput(" Bones:", ArmatureManager, UpdateExtended);
    }

    public void Instance()
    {
        NameInput.Instance(Container, "");
        ColorSelector.Instance(Container, Colors.Red);
        VectorInput.Instance(Container, new Vector2(1,1));
        NextBoneInput.Instance(Container, -1);
        PreviousBoneInput.Instance(Container, -1);
        ExtendedBonesInput.Instance(Container, new List<int>());
        ArmatureManager.BoneUpdateEvent += Display;
    }

    public void Display()
    {
		// Logs.Print("Displaying Bone: " + ArmatureManager.Target.BoneSettings.Name);
        locked = true;
        NameInput.Input = ArmatureManager.Target.BoneSettings.Name;
        ColorSelector.Input = ArmatureManager.Target.BoneSettings.Color;
        VectorInput.Input = ArmatureManager.Target.BoneSettings.Position;
        NextBoneInput.Input = ArmatureManager.Target.BoneNode.Next;
        PreviousBoneInput.Input = ArmatureManager.Target.BoneNode.Previous;
        ExtendedBonesInput.Input = new List<int>(ArmatureManager.Target.BoneNode.Extended);
        locked = false;
    }

    public void UpdateSettings()
    {
        if(locked)
        {
            return;
        }
        ArmatureManager.Update(new BoneSettings(
            NameInput.Input,
            ColorSelector.Input,
            VectorInput.Input
        ));
    }

    public void UpdateNext()
    {
        if(locked)
        {
            return;
        }
        if(NextBoneInput.Input == -1)
        {
            ArmatureManager.isPicking = ArmatureManager.RefBone.Next;
		    Logs.Print("Picking Next...");
            return;
        }
        ArmatureManager.UpdateNext(-1);
    }

    public void UpdatePrevious()
    {
        if(locked)
        {
            return;
        }
        if(PreviousBoneInput.Input == -1)
        {
            ArmatureManager.isPicking = ArmatureManager.RefBone.Previous;
		    Logs.Print("Picking Previous...");
            return;
        }
        ArmatureManager.UpdatePrevious(-1);
    }

    public void UpdateExtended()
    {
        if(locked)
        {
            return;
        }
        if(ExtendedBonesInput.RemovedBone == -1)
        {
            ArmatureManager.isPicking = ArmatureManager.RefBone.Extended;
		    Logs.Print("Picking Extended...");
            return;
        }
        ArmatureManager.RemoveExtended(ExtendedBonesInput.RemovedBone);
        ExtendedBonesInput.RemovedBone = -1;
    }
}
