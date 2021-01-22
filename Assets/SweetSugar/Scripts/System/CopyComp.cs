using UnityEngine;

public class CopyComp
{
    public static GameObject Copy(Component original, GameObject destination)
    {
        var type = original.GetType();
        Debug.Log(type);
        var copy = destination.AddComponent(type);
        var fields = type.GetFields();
        Debug.Log(fields.Length);
        foreach (var field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return destination;
    }
}