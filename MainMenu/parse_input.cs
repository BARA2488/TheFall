using Godot;
using System;

public partial class parse_input : TextureRect
{
	private SubViewport vp;
	private bool isWork = true;

	public override void _Ready()
	{
		vp = GetNode<SubViewport>("/root/MainMenu/Test/SubViewport");
		if(vp == null)
		{
			GD.PushError("Error: subviewport not found");
		}
		else if(vp != null)
		{
			GD.Print("SubViewport: founded.");
		}
		//vp.Size = new Vector2I(1920,1080);
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
