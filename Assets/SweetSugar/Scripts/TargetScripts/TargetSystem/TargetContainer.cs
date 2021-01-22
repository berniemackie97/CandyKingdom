using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "TargetContainer", menuName = "TargetContainer", order = 1)]
public class TargetContainer
{
    public string name = "";
    // public TargetContainer target;
    [UsedImplicitly] public string Description = "";
    public string FailedDescription = "";
    public List<GameObject> prefabs = new List<GameObject>();

    public TargetContainer DeepCopy()
    {
        var other = (TargetContainer)MemberwiseClone();

        return other;
    }
}