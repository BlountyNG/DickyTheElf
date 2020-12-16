using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomElevator : RoomScript<RoomElevator>
{


	public void OnEnterRoom()
	{
		C.Player.SetPosition(Point("Enter1"));
	}

	public IEnumerator OnWalkTo()
	{

		yield return E.Break;
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		yield return C.Dicky.WalkTo(Point("Elevator1"));
		yield return C.Dicky.FaceRight();
		yield return E.Break;
		
	}

	public IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotPanel( IHotspot hotspot )
	{
		yield return C.Player.WalkTo( E.GetMousePosition() );
		yield return C.Player.FaceUp();
		yield return C.Dicky.Say("It's the elevator control panel");
		yield return E.Break;
	}
}