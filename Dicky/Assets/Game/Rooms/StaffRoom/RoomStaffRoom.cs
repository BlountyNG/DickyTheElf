using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomStaffRoom : RoomScript<RoomStaffRoom>
{


	public void OnEnterRoom()
	{
		
		
		//Hide toolbar for web games
		G.Toolbar.Visible = false;
		G.Toolbar.Clickable = false;
		
		//Elevator Sound stop
		Audio.Stop("Sound381577__midfag__engine-medium");
		
		//Set position for entry walk
		C.Player.SetPosition(Point("Point1"));
		
		//Hide locker
		Prop("OpenLocker").Visible = false;
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		//Walk into room
		yield return C.Plr.WalkTo(Point("Point0"));
		yield return E.Break;
	}

	public IEnumerator OnInteractPropBanner( IProp prop )
	{

		yield return E.Break;
	}
}