#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using UnityEngine;

public class ItemChangerTest : MonoBehaviour
{
    private Item item;
    public void ShowMenuItems(Item _item)
    {
        item = _item;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (Application.platform != RuntimePlatform.OSXEditor) return;
        if (item != null)
        {
            if (GUILayout.Button("Select item"))
            {
                Selection.objects = new Object[] { item.gameObject };
            }       
//            if (GUILayout.Button("Show in console"))
//            {
//                Debug.Log("\nCPAPI:{\"cmd\":\"Search\" \"text\":\"" + item.instanceID + "\"}");
//            }
            if (GUILayout.Button("Select square"))
            {
                Selection.objects = new Object[] { item.square.gameObject };
            }
            foreach (var itemType in EnumUtil.GetValues<ItemsTypes>())
            {
                if (GUILayout.Button(itemType.ToString()))
                {
                    item.SetType(itemType);
                    // item.debugType = itemType;
                }
            }
            if (GUILayout.Button("Destroy"))
            {
                item.DestroyItem(true);
                LevelManager.THIS.FindMatches();
            }
        }
    }
#endif
}