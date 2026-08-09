using Godot;
using System;
using System.Numerics;

public partial class MainMenu : Node2D
{   
    private AnimationPlayer animation;
    private AudioStreamPlayer inB;
    private AudioStreamPlayer selectB;
    private AudioStreamPlayer Ambient;
    private Sprite2D paralax;
    private Node2D bk;
    public override void _Ready()
    {
        paralax = GetNode<Sprite2D>("Paralax");
        bk = GetNode<Node2D>("SceneForMainMenu");
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        Ambient = GetNode<AudioStreamPlayer>("Ambient");
        inB = GetNode<AudioStreamPlayer>("Inside");
        selectB = GetNode<AudioStreamPlayer>("Select");
        animation.Play("version_shading");

        Ambient.Play();
        Ambient.Stream._HasLoop();
    }
    public override void _Process(double delta)
    {
        //paralax.Position = new Vector2(1498.0f + GetGlobalMousePosition().X * -1 / 15, 539.0f);
        //bk.Position = new Vector2(960.0f + GetGlobalMousePosition().X * -1 / 50, 540.0f);

        //var start_pos = new Vector2(960f, 540f);
        //var target_pos = new Vector2(60f, 540f);
        //float target_pos = Mathf.MoveToward(bk.GlobalPosition.X, GetGlobalMousePosition().X * -1 / 15, 500 * (float)delta);
        //bk.Position = new Godot.Vector2(target_pos, 540f);
        //GD.Print(target_pos);
    }

    private async void _on_play_button_pressed()
    {
        selectB.Play();
        
        var ParseInput = GetNode<parse_input>("CanvasLayer/Control/main");
        ParseInput.SetVpNull();
        
        await Transition.Instance.FadeOut();
        GetTree().ChangeSceneToFile("res://DevRoom/dev_room.tscn");
        await Transition.Instance.FadeIn();
        
    }
    //close game
    private async void _on_exit_button_pressed()
    {
        selectB.Play();
        await Transition.Instance.FadeOut();
        GetTree().Quit();
    }

    public void _on_play_button_mouse_entered()
    {
        inB.Play();
    }
    public void _on_setting_button_mouse_entered()
    {
        //inB.Play();
    }
    public void _on_exit_button_mouse_entered()
    {
        inB.Play();
    }
}
