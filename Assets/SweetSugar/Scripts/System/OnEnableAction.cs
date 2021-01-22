using UnityEngine;
using UnityEngine.Events;

public class OnEnableAction : MonoBehaviour
{
    public UnityEvent[] myEvent;
 
    void OnEnable()
    {
        foreach (var unityEvent in myEvent)
        {
            unityEvent.Invoke();
            
        }
    }
}