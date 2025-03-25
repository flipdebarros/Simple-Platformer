using System.Collections;
using UnityEngine;

public class UniqueCoroutine
{
	public bool IsRunning { get; private set; }

	readonly MonoBehaviour runner;

	Coroutine currentRoutine;

	public UniqueCoroutine(MonoBehaviour runner)
	{
		this.runner = runner;
	}

	public void Start(IEnumerator routine)
	{
		DisposeCurrentRoutine();
		currentRoutine = runner.StartCoroutine(RunnerRoutine(routine));
	}

	public void Stop()
	{
		DisposeCurrentRoutine();
	}

	void DisposeCurrentRoutine()
	{
		if (currentRoutine == null)
			return;

		runner.StopCoroutine(currentRoutine);
		currentRoutine = null;
		IsRunning = false;
	}

	IEnumerator RunnerRoutine(IEnumerator routine)
	{
		IsRunning = true;
		yield return routine;
		IsRunning = false;
	}
}