using Godot;
using System;
using System.Threading.Tasks;

public partial class Transition : CanvasLayer
{
    private AnimationPlayer _anim;
    private Node _lastScene;

    public static Transition Instance;

    public override void _Ready()
    {
        Instance = this;

        _anim = GetNode<AnimationPlayer>("AnimationPlayer");

        _anim.Play("fade_in");
        _lastScene = GetTree().CurrentScene;

        GetTree().TreeChanged += OnTreeChanged;
    }

    private void OnTreeChanged()
    {
        var tree = GetTree();
        if (tree == null)
            return;

        var current = tree.CurrentScene;
        if (current != _lastScene)
        {
            _lastScene = current;
            FadeIn();
        }
    }

    public async Task FadeOut()
    {
        _anim.Play("fade_out");
        await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
    }

    public async Task FadeIn()
    {
        _anim.Play("fade_in");
        await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
    }
}