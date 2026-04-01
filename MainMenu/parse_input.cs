using Godot;
using System;

public partial class parse_input : TextureRect
{
	private SubViewport vp;
	private bool isWork = true;

	public override void _Ready()
	{
		vp = GetNode<SubViewport>("/root/MainMenu/SubViewport");
		if(vp == null)
		{
			GD.PushError("Error: subviewport not found");
		}
		else
		{
			return;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if(!isWork)
		{
			return;
		}
		if(vp != null && vp.IsInsideTree() && @event is InputEventMouse)
		{
			vp.PushInput(@event);
		}
	}

	public void SetVpNull()
	{
		isWork = false;
	}


}
