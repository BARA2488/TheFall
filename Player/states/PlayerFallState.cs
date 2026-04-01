using Godot;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        GD.Print("Enter Fall");
    }

    public override void Update()
    {
        player.Fall();
        player.Move();
        if(player.IsOnFloor())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}