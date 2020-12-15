using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomMailRoom : RoomScript<RoomMailRoom>
{
	public IEnumerator OnEnterRoomAfterFade()
	{
		yield return C.Dicky.WalkTo(Point("EntryWalk"));
		yield return E.Break;
	}

	public void OnEnterRoom()
	{
	}
}