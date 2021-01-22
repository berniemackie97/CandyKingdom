using System;

public interface ILongDestroyable
{
	void SecondPartDestroyAnimation(Action callback);
	bool CanBeStarted();
	bool IsAnimationFinished();
	int GetPriority();

}