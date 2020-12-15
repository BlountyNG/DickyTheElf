using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomElevator : RoomScript<RoomElevator>
{


	public void OnEnterRoom()
	{
	}

	public IEnumerator OnWalkTo()
	{

		yield return E.Break;
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		yield return C.Dicky.WalkTo(Point("Elevator1"));
		yield return E.Break;
		
	}
}