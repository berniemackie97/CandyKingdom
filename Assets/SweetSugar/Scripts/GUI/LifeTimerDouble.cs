using TMPro;
using UnityEngine;

public class LifeTimerDouble : MonoBehaviour
{
    public TextMeshProUGUI textSource;
    public TextMeshProUGUI textDest;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textDest.text = "+1 life after " + textSource.text;
    }
}
