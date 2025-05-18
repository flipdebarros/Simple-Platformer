using System;using UnityEngine;

public interface ICharacterBrain : IDisposable
{
	event Action OnJump;
	float MovementAxis { get; }

	void Initialize();
	void Enable();
	void Disable();
	void Update();
}