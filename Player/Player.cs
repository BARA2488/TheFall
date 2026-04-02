using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	public PlayerStateMachine StateMachine { get; private set; }

	public PlayerIdleState IdleState { get; private set; }
	public PlayerWalkState WalkState {get; private set; }
	public PlayerFallState FallState {get; private set; }

	//Движение
	public float InputDir {get; set; }
	public float deltaTime;
	private float speed = 100.0f;
	private float accel = 260f;
	private float jumpVelocity = -220.0f;
	private float CurrentDir;
	
	//падение
	private float gravity = 500.0f;
	private float fall_speed_threshold = 700.0f;
	private float VerticalSpeed;
	private bool was_on_floor = false;
	
	//рывок
	private float dashSpeed = 270.0f;
	private float DASH_TIME = 0.1f;
	private float dash_timer = 0.0f;
	private float DASH_COLDOWN = 0.7f;
	private float dash_cd_timer = 0.0f;

	//чит-меню
	private bool invicible = false;

	//спрайты
	private ColorRect DeadScreen;

	//звуки
	private AudioStreamPlayer sfxLaning;
	private AudioStreamPlayer sfxFall;

    public override void _Ready()
    {
        StateMachine = new PlayerStateMachine();

		IdleState = new PlayerIdleState(this, StateMachine);
		WalkState = new PlayerWalkState(this, StateMachine);
		FallState = new PlayerFallState(this, StateMachine);

		StateMachine.Initialize(IdleState);

		//загрузка ресурсов
		DeadScreen = GetNode<ColorRect>("CanvasLayer/ColorRect");
		//sfx
		sfxLaning = GetNode<AudioStreamPlayer>("sfx/sfxLanding");
    }

    public override void _Process(double delta)
    {
        InputDir = Input.GetAxis("move_left", "move_right");
		StateMachine.CurrentState.Update();
		TimersManager();
    }

    public override void _PhysicsProcess(double delta)
    {
		//Частота обновления
		deltaTime = (float)delta;
		
		//Посследний ввод
		if(InputDir != 0)
		{
			CurrentDir = InputDir;
		}
		
		//Обновление вертикальной скорости
		VerticalSpeed = Velocity.Y;

		//Обновление состояния
		StateMachine.CurrentState.PhysicsUpdate();
		
		MoveAndSlide();
    }

    //Ввод
    public override void _Input(InputEvent e)
    {
        //Прыжок
		if(e.IsActionPressed("jump") && IsOnFloor())
        {
            Jump();
        }
		//Рывок
		if(Input.IsActionJustPressed("dash") && dash_cd_timer <= 0)
		{
			dash_timer = DASH_TIME;
			dash_cd_timer = DASH_COLDOWN;
			Dash();
		}
    }



    public void Move()
    {	float targetSpeed = InputDir * speed;
		float newVelocity = Mathf.MoveToward(Velocity.X, targetSpeed, accel * deltaTime);
        Velocity = new Vector2(newVelocity, Velocity.Y);
    }

	public void Fall()
	{	
		Velocity = new Vector2(Velocity.X, Velocity.Y + gravity * deltaTime);
		
		//Расчет смерти при призимлении
		
		//GD.Print("vert. s. : " + VerticalSpeed);
		if(!was_on_floor && IsOnFloor() && invicible == false)
		{
			float impact_speed = Mathf.Abs(VerticalSpeed);
			//GD.Print(impact_speed);
			if(impact_speed > fall_speed_threshold)
			{
				Dead();
			}
			if(impact_speed > 100 && impact_speed < fall_speed_threshold)
			{
				sfxLaning.Play();
			}
		}
		was_on_floor = IsOnFloor();
	}

	public void Jump()
	{
		Velocity = new Vector2(Velocity.X, jumpVelocity);
		GD.Print("Прыгнул");
	}

	public void Dash()
	{
		Velocity = new Vector2(CurrentDir * dashSpeed, Velocity.Y);
		GD.Print("Сделал рывок");
	}

	public async void Dead()
	{
		GetTree().Paused = true;
		var timer = GetTree().CreateTimer(0.05f);
		await ToSignal(timer, Timer.SignalName.Timeout);	
		//GD.Print("falled");
		DeadScreen.Visible = true;
		var timer2 = GetTree().CreateTimer(1.0f);
		await ToSignal(timer2, Timer.SignalName.Timeout);
		GetTree().Paused = false;
		//fallSound.Stop();
		GetTree().ReloadCurrentScene();
	}

	public void TimersManager()
	{
		if(dash_cd_timer > 0)
		{
			dash_cd_timer -= deltaTime;
		}
		if(dash_timer > 0)
		{
			dash_timer -= deltaTime;
		}
	}

	//Менеджер звуков - в архиве
	/* public AudioStreamPlayer SoundManager(int sound_id)
	{
		Dictionary<int, AudioStreamPlayer> sfxDic = new();
		sfxDic[1] = sfxLaning;

		AudioStreamPlayer sound = sfxDic[sound_id];
		if(sound.Playing)
		{
			sound.Stop();
		}
		return sound;
	}
	*/
}