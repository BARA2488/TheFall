using Godot;
using System;

public partial class MainMenu : Node2D
{   
    private AnimationPlayer animation;
    private AudioStreamPlayer inB;
    private AudioStreamPlayer selectB;
    private AudioStreamPlayer Ambient;
    public override void _Ready()
    {
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        Ambient = GetNode<AudioStreamPlayer>("Ambient");
        inB = GetNode<AudioStreamPlayer>("Inside");
        selectB = GetNode<AudioStreamPlayer>("Select");
        animation.Play("version_shading");

        Ambient.Play();
        Ambient.Stream._HasLoop();
    }

    private async void _on_play_button_pressed()
    {
        selectB.Play();
        
        var ParseInput = GetNode<parse_input>("CanvasLayer/Control/main");
        ParseInput.SetVpNull();
        
        await Transition.Instance.FadeOut();
        GetTree().ChangeSceneToFile("res://dev_room.tscn");
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
