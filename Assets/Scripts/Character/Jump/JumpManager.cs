using UnityEngine;

public class JumpManager : IJumpManager
{
	float AirTime => settings.Reach / movementManager.Speed;
	float G => -8.0f * settings.Height / (AirTime * AirTime);
	float InitialVelocity => 4.0f * settings.Height / AirTime;
	
	readonly JumpSettings settings;
	readonly IMovementManager movementManager;
	readonly ICharacterSensesManager sensesManager;

	public JumpManager(
		JumpSettings settings,
		IMovementManager movementManager,
		ICharacterSensesManager sensesManager
	)
	{
		this.settings = settings;
		this.movementManager = movementManager;
		this.sensesManager = sensesManager;
	}

	public void Initialize()
	{
		settings.Rigidbody2D.gravityScale = G / Physics2D.gravity.y;
	}

	public void Jump()
	{
		Vector2 velocity = settings.Rigidbody2D.linearVelocity;
		settings.Rigidbody2D.linearVelocity = new Vector2(velocity.x, InitialVelocity);
	}

	public void Fall()
	{
	}
}