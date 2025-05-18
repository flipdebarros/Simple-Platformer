using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] MovementSettings movementSettings;
	[SerializeField] SensesSettings sensesSettings;
	[SerializeField] JumpSettings jumpSettings;

	ICharacterStateMachine stateMachine;
	ICharacterBrain characterBrain;
	IMovementManager movementManager;
	IAnimationManager animationManager;
	ICharacterSensesManager sensesManager;
	IJumpManager jumpManager;

	IState groundState;

	void Awake()
	{
		characterBrain = new CharacterInputBrain();
		characterBrain.Initialize();

		sensesManager = new CharacterSensesManager(sensesSettings);
		
		movementManager = new MovementManager(movementSettings, sensesManager);

		jumpManager = new JumpManager(jumpSettings, movementManager, sensesManager);
		jumpManager.Initialize();

		animationManager = new AnimationManager(animator, this);
		
		groundState = new GroundState(characterBrain, movementManager, animationManager, sensesManager, jumpManager);
		
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