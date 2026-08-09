using Godot;
using System;

public partial class DevRoom : Node2D
{
    public override void _Ready()
    {
        var player = GetNode<Player>("Player");
        var door_1 = GetNode<Door>("Door1");

        player.Connect(Player.SignalName.Active, Callable.From(door_1.Activate));
    }

}
