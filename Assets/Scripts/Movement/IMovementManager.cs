using UnityEngine;

public interface IMovementManager
{
	float Speed { get; }
	bool IsMoving { get; }

	void Move(float axis);
}