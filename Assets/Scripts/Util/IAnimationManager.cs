using System.Collections;

public interface IAnimationManager
{
	void Play(string animation);
	IEnumerator PlayAndWait(string animation);
	void PlayNext(string animation);
}