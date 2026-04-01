using Godot;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	public PlayerStateMachine StateMachine { get; private set; }

	public PlayerIdleState IdleState { get; private set; }
	public PlayerWalkState WalkState {get; private set; }
	public PlayerFallState FallState {get; private set; }

	//движение влево и вправо
	public float InputDir {get; private set; }
	public float deltaForce;
	private float speed = 100.0f;
	private float accel = 260f;
	//падение
	private float gravity = 500.0f;

    public override void _Ready()
    {
        StateMachine = new PlayerStateMachine();

		IdleState = new PlayerIdleState(this, StateMachine);
		WalkState = new PlayerWalkState(this, StateMachine);
		FallState = new PlayerFallState(this, StateMachine);

		StateMachine.Initialize(IdleState);
    }

    public override void _Process(double delta)
    {
        InputDir = Input.GetAxis("move_left", "move_right");
		StateMachine.CurrentState.Update();
    }

    public override void _PhysicsProcess(double delta)
    {
        StateMachine.CurrentState.PhysicsUpdate();

		deltaForce = (float)delta;
    }

    public void Move()
    {	float targetSpeed = InputDir * speed;
		float newVelocity = Mathf.MoveToward(Velocity.X, targetSpeed, accel * deltaForce);
        Velocity = new Vector2(newVelocity, Velocity.Y);
		MoveAndSlide();
    }

	public void Fall()
	{
		Velocity = new Vector2(Velocity.X, Velocity.Y + gravity * deltaForce);
	}

}