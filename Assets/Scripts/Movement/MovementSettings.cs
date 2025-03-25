using System;
using UnityEngine;

[Serializable] [Drawable]
public class MovementSettings
{
	[field: SerializeField] 
	public Rigidbody2D Rigidbody2D { get; private set; }

	[field: SerializeField] 
	public SpriteRenderer SpriteRenderer { get; private set; }

	[field: SerializeField] 
	public float MovementAccelerationTime { get; private set; }
	
	[field: SerializeField]
	public float MovementDecelerationTime { get; private set; }

	[field: SerializeField] 
	public float MovementTurnAccelerationTime { get; private set; }
	
	[field: SerializeField]
	public float MovementSpeed { get; private set; }
	
	[field: SerializeField]
	public float MovingThreshold { get; private set; }

	[field: SerializeField] 
	public bool FacingRight { get; set; }
}