using TMPro;
using UnityEngine;

public class LifeShop : MonoBehaviour
{
	public int CostIfRefill = 12;
	// Use this for initialization
	void OnEnable ()
	{
		transform.Find ("Image/Buttons/BuyLife/Price").GetComponent<TextMeshProUGUI> ().text = "" + CostIfRefill;
		if (!LevelManager.THIS.enableInApps)
			transform.Find ("Image/Buttons/BuyLife").gameObject.SetActive (false);
		
	}
	
}
