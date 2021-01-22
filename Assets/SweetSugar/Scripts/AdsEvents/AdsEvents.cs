using System;

public enum AdType
{
	AdmobInterstitial,
	UnityAdsVideo,
	ChartboostInterstitial,
	Appodeal
}

[Serializable]
public class AdEvents
{
	public GameState gameEvent;
	public AdType adType;
	public int everyLevel;
	//1.6
	public int calls;

}