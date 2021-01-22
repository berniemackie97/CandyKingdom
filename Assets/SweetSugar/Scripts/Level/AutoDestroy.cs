using UnityEngine;

namespace Sweet_sugar.Assets.SweetSugar.Scripts.Level
{
    public class AutoDestroy : MonoBehaviour {
        public float sec;
        private void Start() {
            Destroy(gameObject,sec);
        }
    }
}