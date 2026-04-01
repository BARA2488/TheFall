using Godot;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        GD.Print("Enter Idle");
    }

    public override void Update()
    {
        if(player.InputDir != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        }
        else if(player.IsOnFloor() == false)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}