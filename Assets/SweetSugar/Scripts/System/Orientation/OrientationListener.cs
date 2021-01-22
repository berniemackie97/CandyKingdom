using UnityEngine;

[ExecuteInEditMode]
public class OrientationListener : MonoBehaviour
{
    public delegate void OrientationChanged(ScreenOrientation orientation);
    public static event OrientationChanged OnOrientationChanged;

    private float previousAspect;
    public static ScreenOrientation previousOrientation;

    void OnEnable()
    {
        LevelManager.OnMapState += OnStateChanged;
        LevelManager.OnEnterGame += OnStateChanged;
    }


    void OnDisable()
    {
        LevelManager.OnMapState -= OnStateChanged;
        LevelManager.OnEnterGame -= OnStateChanged;

    }


    void OnStateChanged()
    {
        previousOrientation = ScreenOrientation.Unknown;
    }


    void Update()
    {
        // #if UNITY_EDITOR 
        var v = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        v = new Vector3(Screen.width, Screen.height, 0);
        var aspect = Screen.height / (float)Screen.width;
        // #endif
        // if (Application.isPlaying)
        // {
        //     if (LevelManager.This.gameStatus == GameState.WaitForPopup) return;
        // }
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft || v.x > v.y) && (ScreenOrientation.Landscape != previousOrientation) || aspect != previousAspect)
        {
            SetOrientation(ScreenOrientation.Landscape);
        }

        else if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight || v.x > v.y) && (ScreenOrientation.Landscape != previousOrientation) || aspect != previousAspect)
        {
            SetOrientation(ScreenOrientation.Landscape);
        }

        else if ((Input.deviceOrientation == DeviceOrientation.Portrait || v.x < v.y) && (ScreenOrientation.Portrait != previousOrientation) || aspect != previousAspect)
        {
            SetOrientation(ScreenOrientation.Portrait);
        }

        else if ((Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown || v.x < v.y) && (ScreenOrientation.Portrait != previousOrientation) || aspect != previousAspect)
        {
            SetOrientation(ScreenOrientation.Portrait);
        }

    }

    private void SetOrientation(ScreenOrientation orientation)
    {
        previousAspect = Screen.height / (float)Screen.width;
        previousOrientation = orientation;
        if (OnOrientationChanged != null)
            OnOrientationChanged(orientation);
    }
}