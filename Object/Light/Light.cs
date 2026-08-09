using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Light : StaticBody2D
{
    private AnimationPlayer anim;

    public override void _Ready()
    {
        anim = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        if (body.IsInGroup("player") && !anim.IsPlaying())
        {
            anim.Play("left_w");
        }
    }
    private void _on_area_2d_2_body_entered(Node2D body)
    {
        if (body.IsInGroup("player") && !anim.IsPlaying())
        {
            anim.Play("right_w");
        }
    }
}
