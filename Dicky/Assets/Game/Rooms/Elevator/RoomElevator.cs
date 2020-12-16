using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomElevator : RoomScript<RoomElevator>
{


	public void OnEnterRoom()
	{
		
		//Hide toolbar for web games
		G.Toolbar.Visible = false;
		G.Toolbar.Clickable = false;
		
		// Hide the elevator doors
		Prop("ElevatorClosed").Visible = false;
		
		//Set position for entry walk
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
		yield return C.Dicky.Say("Oh boy, another day in the north pole mail room");
		yield return C.Dicky.Say("Best take the elevator down to the staff room so I can put on my work clothes");
		yield return E.Break;
		
	}

	public IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotPanel( IHotspot hotspot )
	{
		//C.Player.WalkTo( E.GetMousePosition() );
		yield return C.Player.FaceClicked();
		yield return C.Dicky.Say("It's the elevator control panel");
		yield return E.Break;
	}

	public IEnumerator OnInteractHotspotPanel( IHotspot hotspot )
	{
		//Walk to panel
		yield return C.Player.WalkTo( E.GetMousePosition() );
		
		// Show the elevator doors
		Prop("ElevatorClosed").Visible = true;
		Prop("ElevatorOpen").Visible = false;
		yield return E.Break;
	}
}