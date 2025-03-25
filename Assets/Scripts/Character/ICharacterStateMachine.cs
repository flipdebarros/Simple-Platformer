public interface ICharacterStateMachine
{
	void Initialize(IState initialState);
	void Update();
}