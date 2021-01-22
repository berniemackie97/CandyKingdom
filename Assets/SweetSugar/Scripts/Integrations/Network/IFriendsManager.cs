using System;
using System.Collections.Generic;

public interface IFriendsManager {
	void GetFriends (Action<Dictionary<string,string>> Callback) ;

	void PlaceFriendsPositionsOnMap (Action<Dictionary<string,int>> Callback);

	void GetLeadboardOnLevel (int LevelNumber, Action<List<LeadboardPlayerData>> Callback);

	void Logout ();
}


