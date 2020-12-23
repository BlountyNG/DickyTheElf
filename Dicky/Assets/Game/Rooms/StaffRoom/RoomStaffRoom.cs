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
		if (C.Player.LastRoom == R.MailRoom)
			{
			C.Player.SetPosition(Point("Point2"));
			}
		else
			{
			C.Player.SetPosition(Point("Point1"));
			}
		
		
		//Hide locker
		Prop("OpenLocker").Visible = false;
		Prop("OpenLocker").Clickable = false;
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		//Walk into room
		if (C.Player.LastRoom == R.MailRoom)
			{
			yield return C.Plr.WalkTo(Point("Point3"));
			}
		else
			{
			yield return C.Plr.WalkTo(Point("Point0"));
			}
		
		yield return E.Break;
	}

	public IEnumerator OnInteractPropBanner( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.WalkToClicked();
		yield return C.Dicky.Say("I can't reach it");
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropBanner( IProp prop )
	{
		
		if (Prop("Banner").FirstLook)
			{
			yield return C.Dicky.Say("It's the Christmas Banner");
			yield return C.Dicky.Say("Still up from last Christmas");
			yield return C.Dicky.Say("and the Christmas before");
			}
		else 
			{
			yield return C.Dicky.Say(" and the one before");
			}
		yield return E.Break;
	}

	public IEnumerator OnInteractPropLockers( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say("Ok time to put in my code");
		
		if (GlobalScript.Script.m_lockerCode)
		{
		
		yield return C.Dicky.Say("0");
		yield return E.WaitSkip(1.5f);
		yield return C.Dicky.Say("0");
		yield return E.WaitSkip(1.5f);
		yield return C.Dicky.Say("3");
		yield return E.WaitSkip(1.5f);
		yield return C.Dicky.Say("9");
		yield return C.Dicky.Say(" Done");
		Audio.Play("SoundMicrowave_Open");
		Prop("OpenLocker").Visible = true;
		Prop("OpenLocker").Clickable = true;
		
		Prop("Lockers").Clickable = false;
		}
		
		else
		{
		yield return C.Dicky.Say("What was it again...");
		yield return C.Dicky.Say("It's the same price as my favourite snack from the vending machine");
		yield return C.Dicky.Say("If only I could remember what that was");
		yield return C.Dicky.Say("Darn my small elf memory");
		}
		yield return E.Break;
	}

	public IEnumerator OnInteractPropOpenLocker( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return E.FadeOut(1);
		yield return E.FadeIn(1);
		yield return C.Dicky.Say("Ah it feels good to be in uniform again");
		GlobalScript.Script.m_changedClothes = true;
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropOpenLocker( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say(" It's my locker");
		yield return C.Dicky.Say(" My work uniform is in here");
		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotSockets( IHotspot hotspot )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("It's the electrical sockets");
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropLights( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("Christmas lights");
		yield return C.Dicky.Say("They don't work");
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropLockers( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("My locker is over there");
		yield return E.Break;
	}

	public IEnumerator OnEnterRegionToMailRoom( IRegion region, ICharacter character )
	{
		if (GlobalScript.Script.m_enteredMail)
			{
			// Move the player to the room
			C.Player.Room = R.MailRoom;
			}
		
		else if (GlobalScript.Script.m_changedClothes)
			{
			yield return C.Dicky.Say("Oh boy, work time");
			// Move the player to the room
			C.Player.Room = R.MailRoom;
			}
			
		else
			{
			yield return C.Dicky.Say("I need to get changed first");
			}
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropChairs( IProp prop )
	{
		yield return C.Dicky.Say("A cheap plastic chair");
		yield return C.Dicky.Say("I sit there when I have my break");
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropTable( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say("The break table");
		yield return C.Dicky.Say("It's still sticky from where I spilled my cinammon frappe-latte last week");
		yield return E.Break;
	}

	public IEnumerator OnInteractHotspotSockets( IHotspot hotspot )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtPropVendingMachine( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("Ooh snacks!");
		yield return E.Break;
	}

	public IEnumerator OnInteractPropChairs( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("It's not break time yet");
		yield return E.Break;
	}

	public IEnumerator OnInteractPropLights( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.WalkToClicked();
		yield return C.Dicky.Say("I can't reach them");
		yield return C.Dicky.Say("They don't work anyway");
		yield return E.Break;
	}

	public IEnumerator OnInteractPropVendingMachine( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		
		if (Prop("Lockers").UseCount < 1)
		{
		yield return C.Dicky.Say("The company vending machine");
		}
		
		else 
		{
		yield return C.Dicky.Say("This vending machine sells my favourite snack");
		yield return C.Dicky.Say("Packets of sugar");
		yield return C.Dicky.Say("They cost 39 elfies");
		yield return C.Player.FaceDown();
		yield return C.Dicky.Say("That's the currency Santa pays us in");
		yield return C.Dicky.Say("It's only legal tender in the North Pole");
		GlobalScript.Script.m_lockerCode = true;
		}
		yield return E.Break;
	}

	public IEnumerator OnInteractPropTable( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say("Sticky");
		yield return E.Break;
	}

	public IEnumerator OnExitRegionToMailRoom( IRegion region, ICharacter character )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtPropCandyCane( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.WalkToClicked();
		yield return C.Dicky.Say("It's a candy cane");
		yield return C.Dicky.Say("YUMMY!");
		yield return E.Break;
	}

	public IEnumerator OnInteractPropCandyCane( IProp prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say("Don't mind if i do");
		Audio.Play("SoundGetItem1");
		Prop("CandyCane").Hide();
		I.CandyCane.Add();
		
		yield return E.Break;
	}

	public IEnumerator OnUseInvPropCandyCane( IProp prop, IInventory item )
	{

		yield return E.Break;
	}
}