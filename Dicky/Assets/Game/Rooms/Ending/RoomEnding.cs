using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomEnding : RoomScript<RoomEnding>
{


	public void OnEnterRoom()
	{
		Audio.StopMusic();
		Audio.PlayAmbientSound("SoundTundra_Loop");
		Audio.Play("Sound523783__bennettfilmteacher__boom");
		
		Prop("TheEnd").Hide();
		
		GameObject.Find("NGHelper").GetComponent<NGHelper>().unlockMedal(61635);
		
		if ( I.Bucket.Owned )
		{
					//Unlock Hard Worker medal
			GameObject.Find("NGHelper").GetComponent<NGHelper>().unlockMedal(61619);
		}
		
		// Hide the inventory and info bar in the title
		G.InfoBar.Visible = false;
		G.Inventory.Visible = false;
		G.InfoBar.Clickable = false;
		G.Inventory.Clickable = false;
		E.Cursor.Visible = false;
		
		//Hide toolbar for web games
		G.Toolbar.Visible = false;
		G.Toolbar.Clickable = false;
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		Audio.PlayMusic("SoundXmas Prayer", 1, 3f);
		Prop("TheEnd").Show();
		yield return Prop("TheEnd").Fade(0,1,3);
		yield return E.Wait(5f);
		E.Cursor.Visible = true;
		yield return E.Break;
	}

	public IEnumerator OnInteractPropTheEnd( IProp prop )
	{
		E.Restart();
		yield return E.Break;
	}
}