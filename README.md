# Simple Platformer

This project is a sample game with basic platformer functionalities. 

Obs: This is a work in progress.

## Specifications
**Game Engine:** Unity

**Version:** Unity 6 (6000.0.38f1)

**How To Test:** Play from _Sample Scene_

## Code Overview

### PlayerBehaviour

Inherits from `MonoBehaviour` and has the responsibility of centralizing the player character logic and its subsystems, and exposing their relevant parameters in the Inspector.
Its subsystems are: `CharacterInputBrain`, `CharacterSensesManager`, `JumpManager`, `MovementManager`, `AttackManager`, `AnimationManager` and `CharacterStateMachine`.

### ICharacterBrain

Defines the common interface between all the classes that implement how the character behaves, be it via input from the player or the IA for enemies.

### CharacterInputBrain

Inherits from `ICharacterBrain`. Inplements how the player input is read and used to control the player character.

### CharacterSensesManager

Implements the logic for detecting if character is grounded, is against a wall and other relationships between the character and the game world.

### JumpManager / MovementManager / AttackManager

Implements the modular logic for basic functionalities allowing every entity to use the same base implementation and avoid code duplication, giving a single point of access for changes.

### AnimationManager

Interfaces with the Animator and allows every state to interact with it.

### CharacterStateMachine

Implements the state machine that governs the change of state of the character's behaviour.

## Tools

### DrawRectTool

Uses reflection to find and draw in the scene view any Rect with the `DrawRect` attribute inside the Monobehavior script or inside any field or property of a class type with the `Drawable` attribute.

### UniqueCoroutine

Creates a wrapper class for a `MonoBehaviour` runner that allows any class (even if it doesn't inherits from `MonoBehavior`) to start a coroutine and avoids any duplication of the routine as it only permits a single (unique) routine running at a time.
