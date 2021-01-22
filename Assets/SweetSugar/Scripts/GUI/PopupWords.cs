using System;
using UnityEngine;

namespace UnityEngine.UI
{
    public class PopupWords : MonoBehaviour
    {
        private void Update()
        {
            transform.position = LevelManager.THIS.field.GetPosition();
        }
    }
}