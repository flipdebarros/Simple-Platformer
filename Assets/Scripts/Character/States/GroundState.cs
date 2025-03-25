using UnityEngine;

public class GroundState : IState
{
	readonly ICharacterBrain characterBrain;
	readonly IMovementManager movementManager;
	readonly IAnimationManager animationManager;

	public GroundState(
		ICharacterBrain characterBrain,
		IMovementManager movementManager,
		IAnimationManager animationManager
	)
	{
		this.characterBrain = characterBrain;
		this.movementManager = movementManager;
		this.animationManager = animationManager;
	}

	public void Enter()
	{
	}

	public void Exit()
	{
	}

	public void Update()
	{
		float movementAxis = characterBrain.MovementAxis;
		movementManager.Move(movementAxis);
		animationManager.Play(movementManager.IsMoving ? "king-run" : "king-idle");
	}
}