using TMPro;
using UnityEngine;

public class MovesLabel : MonoBehaviour
{
    // Use this for initialization
    void OnEnable()
    {
        if(LevelManager.THIS?.levelData == null || !LevelManager.THIS.levelLoaded)
            LevelManager.OnLevelLoaded += Reset;
        else 
            Reset();
    }

    void OnDisable()
    {
        LevelManager.OnLevelLoaded -= Reset;
    }


    void Reset()
    {
        if (LevelManager.THIS != null && LevelManager.THIS.levelLoaded)
        {
            if (LevelManager.THIS.levelData.limitType == LIMIT.MOVES)
                GetComponent<TextMeshProUGUI>().text = "MOVES";
            else
                GetComponent<TextMeshProUGUI>().text = "TIME";
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
