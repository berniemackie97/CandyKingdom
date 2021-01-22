using System.Collections.Generic;
using UnityEngine;

namespace Sweet_sugar.Assets.SweetSugar.Scripts.Integrations
{
    public class DefineHidder : MonoBehaviour
    {

        private void Start()
        {
#if !USE_GETSOCIAL_UI
            gameObject.SetActive(false);
#endif
        }
    }
}