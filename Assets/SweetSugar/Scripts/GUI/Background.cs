using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
	public Sprite[] pictures;

	// Use this for initialization
	void OnEnable ()
	{
		if (LevelManager.THIS != null)
		{
			var backgroundSpriteNum = (int) (LevelManager.THIS.currentLevel / 20f - 0.01f);
			if(pictures.Length > backgroundSpriteNum)
				GetComponent<Image>().sprite = pictures[backgroundSpriteNum];
		}


	}


}
