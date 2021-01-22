namespace SweetSugar.Scripts.Items
{
    public class GameBlocker : UnityEngine.MonoBehaviour
    {
        private void Start()
        {
            LevelManager.THIS._stopFall.Add(this);
        }

        private void OnDisable()
        {
            Destroy(this);
        }

        private void OnDestroy()
        {
            LevelManager.THIS._stopFall.Remove(this);
        }
    }
}