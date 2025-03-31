using Godot;
using System;

[GlobalClass]
public partial class Trash : Resource
{
	[Export] public PackedScene trashType = null;
	[Export] public int TrashAmount = 0;
}
