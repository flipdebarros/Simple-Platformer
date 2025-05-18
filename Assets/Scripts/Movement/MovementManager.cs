using UnityEngine;

public class MovementManager : IMovementManager
{
	public bool IsMoving => Mathf.Abs(Rigidbody2D.linearVelocity.x) > MovingThreshold;
	public float Speed => settings.Speed;

	Rigidbody2D Rigidbody2D => settings.Rigidbody2D;
	float Acceleration => Speed / settings.AccelerationTime;
	float Deceleration => Speed / settings.DecelerationTime;
	float TurnAcceleration => Speed / settings.TurnAccelerationTime;
	float MovingThreshold => settings.MovingThreshold;

	readonly MovementSettings settings;
	readonly ICharacterSensesManager sensesManager;

	public MovementManager(
		MovementSettings settings,
		ICharacterSensesManager sensesManager
	)
	{
		this.settings = settings;
		this.sensesManager = sensesManager;
	}

	public void Move(float axis)
	{
		Vector2 velocity = Rigidbody2D.linearVelocity;
		if (axis != 0f && sensesManager.IsOnWall(checkRightWall: axis > 0f))
		{
			Rigidbody2D.linearVelocity = new Vector2(0f, velocity.y);
			return;
		}

		float currentAcceleration = Acceleration;
		if (ChangedDirection(axis, velocity.x))
			currentAcceleration = TurnAcceleration;
		else if (axis == 0f)
			currentAcceleration = Deceleration;

		float speed = Mathf.MoveTowards(
			velocity.x,
			axis * Speed,
			Time.deltaTime * currentAcceleration
		);

		Rigidbody2D.linearVelocity = new Vector2(speed, velocity.y);
		sensesManager.FlipDirection(Rigidbody2D.linearVelocity.x);
	}

	static bool ChangedDirection(float axis, float velocity)
	{
		if (axis == 0f || velocity == 0f)
			return false;
		return Mathf.Sign(axis) * Mathf.Sign(velocity) < 0f;
	}
}