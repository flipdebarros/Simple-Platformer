using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputBrain : ICharacterBrain
{
	public event Action OnJump;
	public float MovementAxis { get; private set; }
	
	CharacterInputActions inputActions;
	InputAction moveAction;
	InputAction jumpAction;
	bool isEnabled;
	
	public void Initialize()
	{
		inputActions = new CharacterInputActions();
		moveAction = inputActions.Player.Move;
		jumpAction = inputActions.Player.Jump;
	}

	public void Enable()
	{
		AddListeners();
		inputActions.Enable();
		isEnabled = true;
	}

	public void Disable()
	{
		RemoveListeners();
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

	void AddListeners()
	{
		jumpAction.started += HandleJumpStarted;
	}

	void RemoveListeners()
	{
		jumpAction.started -= HandleJumpStarted;
	}

	void HandleJumpStarted(InputAction.CallbackContext context)
	{
		OnJump?.Invoke();
	}

	public void Dispose()
	{
		RemoveListeners();
		inputActions.Dispose();
	}
}