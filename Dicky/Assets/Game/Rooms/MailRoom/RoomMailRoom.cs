using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomMailRoom : RoomScript<RoomMailRoom>
{
	public IEnumerator OnEnterRoomAfterFade()
	{
		C.Player.SetPosition(98, -61);
		yield return C.Dicky.WalkTo(Point("EntryWalk"));
		
		
		
		if (R.MailRoom.FirstTimeVisited)
		 {
		yield return C.Dicky.FaceRight();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.FaceLeft();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.FaceRight();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.FaceLeft();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.Say("Where the heck is everyone?");
		yield return C.Dicky.Say("It's the day after Christmas");
		yield return C.Dicky.Say("Everyone should be hard at work");
		yield return E.WaitSkip(1.0f);
		//Show Mail
		Audio.Play("Sound73690__digifishmusic__air-hose-blowoff");
		Prop("Mail").Show();
		}
		yield return E.Break;
	}

	public void OnEnterRoom()
	{
		
		Audio.PlayAmbientSound("SoundIndustrial_Place");
		
		//Hide toolbar for web games
		G.Toolbar.Visible = false;
		G.Toolbar.Clickable = false;
		
		//Hide Mail till Dicky speaks
		Prop("Mail").Hide();
		
		//set entered true
		GlobalScript.Script.m_enteredMail = true;
	}

	public IEnumerator OnEnterRegionUpstairs( IRegion region, ICharacter character )
	{
		yield return C.Player.WalkTo(Point("Point2"));
		yield return E.Break;
	}

	public IEnumerator OnEnterRegionDownstairs( IRegion region, ICharacter character )
	{
		yield return C.Player.WalkTo(Point("Point1"));
		yield return E.Break;
	}

	public IEnumerator OnInteractPropMail( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.WalkToClicked();
		Prop("Mail").Hide();
		yield return C.Dicky.Say("It's a letter from Santa addressed to me	");
		yield return C.Display("Dicky, I have recieved an offer of being bought out by COMPANY NAME REDACTED be a good fella and turn off the naughty and nice machines as all orders are being fulffilled by them now. From Santa.");
		yield return E.WaitSkip();
		yield return C.Dicky.Say("Sold out to a faceless corporation");
		yield return C.Dicky.Say("Why would santa do this to us?");
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropMail( IProp prop )
	{
		yield return C.Dicky.Say("It's a piece of mail");
		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotMailTube( IHotspot hotspot )
	{
		yield return C.Dicky.Say("It's a pneumatic mail tube for recieving orders from Santa");
		yield return E.Break;
	}

	public IEnumerator OnInteractHotspotMailTube( IHotspot hotspot )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotMachineControl( IHotspot hotspot )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say("It's the control box for the Naughty and Nice machines");
		yield return E.Break;
	}

	public IEnumerator OnInteractHotspotMachineControl( IHotspot hotspot )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Dicky.Say("It's locked, I don't have the key");
		yield return C.Dicky.Say("Maybe there is someway to pry it open");
		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotNaughtySign( IHotspot hotspot )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("It's the sign for the Naughty Machine");
		yield return C.Dicky.Say("*cough* Incinerator *cough*");
		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotNiceSign( IHotspot hotspot )
	{
		yield return C.FaceClicked();
		yield return C.Dicky.Say("It's the sign for the Nice Machine");
		yield return C.Dicky.Say("I painted this");
		yield return E.Break;
	}

	public IEnumerator OnEnterRegionToStaffRoom( IRegion region, ICharacter character )
	{
		C.Player.ChangeRoom(R.StaffRoom);
		yield return E.Break;
	}

	public IEnumerator OnUseInvHotspotMachineControl( IHotspot hotspot, IInventory item )
	{
		if ( I.CandyCane.Active )
		{
			yield return C.WalkToClicked();
			yield return C.FaceClicked();
			yield return C.Dicky.Say("This oughta work");
			Audio.Play("SoundKnife-Attack-Metal_Impact_Clash_01");
			I.CandyCane.Remove();
			C.Player.ChangeRoom(R.Ending);
		}
		
		
		yield return E.Break;
	}

	public IEnumerator OnUseInvPropMail( IProp prop, IInventory item )
	{

		yield return E.Break;
	}

	public IEnumerator OnUseInvHotspotMailTube( IHotspot hotspot, IInventory item )
	{

		yield return E.Break;
	}
}