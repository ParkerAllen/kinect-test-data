using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class ArmatureManager
{
	private Armature Armature { get; set; }
	public int Index { get; private set; }

	public Bone Target => Armature.Bones[Index];

	public delegate void OnUpdateEventHandler();
	public event OnUpdateEventHandler BoneUpdateEvent = delegate { };
	public event OnUpdateEventHandler ArmatureUpdateEvent = delegate { };

	public RefBone isPicking = RefBone.None;
	public enum RefBone
	{
		None,
		Next,
		Previous,
		Extended
	}

	public ArmatureManager()
	{
		Armature = new Armature();
	}

	public ArmatureManager(Armature armature)
	{
		Armature = armature;
	}

	public Bone Get(int index)
	{
		if(index < 0 || index >= Armature.Bones.Count)
		{
			return null;
		}
		return Armature.Bones[index];
	}

	public List<Bone> Get()
	{
		return Armature.Bones;
	}

	public List<int> GetChildren(int index)
	{
		List<int> children = new List<int>();
		if(index == -1)
		{
			return children;
		}
		children.Add(index);
		children.AddRange(GetChildren(Armature.Bones[index].BoneNode.Next));
		foreach(int i in Armature.Bones[index].BoneNode.Extended)
		{
			children.AddRange(GetChildren(i));
		}

		return children;
	}

	public void Add(Bone bone)
	{
		Logs.Print("Adding Bone");
		Armature.Bones.Add(bone);
		SelectBone(Armature.Bones.Count - 1);
	}

	public void Remove()
	{
		Logs.Print("Removinf Bone: " + Index);
		Armature.Bones.RemoveAt(Index);
	}

	public void Move(Vector2 velocity)
	{
		Move(Index, velocity);
		UpdateBone();
	}

	public void Move(int index, Vector2 velocity)
	{
		Bone target = Armature.Bones[index];
		Armature.Bones[index] = Settings(target, new BoneSettings(
			target.BoneSettings.Name,
			target.BoneSettings.Color,
			target.BoneSettings.Position + velocity
		));
	}

	public void Update(BoneSettings boneSettings)
	{
		Armature.Bones[Index] = Settings(Target, boneSettings);
		UpdateBone();
	}

	public void SelectBone(int index)
	{
		Logs.Print("Selected Bone: " + Index);
		switch(isPicking)
		{
			case RefBone.Next:
			{
				UpdateNext(index);
				break;
			}
			case RefBone.Previous:
			{
				UpdatePrevious(index);
				break;
			}
			case RefBone.Extended:
			{
				AddExtended(index);
				break;
			}
			default:
			{
				Index = index;
				UpdateBone();
				break;
			}
		}
	}

	public void UpdateNext(int newNext)
	{
		isPicking = RefBone.None;
		if(Target.BoneNode.Next == newNext)
		{
			return;
		}

		SetNext(Index, newNext);
		if(newNext != -1)
		{
			SetPrevious(newNext, Index);
		}

		UpdateBone();
	}

	public void UpdatePrevious(int newPrevious)
	{
		isPicking = RefBone.None;
		if(Target.BoneNode.Previous == newPrevious)
		{
			return;
		}
		
		SetPrevious(Index, newPrevious);
		if(newPrevious != -1)
		{
			Armature.Bones[newPrevious] = Next(Armature.Bones[newPrevious], Index);
		}
		UpdateBone();
	}

	public void AddExtended(int newExtended)
	{
		isPicking = RefBone.None;
		if(Target.BoneNode.Extended.Contains(newExtended))
		{
			return;
		}

		int[] newExtendedList = Target.BoneNode.Extended.Append(newExtended).ToArray();

		SetPrevious(newExtended, Index);

		Armature.Bones[Index] = Extended(Target, newExtendedList);
		UpdateBone();
	}

	public void RemoveExtended(int newExtended)
	{        
		Armature.Bones[newExtended] = Previous(Armature.Bones[newExtended], -1);
		Armature.Bones[Index] = Extended(
			Target,
			Target.BoneNode.Extended.Where((source, val) => val != newExtended).ToArray()
		);
		UpdateBone();
	}

	private void SetNext(int target, int newNext)
	{
		ClearNextNode(target);
		Armature.Bones[target] = Next(Armature.Bones[target], newNext);
	}

	private void SetPrevious(int target, int newPrevious)
	{
		ClearPreviousNode(target);
		Armature.Bones[target] = Previous(Armature.Bones[target], newPrevious);
	}

	private void ClearNextNode(int target)
	{
		int next = Armature.Bones[target].BoneNode.Next;
		if(next == -1)
		{
			return;
		}
		Armature.Bones[next] = Previous(Armature.Bones[next], -1);
	}

	private void ClearPreviousNode(int target)
	{
		int previous = Armature.Bones[target].BoneNode.Previous;
		if(previous == -1)
		{
			return;
		}
		if(Armature.Bones[previous].BoneNode.Next == target)
		{
			Armature.Bones[previous] = Next(Armature.Bones[previous], -1);
		}
		Armature.Bones[previous] = Extended(
			Armature.Bones[previous], 
			Armature.Bones[previous].BoneNode.Extended.Where((source, val) => val != target).ToArray()
		);
	}

	private Bone Next(Bone target, int next)
	{
		return Node(target, new BoneNode(
			next,
			target.BoneNode.Previous,
			target.BoneNode.Extended
		));
	}

	private Bone Previous(Bone target, int previous)
	{
		return Node(target, new BoneNode(
			target.BoneNode.Next,
			previous,
			target.BoneNode.Extended
		));
	}

	private Bone Extended(Bone target, int[] extended)
	{
		return Node(target, new BoneNode(
			target.BoneNode.Next,
			target.BoneNode.Previous,
			extended
		));
	}

	private Bone Settings(Bone target, BoneSettings boneSettings)
	{
		return new Bone(target.BoneNode, boneSettings);
	}

	private Bone Node(Bone target, BoneNode boneNode)
	{
		return new Bone(boneNode, target.BoneSettings);
	}

	public void UpdateBone()
	{
		BoneUpdateEvent.Invoke();
		ArmatureUpdateEvent.Invoke();
	}

	public void Save(string path)
	{
		Logs.Print("Saving Armature...");
		new FileSaverArmature(path).Save(Armature);
	}

	public void Load(string path)
	{
		Load(new FileLoaderArmature(path).Armature);
	}

	public void Load(Armature armature)
	{
		Logs.Print("Loading Armature...");
		Armature = armature;
		SelectBone(0);
	}
}
