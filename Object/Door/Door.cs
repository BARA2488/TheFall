using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Door : Node2D
{
    //логика
    private Node2D player_in_radius;
    private bool is_active;
    private bool door_is_open = false;
    
    //ресурсы
    private Sprite2D open_door;
    private Sprite2D close_door;
    private AudioStreamPlayer open_sound;
    private AudioStreamPlayer close_sound;

    //взаимодействие
    private Area2D activate_radius;
    private CollisionShape2D door_collision;
    public override void _Ready()
    {
        open_door = GetNode<Sprite2D>("DoorOpen");
        close_door = GetNode<Sprite2D>("DoorClose");
        door_collision = GetNode<CollisionShape2D>("collision/door_collision");
        open_sound = GetNode<AudioStreamPlayer>("OpenDoorSound");
        close_sound = GetNode<AudioStreamPlayer>("CloseDoorSound");
    }

    public void Activate()
    {
        GD.Print("я среагировала");
        if(is_active == false){return;}

        if(door_is_open)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        door_is_open = true;
        open_door.Visible = true;
        close_door.Visible = false;
        door_collision.Disabled = true;
        open_sound.Play();
    }

    private void CloseDoor()
    {
        door_is_open = false;
        open_door.Visible = false;
        close_door.Visible = true;
        door_collision.Disabled = false;
        close_sound.Play();
    }

    public void _on_active_zone_body_entered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            player_in_radius = body;
            is_active = true;
        }
    }

    public void _on_active_zone_body_exited(Node2D body)
    {
        if (body.IsInGroup("player") && player_in_radius == body)
        {
            player_in_radius = null;
            is_active = false;
        }
    }
}
