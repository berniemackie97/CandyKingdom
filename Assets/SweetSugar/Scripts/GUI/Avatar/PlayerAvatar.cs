using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour, IAvatarLoader
{
    public Image image;

    void Start()
    {
        image.enabled = false;
        var lastReachedLevel = LevelsMap._instance.GetMapLevels().Where(l => !l.IsLocked).Last();
        if (lastReachedLevel) lastReachedLevel.transform.Find("Idle").gameObject.SetActive(true);
    }

#if PLAYFAB || GAMESPARKS
	void OnEnable () {
		NetworkManager.OnPlayerPictureLoaded += ShowPicture;
        LevelsMap.LevelReached += OnLevelReached;
        if(NetworkManager.profilePic != null) ShowPicture();
	}

	void OnDisable () {
		NetworkManager.OnPlayerPictureLoaded -= ShowPicture;
        LevelsMap.LevelReached -= OnLevelReached;

	}


#endif
    public void ShowPicture()
    {
        image.sprite = NetworkManager.profilePic;
        image.enabled = true;
    }

    private void OnLevelReached(object sender, LevelReachedEventArgs e)
    {
        Debug.Log(string.Format("Level {0} reached.", e.Number));
    }

}
