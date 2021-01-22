using System.Linq;
using UnityEngine;

public static class AnimatorUtils
{

	public static void SetBoolScaled(this Animator anim, string clipName, bool value)
	{
		anim.SetBool(clipName, value);

	}
}
