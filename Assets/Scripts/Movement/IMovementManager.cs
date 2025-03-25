using UnityEngine;

public interface IMovementManager
{
	bool IsMoving { get; }
	
	void Move(float axis);
}