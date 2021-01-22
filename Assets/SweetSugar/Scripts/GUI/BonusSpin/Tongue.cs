using UnityEngine;

public class Tongue : MonoBehaviour
{
	public AudioClip clip;

	private void OnCollisionEnter2D(Collision2D other)
	{
		SoundBase.Instance.PlayOneShot( clip );

	}
}
