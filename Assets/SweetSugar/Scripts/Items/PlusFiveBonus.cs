using System;
using DefaultNamespace;

namespace UnityEngine
{
    public class PlusFiveBonus : UnityEngine.MonoBehaviour
    {
        public GameObject sprite;
        public Animation animation;
        public void Destroy(){
            transform.parent = null;
            sprite.GetComponent<BindSortingOrder>().enabled = false;
            animation.clip.legacy = true;
            animation.Play();
            Destroy(gameObject, 1);
            if (LevelManager.THIS.gameStatus == GameState.Playing)
                LevelManager.THIS.levelData.limit += 5;
        }
    }
}