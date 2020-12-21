using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomForest : RoomScript<RoomForest>
{
	// You can put variables here theat you want to use for game logic, and be saved in the room
	int m_timesClickedSky = 0;

	public void OnEnterRoom()
	{

	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		yield return C.Dave.Say("Well, I guess this is a test project for an adventure game");
		yield return C.Dave.WalkTo(Point("EntryWalk"));
		yield return C.Dave.Say("Sure looks adventurey!");
		Audio.PlayMusic("MusicExample");
		yield return E.Break;
	}

	public IEnumerator OnInteractHotspotForest( Hotspot hotspot )
	{
		yield return C.Dave.WalkTo( E.GetMousePosition() );
		C.Dave.FaceUp();
		yield return C.Dave.Say("Feels impenetrable");
		yield return E.Break;
		
	}

	public IEnumerator OnInteractPropWell( Prop prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		E.StartCutscene();
		yield return E.WaitSkip();
		yield return C.Dave.Say("I can't see anything in the well");
		yield return E.WaitSkip();		
		yield return C.Dave.Say("And I'm certainly not climbing down there");
		yield return E.WaitSkip();
		yield return C.Barney.Face(C.Dave);
		yield return C.Barney.Say("Oh go on!");
		yield return C.Dave.Face(C.Barney);
		yield return C.Dave.Say("Ummmm... ");
		yield return E.WaitSkip();
		yield return C.FaceClicked();
		yield return E.WaitSkip(1.0f);
		yield return C.Dave.Face(C.Barney);
		yield return E.WaitSkip();
		yield return C.FaceClicked();
		yield return E.WaitSkip(1.0f);
		yield return C.Dave.Face(C.Barney);
		yield return E.WaitSkip(1.0f);
		yield return C.Dave.Say("No");
		yield return E.WaitSkip();
		yield return C.FaceClicked();
		E.EndCutscene();
		
		yield return E.Break;
		
	}

	public IEnumerator OnInteractHotspotCave( Hotspot hotspot )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return E.WaitSkip();
		yield return C.Dave.Say("No way am I going in there!");
		yield return C.Dave.FaceDown();
		yield return E.WaitSkip();
		yield return C.Dave.Say("There might be beetles");
		yield return E.Break;
		
	}


	public IEnumerator OnInteractPropBucket( Prop prop )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Display("Dave stoops to pick up the bucket");
		Audio.Play("Bucket");
		prop.Disable();
		I.Bucket.AddAsActive();
		yield return E.WaitSkip();
		yield return C.Player.FaceDown();
		yield return C.Dave.Say("Yaaay! I got a bucket!");
		yield return E.Break;
		
	}

	public IEnumerator OnUseInvPropWell( Prop prop, Inventory item )
	{
		if ( item == I.Bucket )
		{ 
			yield return C.WalkToClicked();
			yield return C.FaceClicked();
			yield return C.Display("Dave lowers the bucket down, and collects some juicy well water");
			GlobalScript.Script.m_progressExample = eProgress.GotWater; 
			PowerQuest.Get.GetScript<GlobalScript>().m_progressExample = eProgress.GotWater; 
			GlobalScript.Script.m_progressExample = eProgress.GotWater;
			yield return C.Dave.Say("Yaaay! I solved the real hard puzzle!");
			yield return E.Wait(1);
			yield return C.Display("THE END");
			yield return E.WaitSkip();  
			yield return C.Dave.FaceDown();
			yield return E.WaitSkip();
			yield return C.Dave.Say("Yaay!");
			GlobalScript.Script.m_progressExample = eProgress.WonGame; 
		
		}
		yield return E.Break;
		
	}

	public IEnumerator OnUseInvPropBucket( Prop prop, Inventory item )
	{

		yield return E.Break;

	}

	public IEnumerator OnInteractHotspotSky( Hotspot hotspot )
	{
		m_timesClickedSky++;
		string text = "You've clicked the sky " + m_timesClickedSky + " times";
		yield return C.Display(text);
		yield return E.Break;
		
	}


	public IEnumerator OnLookAtHotspotForest( IHotspot hotspot )
	{
		yield return C.Dave.FaceUp();
		yield return C.Dave.Say("Looks impenetrable");
		yield return E.Break;
	}

	public IEnumerator OnEnterRegionCorner( IRegion region, ICharacter character )
	{
		yield return E.WaitSkip();
		C.Dave.StopWalking();
		yield return E.WaitSkip();
		yield return C.Dave.FaceDown();
		yield return E.WaitSkip();
		yield return C.Dave.Say("This corner gives me the heebie jeebies");
		yield return E.WaitSkip(0.25f);
		yield return C.Barney.Face(C.Dave);
		yield return C.Barney.Say("Yeah, stay away from that corner dude");
		yield return E.WaitSkip(0.25f);
		yield return C.Dave.FaceUp();
		yield return C.Dave.Say("Good idea");
		Region("Corner").Walkable = false;
		yield return E.Break;
	}

	public IEnumerator OnLookAtPropWell( IProp prop )
	{

		yield return E.Break;
	}

	public IEnumerator OnExitRegionCorner( IRegion region, ICharacter character )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtPropBucket( IProp prop )
	{

		yield return E.Break;
	}

	public IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{

		yield return E.Break;
	}

	public IEnumerator OnWalkTo()
	{

		yield return E.Break;
	}

	public IEnumerator UpdateBlocking()
	{

		yield return E.Break;
	}

	public void Update()
	{
	}

	public IEnumerator OnAnyClick()
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAtHotspotCave( IHotspot hotspot )
	{

		yield return E.Break;
	}

	public void OnPostRestore( int version )
	{
	}
}