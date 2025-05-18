using System;
using UnityEngine;

[Serializable] [Drawable]
public class SensesSettings
{
	[field: SerializeField] 
	public Rigidbody2D Rigidbody2D { get; private set; }
	
	[field: SerializeField] 
	public SpriteRenderer SpriteRenderer { get; private set; }
	
	[field: SerializeField] [field: DrawRect] 
	public Rect FloorDetectionRect { get; private set; }
	
	[field: SerializeField] [field: DrawRect] 
	public Rect WallDetectionRect { get; private set; }
	
	[field: SerializeField] 
	public bool FacingRight { get; set; }

	[field: SerializeField] 
	public LayerMask GroundLayer { get; private set; }
	
	[field: SerializeField] 
	public LayerMask WallLayer { get; private set; }
}