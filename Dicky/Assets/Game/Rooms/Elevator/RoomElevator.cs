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
		
		
		//Set position for entry walk
		C.Player.SetPosition(Point("Enter1"));
		
		//Music
		Audio.PlayMusic("SoundCool Style Carols", 2f);
		
		//Add Coffee
		I.Bucket.Add();
		
		//Hide Elevator Doors
		Prop("ElevatorClosed").Visible = false;
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
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		
		//Bell Sound
		Audio.Play("Sound514864__matrixxx__elevator-ping-03");
		
		//Door Pause
		yield return E.Wait(1);
		
		
		//Walk to center
		yield return C.Dicky.WalkTo(Point("Elevator1"));
		
		//Show Elevator Doors
		Prop("ElevatorClosed").Visible = true;
		
		//Room Change Pause
		yield return E.Wait(1);
		
		//Elevator Sound
		Audio.Play("Sound381577__midfag__engine-medium");
		
		Camera.Shake( 0.2f, 3, 15);
		
		yield return E.Wait(2);
		
		// Move the player to the room
		C.Player.Room = R.StaffRoom;
		
		yield return E.Break;
	}

	public IEnumerator OnUseInvHotspotPanel( IHotspot hotspot, IInventory item )
	{

		yield return E.Break;
	}

	public IEnumerator OnEnterRegionPurpleRoom( IRegion region, ICharacter character )
	{
		yield return C.Dicky.Say("I can't go home, I have work to do");
		yield return C.Player.WalkTo(Point("Elevator1"));
		yield return E.Break;
	}

	public IEnumerator OnEnterRegionBlueRoom( IRegion region, ICharacter character )
	{
		yield return C.Dicky.Say("This isn't the floor I work on");
		yield return C.Player.WalkTo(Point("Elevator1"));
		yield return E.Break;
	}
}