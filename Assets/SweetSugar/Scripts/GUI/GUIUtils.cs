using SweetSugar.Scripts.System;

namespace UnityEngine.UI
{
    public class GUIUtils : MonoBehaviour
    {
        public static GUIUtils THIS;

        private void Start()
        {
            if (!Equals(THIS, this)) THIS = this;
        }

        public void StartGame()
        {
            if (InitScript.lifes > 0)
            {
                InitScript.Instance.SpendLife(1);
                LevelManager.THIS.gameStatus = GameState.PrepareGame;
            }
            else
            {
                BuyLifeShop();
            }

        }

        public void BuyLifeShop()
        {

            if (InitScript.lifes < InitScript.Instance.CapOfLife)
                MenuReference.THIS.LiveShop.gameObject.SetActive(true);

        }
    }
}