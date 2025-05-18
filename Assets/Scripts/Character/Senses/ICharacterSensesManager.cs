public interface ICharacterSensesManager
{
	void FlipDirection(float horizontal);
	bool IsGrounded();
	bool IsOnWall(bool checkRightWall);

}