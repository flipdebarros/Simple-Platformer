using UnityEngine;

public class CharacterSensesManager : ICharacterSensesManager
{
	Rigidbody2D Rigidbody2D => settings.Rigidbody2D;
	SpriteRenderer SpriteRenderer => settings.SpriteRenderer;
	Rect FloorDetectionRect => settings.FloorDetectionRect;
	Rect WallDetectionRect => settings.WallDetectionRect;
	LayerMask GroundLayer => settings.GroundLayer;
	LayerMask WallLayer => settings.WallLayer;

	bool FacingRight
	{
		get => settings.FacingRight;
		set => settings.FacingRight = value;
	}

	readonly SensesSettings settings;
	readonly ICharacterBrain characterBrain;

	public CharacterSensesManager(SensesSettings settings)
	{
		this.settings = settings;
	}

	public void FlipDirection(float horizontal)
	{
		if (horizontal == 0f ||
		    FacingRight && horizontal > 0f ||
		    !FacingRight && horizontal < 0f)
			return;

		FacingRight = !FacingRight;
		SpriteRenderer.flipX = !FacingRight;

		Vector3 pos = SpriteRenderer.transform.localPosition;
		pos.x *= -1f;
		SpriteRenderer.transform.localPosition = pos;
	}

	public bool IsGrounded()
	{
		if (Rigidbody2D.linearVelocity.y > 0f)
			return false;

		Vector3 origin = FloorDetectionRect.center + (Vector2)Rigidbody2D.transform.position;
		RaycastHit2D hit = Physics2D.BoxCast(
			origin,
			FloorDetectionRect.size,
			0f,
			Vector3.forward,
			Mathf.Infinity,
			GroundLayer
		);
		return hit.collider && hit.normal.y > 0f;
	}

	public bool IsOnWall(bool checkRightWall)
	{
		Vector3 origin = GetWallDetectionOrigin(checkRightWall);
		RaycastHit2D hit = Physics2D.BoxCast(
			origin,
			WallDetectionRect.size,
			0f,
			Vector3.forward,
			Mathf.Infinity,
			WallLayer
		);
		return hit.collider;
	}

	Vector3 GetWallDetectionOrigin(bool checkRightWall)
	{
		Vector3 origin = WallDetectionRect.center;
		if (!checkRightWall)
		{
			origin.x *= -1f;
			origin.x -= WallDetectionRect.width / 2f;
		}
		origin += Rigidbody2D.transform.position;
		return origin;
	}
}