using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BoostType
{
    ExtraMoves,
    Packages,
    Stripes,
    ExtraTime,
    Bomb,
    MulticolorCandy,
    FreeMove,
    ExplodeArea,
    Marmalade,
    None
}

public class BoostShop : MonoBehaviour
{
    // public Sprite[] icons;
    // public string[] descriptions;
    public int[] prices;
    public Image icon;
    public TextMeshProUGUI description;
    public TextMeshProUGUI boostName;
    private Action callback;

    BoostType boostType;

    public List<BoostProduct> boostProducts = new List<BoostProduct>();


    private void OnEnable()
    {
        foreach (var item in boostProducts.Select(i => i.boostIconObject))
        {
            item.SetActive(false);
        }
    }

    public void SetBoost(BoostProduct boost, Action callbackL)
    {
        boostType = boost.boostType;
        gameObject.SetActive(true);
        // icon.sprite = boost.icon;
        boost.boostIconObject.SetActive(true);
        description.text = boost.description;
        transform.Find("Image/BuyBoost/Count").GetComponent<TextMeshProUGUI>().text = "x" + boost.count;
        transform.Find("Image/BuyBoost/Price").GetComponent<TextMeshProUGUI>().text = "" + boost.GemPrices;
        boostName.text = boost.name;
        callback = callbackL;
    }

    [UsedImplicitly]
    public void BuyBoost(GameObject button)
    {
        var count = int.Parse(button.transform.Find("Count").GetComponent<TextMeshProUGUI>().text.Replace("x", ""));
        var price = int.Parse(button.transform.Find("Price").GetComponent<TextMeshProUGUI>().text);
        GetComponent<AnimationEventManager>().BuyBoost(boostType, price, count, callback);
    }
}

[Serializable]
public class BoostProduct
{
    public BoostType boostType;
    public Sprite icon;
    public string description;
    public string name;
    public int count;
    public int GemPrices;
    public GameObject boostIconObject;

}