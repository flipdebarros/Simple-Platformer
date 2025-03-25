using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : IAnimationManager
{
	readonly Animator animator;
	readonly Dictionary<float, WaitForSeconds> waitForSecondsCache = new();

	readonly UniqueCoroutine animationRoutine;

	public AnimationManager(Animator animator, MonoBehaviour routineRunner)
	{
		this.animator = animator;
		animationRoutine = new UniqueCoroutine(routineRunner);
	}

	public void Play(string animation)
	{
		animator.Play(animation);
	}

	public IEnumerator PlayAndWait(string animation)
	{
		animator.Play(animation);
		float length = animator.GetCurrentAnimatorStateInfo(0).length;

		if (!waitForSecondsCache.ContainsKey(length))
			waitForSecondsCache[length] = new WaitForSeconds(length);
		yield return waitForSecondsCache[length];
	}

	public void PlayNext(string animation)
	{
		animationRoutine.Start(PlayNextRoutine(animation));
	}

	IEnumerator PlayNextRoutine(string animation)
	{
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		float time = stateInfo.normalizedTime;
		time -= (int)time;

		float remainingTime = time * stateInfo.length;
		while (remainingTime > 0)
		{
			remainingTime -= Time.deltaTime;
			yield return null;
		}

		animator.Play(animation);
	}
}