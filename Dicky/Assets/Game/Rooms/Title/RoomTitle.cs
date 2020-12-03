using UnityEngine;
using System.Collections;
using PowerScript;
using PowerTools.Quest;

public class RoomTitle : RoomScript<RoomTitle>
{
	public void OnEnterRoom()
	{
		
		// Hide the inventory and info bar in the title
		G.InfoBar.Visible = false;
		G.Inventory.Visible = false;
		G.InfoBar.Clickable = false;
		G.Inventory.Clickable = false;
		
		// Play Intro Music
		Audio.PlayMusic("SoundRetro Traditional Theme", 1, 3f);
		
		
		//Player Footstep sound
		C.Player.FootstepSound = "SoundFootsteps_Casual_Earth_01";
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		
		// Start cutscene, so this can be skipped by pressing ESC
		E.StartCutscene();
		
		// Wait a second
		yield return E.Wait(1.0f);
		
		// Fade in the title prop
		Prop("Title").Visible = true;
		yield return Prop("Title").Fade(0,1,1.0f);
		
		yield return E.Wait(0.5f);
		
		// Check if we have any save games. If so, turn on the "continue" prop.
		if (  E.GetSaveSlotData().Count > 0 )
		{
			// Enable the "Continue" prop and start it fading in
			Prop("Continue").Enable();
			Prop("Continue").FadeBG(0,1,1.0f);		
		}
		
		// Turn on the "new game" prop and fade it in
		Prop("New").Enable();
		yield return Prop("New").Fade(0,1,1.0f);
		
		// This is the point the game will skip to if ESC is pressed
		E.EndCutscene();
	}


	public IEnumerator OnInteractPropNew( Prop prop )
	{
		
		// Turn on the inventory and info bar now that we're starting a game
		G.InfoBar.Visible = true;
		G.Inventory.Visible = true;
		G.InfoBar.Clickable = true;
		G.Inventory.Clickable = true;
		
		// Move the player to the room
		C.Player.Room = R.Forest;
		yield return E.Break;
	}

	public IEnumerator OnInteractPropContinue( Prop prop )
	{
	    E.RestoreSave(1);
		yield return E.Break;
	}



	public IEnumerator OnLookAtPropContinue( IProp prop )
	{

		yield return E.Break;
	}

	public IEnumerator OnUseInvPropContinue( IProp prop, IInventory item )
	{

		yield return E.Break;
	}

	public void Update()
	{
	}

	public IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{
		Audio.StopMusic(2f);
		yield return E.Break;
	}

	public IEnumerator OnAnyClick()
	{

		yield return E.Break;
	}

	public IEnumerator OnWalkTo()
	{

		yield return E.Break;
	}

	public void OnPostRestore( int version )
	{
	}

	public IEnumerator OnLookAtPropNew( IProp prop )
	{

		yield return E.Break;
	}

	public IEnumerator OnUseInvPropNew( IProp prop, IInventory item )
	{

		yield return E.Break;
	}
}