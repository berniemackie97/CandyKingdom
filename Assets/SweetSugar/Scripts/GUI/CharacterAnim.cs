using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
	public Animator anim;

	private void OnEnable()
	{
		anim.SetTrigger("Game");
		LevelManager.OnCombo += OnCombo;
	}

	private void OnDisable()
	{
		LevelManager.OnCombo -= OnCombo;

	}

	void OnCombo()
	{
		anim.SetTrigger("Cool");

	}
}
