using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class RoomMailRoom : RoomScript<RoomMailRoom>
{
	public IEnumerator OnEnterRoomAfterFade()
	{
		C.Player.SetPosition(189, -62);
		yield return C.Dicky.WalkTo(Point("EntryWalk"));
		yield return C.Dicky.FaceRight();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.FaceLeft();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.FaceRight();
		yield return E.WaitSkip(1.0f);
		yield return C.Dicky.Say("Where the heck is everyone");
		yield return C.Dicky.Say("It's December 26th, The day after Christmas");
		yield return C.Dicky.Say("This place should be in full swing");
		yield return E.Break;
	}

	public void OnEnterRoom()
	{
		
		//Hide toolbar for web games
		G.Toolbar.Visible = false;
		G.Toolbar.Clickable = false;
	}
}