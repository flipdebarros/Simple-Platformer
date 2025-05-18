using UnityEngine;

public class GroundState : IState
{
	readonly ICharacterBrain characterBrain;
	readonly IMovementManager movementManager;
	readonly IAnimationManager animationManager;
	readonly ICharacterSensesManager sensesManager;
	readonly IJumpManager jumpManager;

	public GroundState(
		ICharacterBrain characterBrain,
		IMovementManager movementManager,
		IAnimationManager animationManager,
		ICharacterSensesManager sensesManager,
		IJumpManager jumpManager
	)
	{
		this.characterBrain = characterBrain;
		this.movementManager = movementManager;
		this.animationManager = animationManager;
		this.sensesManager = sensesManager;
		this.jumpManager = jumpManager;
	}

	public void Enter()
	{
		AddListeners();
	}

	public void Exit()
	{
		RemoveListeners();
	}

	public void Update()
	{
		float movementAxis = characterBrain.MovementAxis;
		movementManager.Move(movementAxis);
		animationManager.Play(movementManager.IsMoving ? "king-run" : "king-idle");
	}

	void AddListeners()
	{
		characterBrain.OnJump += HandleJump;
	}

	void RemoveListeners()
	{
		characterBrain.OnJump -= HandleJump;
	}

	void HandleJump()
	{
		if (sensesManager.IsGrounded())
		{
			jumpManager.Jump();
		}
	}
}