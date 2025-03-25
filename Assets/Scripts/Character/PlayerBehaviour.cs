using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] MovementSettings movementSettings;

	ICharacterStateMachine stateMachine;
	ICharacterBrain characterBrain;
	IMovementManager movementManager;
	IAnimationManager animationManager;

	IState groundState;

	void Awake()
	{
		characterBrain = new CharacterInputBrain();
		characterBrain.Initialize();

		movementManager = new MovementManager(movementSettings);

		animationManager = new AnimationManager(animator, this);
		
		groundState = new GroundState(characterBrain, movementManager, animationManager);
		
		stateMachine = new CharacterStateMachine();
		stateMachine.Initialize(groundState);
	}

	void OnEnable()
	{
		characterBrain.Enable();
	}

	void OnDisable()
	{
		characterBrain.Disable();
	}

	void Update()
	{
		characterBrain.Update();
		stateMachine.Update();
	}

	void OnDestroy()
	{
		characterBrain.Dispose();
	}
}