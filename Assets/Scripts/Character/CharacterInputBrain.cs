using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputBrain : ICharacterBrain
{
	public float MovementAxis { get; private set; }
	
	CharacterInputActions inputActions;
	InputAction moveAction;
	bool isEnabled;
	
	public void Initialize()
	{
		inputActions = new CharacterInputActions();
		moveAction = inputActions.Player.Move;
	}

	public void Enable()
	{
		inputActions.Enable();
		isEnabled = true;
	}

	public void Disable()
	{
		inputActions.Disable();
		isEnabled = false;
	}

	public void Update()
	{
		if(!isEnabled)
			return;
		
		ReadInput();
	}

	void ReadInput()
	{
		MovementAxis = moveAction.ReadValue<float>();
	}

	public void Dispose()
	{
		inputActions.Dispose();
	}
}