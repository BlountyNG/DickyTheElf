using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerScript;
using PowerTools.Quest;

/// Just an example of using an enum for game state
[QuestAutoCompletable]
public enum eProgress
{
	None,
	GotWater,
	DrankWater,
	WonGame
};

///	Global Script: The home for your game specific logic
/**		
 * - The functions in this script are used in every room in your game.
 * - Put game-wide variables in here and you can access them from the quest script editor 'Globals.' (or from other quest scripts with 'GlobalScript.Script.')
 * - If you've used Adventure Game Studio, this is equivalent to the Global Script in that
*/
public partial class GlobalScript : GlobalScriptBase<GlobalScript>
{
	
	// NB: These regions are just to keep things tidy, they're not required.
	#region Global Game Variables
	
	/// Just an example of using an enum for game state.
	public eProgress m_progressExample = eProgress.None;
	
	/// Just an example of using a global variable that can be accessed in any room. All variables like this in Quest Scripts are automatically saved
	public bool m_spokeToBarney = false;
	public bool m_changedClothes = false;
	
	#endregion
	#region Global Game Functions
	
	/// Called when game first starts
	
	public void OnGameStart()
	{     
	} 

	/// Called after restoring a game. Use this if you need to update any references based on saved data.
	public void OnPostRestore(int version)
	{
	}

	/// Blocking script called whenever you enter a room, before fading in. Non-blocking functions only
	public void OnEnterRoom()
	{
	}

	/// Blocking script called whenever you enter a room, after fade in is complete
	public IEnumerator OnEnterRoomAfterFade()
	{
		yield return E.Break;
	}

	/// Blocking script called whenever you exit a room, as it fades out
	public IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{
		yield return E.Break;
	} 

	/// Blocking script called every frame when nothing's blocking, you can call blocking functions in here that you'd like to occur anywhere in the game
	public IEnumerator UpdateBlocking()
	{
		yield return E.Break;
	}

	/// Called every frame. Non-blocking functions only
	public void Update()
	{
	}

	/// Blocking script called whenever the player clicks anywwere. This function is called before any other click interaction. If this function blocks, it will stop any other interaction from happening.
	public IEnumerator OnAnyClick()
	{
		yield return E.Break;
	}

	/// Blocking script called whenever the player tries to walk somewhere. Even if `C.Player.Moveable` is set to false.
	public IEnumerator OnWalkTo()
	{
		yield return E.Break;
	}

	/// Called when the mouse is clicked in the game screen. Use this to customise your game interface by calling E.ProcessClick() with the verb that should be used. By default this is set up for a 2 click interface
	public void OnMouseClick( bool leftClick, bool rightClick )
	{

		// Clear inventory on Right click, or left click on empty space, or on hotspot with cursor set to "None"
		if ( C.Player.HasActiveInventory && ( rightClick || (E.GetMouseOverClickable() == null && leftClick ) || Cursor.NoneCursorActive ) )
		{					
			C.Player.ActiveInventory = null;
		}
		else if ( Cursor.NoneCursorActive )
		{
			// Special case for clickables with cursor set to "None"- Don't do anything		
		}
		else if ( leftClick )
		{
			// Handle left click
			if ( E.GetMouseOverClickable() != null )
			{	
				// Left clicked a clickable
				if ( C.Player.HasActiveInventory && Cursor.InventoryCursorOverridden == false )
				{
					// Left click with active inventory, use the inventory item
					E.ProcessClick( eQuestVerb.Inventory );
				}
				else
				{
					// Left click on item, so use it
					E.ProcessClick(eQuestVerb.Use);
				}
			}
			else 
			{
				// Left click empty space, walk
				E.ProcessClick( eQuestVerb.Walk );
			}
		}
		else if ( rightClick )
		{
			// Right click on something- look at it
			if ( E.GetMouseOverClickable() != null )
				E.ProcessClick( eQuestVerb.Look );
		}	

	}

	#endregion
	#region Unhandled interactions

	/// Called when player interacted with something that had not specific "interact" script
	public IEnumerator UnhandledInteract(IQuestClickable mouseOver)
	{		
		if ( R.Current.ScriptName == "Title")
			yield break;
		yield return C.Display("You can't use that");
		
	}

	/// Called when player looked at something that had not specific "Look at" script
	public IEnumerator UnhandledLookAt(IQuestClickable mouseOver)
	{
		if ( R.Current.ScriptName == "Title")
			yield break;

		yield return C.Display( "It's nothing interesting" );  
		yield break;
	}

	/// Called when player used inventory on an inventory item that didn't have a specific UseInv script
	public IEnumerator UnhandledUseInvInv(Inventory invA, Inventory invB)
	{
		if ( R.Current.ScriptName == "Title")
			yield break;
		yield return C.Display( "You can't use those together" ); 

	}

	/// Called when player used inventory on something that didn't have a specific UseInv script
	public IEnumerator UnhandledUseInv(IQuestClickable mouseOver, Inventory item)
	{
		if ( R.Current.ScriptName == "Title")
			yield break;
		yield return C.Display( "You can't use that" ); 
	}

	#endregion

}