using UnityEngine;

public class CharacterStateMachine : ICharacterStateMachine
{

	IMovementManager movementManager;
	
	IState CurrentState
	{
		get => currentState;
		set
		{
			currentState?.Exit();
			currentState = value;
			currentState?.Enter();
		}
	}
	IState currentState;

	public void Initialize(IState initialState)
	{
		CurrentState = initialState;
	}

	public void Update()
	{
		CurrentState?.Update();
	}
}