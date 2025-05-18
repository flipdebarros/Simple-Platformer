using System;
using UnityEngine;

[Serializable]
public class MovementSettings
{
	[field: SerializeField] 
	public Rigidbody2D Rigidbody2D { get; private set; }

	[field: SerializeField] 
	public float AccelerationTime { get; private set; }
	
	[field: SerializeField]
	public float DecelerationTime { get; private set; }

	[field: SerializeField] 
	public float TurnAccelerationTime { get; private set; }
	
	[field: SerializeField]
	public float Speed { get; private set; }
	
	[field: SerializeField]
	public float MovingThreshold { get; private set; }
	
}