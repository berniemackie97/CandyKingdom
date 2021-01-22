using System.Collections;
using UnityEditor;

namespace AnyFieldInspector
{
    public class AnyFieldEditor : Editor
    {
        public void ShowList(IList list)
        {
            GuiList.Show(list);
        }
    }
}