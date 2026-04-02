using Godot;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        GD.Print("Enter Walk");
        //player.InputDir = 1;
    }

    public override void Update()
    {
        player.Move();
        if(player.InputDir == 0 && player.Velocity == Vector2.Zero)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(player.IsOnFloor() == false)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}