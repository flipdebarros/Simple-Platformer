using System;
using UnityEngine;

[Serializable]
public class JumpSettings
{
	[field: SerializeField] 
	public Rigidbody2D Rigidbody2D { get; private set; }
	
	[field: SerializeField] 
	public float Height { get; private set; }
	
	[field: SerializeField] 
	public float Reach { get; private set; }
	
	[field: SerializeField] 
	public float TerminalVelocity { get; private set; }
}