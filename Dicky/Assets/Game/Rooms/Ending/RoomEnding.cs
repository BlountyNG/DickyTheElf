using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomEnding : RoomScript<RoomEnding>
{


	public void OnEnterRoom()
	{
		if ( I.Bucket.Owned )
		{
					//Unlock Hard Worker medal
			GameObject.Find("NGHelper").GetComponent<NGHelper>().unlockMedal(61619);
		}
		
		// Hide the inventory and info bar in the title
		G.InfoBar.Visible = false;
		G.Inventory.Visible = false;
		G.InfoBar.Clickable = false;
		G.Inventory.Clickable = false;
		
		//Hide toolbar for web games
		G.Toolbar.Visible = false;
		G.Toolbar.Clickable = false;
	}
}