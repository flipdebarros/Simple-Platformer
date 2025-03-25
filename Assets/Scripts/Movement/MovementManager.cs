using UnityEngine;

public class MovementManager : IMovementManager
{
	public bool IsMoving => Mathf.Abs(Rigidbody2D.linearVelocity.x) > MovingThreshold;
	
	Rigidbody2D Rigidbody2D => settings.Rigidbody2D;
	SpriteRenderer SpriteRenderer => settings.SpriteRenderer;
	float Speed => settings.MovementSpeed;
	float Acceleration => Speed / settings.MovementAccelerationTime;
	float Deceleration => Speed / settings.MovementDecelerationTime;
	float TurnAcceleration => Speed / settings.MovementTurnAccelerationTime;
	float MovingThreshold => settings.MovingThreshold;

	bool FacingRight
	{
		get => settings.FacingRight;
		set => settings.FacingRight = value;
	}

	readonly MovementSettings settings;

	public MovementManager(MovementSettings settings)
	{
		this.settings = settings;
	}

	public void Move(float axis)
	{
		Vector2 velocity = Rigidbody2D.linearVelocity;
		
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

		Rigidbody2D.linearVelocity = Vector2.right * speed;

		FlipDirection(Rigidbody2D.linearVelocity.x);
	}

	void FlipDirection(float horizontal)
	{
		if (horizontal == 0f ||
		    FacingRight && horizontal > 0f ||
		    !FacingRight && horizontal < 0f)
			return;

		FacingRight = !FacingRight;
		SpriteRenderer.flipX = !FacingRight;

		Vector3 pos = SpriteRenderer.transform.localPosition;
		pos.x *= -1f;
		SpriteRenderer.transform.localPosition = pos;
	}

	static bool ChangedDirection(float axis, float velocity)
	{
		if (axis == 0f || velocity == 0f)
			return false;
		return Mathf.Sign(axis) * Mathf.Sign(velocity) < 0f;
	}
}