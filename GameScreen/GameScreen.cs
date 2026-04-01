using Godot;
using System;
using System.Diagnostics;
using System.Numerics;

public partial class GameScreen : Node2D
{
	private int step = 0;
	private TileMapLayer tml;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		tml = GetNode<TileMapLayer>("TileMapLayer");
		//DrawTile(0, 0, step);
		
		for(int i = 1; i < 19; i++)
		{
			step += 6;
			for(int j = 0; j < 2; j++)
			{
				DrawTile(rng.RandiRange(-6, 6), 1, rng.RandiRange(step, step + rng.RandiRange(-1,1)));
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void DrawPlatform()
	{
		
	}

	public void DrawTile(int x, int id, int step)
	{
		tml.SetCell(new Vector2I(x,step),id, new Vector2I(0,0), 0);
	}
}
