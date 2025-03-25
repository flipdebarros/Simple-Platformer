using System;using UnityEngine;

public interface ICharacterBrain : IDisposable
{
	float MovementAxis { get; }

	void Initialize();
	void Enable();
	void Disable();
	void Update();
}