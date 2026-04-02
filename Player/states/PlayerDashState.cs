using Godot;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        GD.Print("Enter Dash");
    }

    public override void Update()
    {
        player.Dash();
    }
}