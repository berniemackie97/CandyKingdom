using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayReward : MonoBehaviour
{
    public Image check;
    public Image image;
    public Sprite CurrentDay;
    public Sprite AheadDay;
    public Sprite PassedDay;
    public TextMeshProUGUI count;
    public Color colorCurrent;
    public Color colorPassed;
    public Color colorAhead;


    public void SetDayAhead()
    {
        image.sprite = AheadDay;
        count.color = colorAhead;
        check.enabled = false;
    }
    public void SetCurrentDay()
    {
        image.sprite = CurrentDay;
        count.color = colorCurrent;
        check.enabled = false;
    }
    public void SetPassedDay()
    {
        image.sprite = PassedDay;
        count.color = colorPassed;
        check.enabled = true;
    }
}