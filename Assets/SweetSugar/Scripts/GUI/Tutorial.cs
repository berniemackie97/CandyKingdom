using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorials;
    int i;

    private void OnEnable()
    {
        foreach (var item in tutorials)
        {
            item.SetActive(false);
        }
        SetTutorial();
    }

    public void Next()
    {
        tutorials[i].SetActive(false);
        i++;
        i = Mathf.Clamp(i, 0, tutorials.Length - 1);
        SetTutorial();
    }
    public void Back()
    {
        tutorials[i].SetActive(false);
        i--;
        i = Mathf.Clamp(i, 0, tutorials.Length - 1);
        SetTutorial();
    }

    void SetTutorial()
    {
        tutorials[i].SetActive(true);
    }
}
