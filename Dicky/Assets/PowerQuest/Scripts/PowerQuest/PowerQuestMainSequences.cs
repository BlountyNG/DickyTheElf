using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Reflection;
using PowerTools;


//
// PowerQuest Partial Class: Main loop coroutine
//

namespace PowerTools.Quest
{

public partial class PowerQuest
{


	#region Coroutine: Load Room

	IEnumerator LoadRoomSequence( string sceneName )
	{

		//
		// Get the camera and canvas
		//
		GameObject guiCamObj = GameObject.Find("QuestGuiCamera");
		Debug.Assert(guiCamObj != null, "Faled to load room- Couldn't find QuestGuiCamera in the scene");
		m_cameraGui = guiCamObj.GetComponent<Camera>();
		// Get the canvas
		m_canvas = guiCamObj.GetComponentInChildren<Canvas>();

		//
		// Set up room object
		//
		RoomComponent roomInstance = GameObject.FindObjectOfType<RoomComponent>();
		Debug.Assert(roomInstance != null, "Failed to find room instance in scene");
		string roomName = roomInstance.GetData().ScriptName;
		// Find the room's data
		Room room = m_rooms.Find( item=> string.Equals(item.ScriptName, roomName, System.StringComparison.OrdinalIgnoreCase) );
		Debug.Assert(room != null, "Failed to load room '"+roomName+"'");
		m_currentRoom = room;
		room.SetInstance(roomInstance);

		//
		// Set up camera object (after room so room is set up right)
		//
		QuestCameraComponent cameraInstance = GameObject.FindObjectOfType<QuestCameraComponent>();
		Debug.Assert(cameraInstance != null);
		m_cameraData.SetInstance(cameraInstance);
		m_cameraData.SetCharacterToFollow(GetPlayer());

		//
		// Setup GUIs
		//
		foreach ( Gui gui in m_guis ) 
		{
			GameObject guiInstance = GameObject.Find(gui.GetPrefab().name);

			if ( guiInstance == null )
			{
				guiInstance = GameObject.Instantiate(gui.GetPrefab() ) as GameObject;

			}
			gui.SetInstance(guiInstance.GetComponent<GuiComponent>());
			guiInstance.SetActive( gui.Visible );
			if ( m_canvas != null )
				guiInstance.transform.SetParent(m_canvas.transform, false);

		}

		// 
		// Set up cursor object
		//
		if( m_cursorPrefab != null )
		{
			GameObject cursorObj = GameObject.Instantiate(m_cursor.GetPrefab()) as GameObject;
			QuestCursorComponent cursorInstance = cursorObj.GetComponent<QuestCursorComponent>();
			m_cursor.SetInstance(cursorInstance);
		}

		//
		// Spawn and position characters
		//

		// For now force player to be in the current room. maybe this could be handled nicer though.
		m_player.Room = GetRoom(roomName);

		// Spawn characters that are supposed to be in this room, and remove those that ain't
		foreach ( Character character in m_characters ) 
		{									
			if ( character.Room == GetRoom(roomName) )
			{
				// Spawn the character
				character.SpawnInstance();
			}
			else
			{
				GameObject characterInstance = GameObject.Find(character.GetPrefab().name);
				if ( characterInstance != null )
				{
					// Character's not supposed to be here, so remove them
					characterInstance.name = "deleted"; // so it doesn't turn up in searches in same frame
					GameObject.Destroy(characterInstance);
				}
			}
		}
		// Now rooms and characters, etc are set up, mark as initialised (this is done once per application load)
		m_initialised = true;



		if ( room.GetInstance() != null ) room.GetInstance().OnLoadComplete();

		if ( m_restoring )
		{
			// When restoring a game, the scene is reloaded, but we don't call on enter room, etc.
			FadeInBG(m_settings.TransitionFadeTime/2.0f, "ENTER");

			yield return new WaitForSeconds(0.05f);// Wait(0.05f); Game might start paused- dont' do this.
			m_restoring = false;
			m_transitioning = false;
		}
		else 
		{

			//
			// On enter room
			//
			Block();

			// Remove any camera override set in the previous room
			m_cameraData.ResetPositionOverride();

			System.Reflection.MethodInfo method = null;
			if ( m_globalScript != null )
			{
				method = m_globalScript.GetType().GetMethod( "OnEnterRoom", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
				if ( method != null ) method.Invoke(m_globalScript,null);
			}

			if ( room != null && room.GetScript() != null )
			{
				method = room.GetScript().GetType().GetMethod( "OnEnterRoom", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
				if ( method != null ) method.Invoke(room.GetScript(),null);
			}

			// update region collision to initial state (So don't get "OnEnter" on first frame of room)
			{
				UpdateRegions();
				room.GetInstance().GetRegionComponents().ForEach( item=>item.OnRoomLoaded() );
			}

			yield return new WaitForEndOfFrame(); 

			m_cameraData.GetInstance().OnEnterRoom();

			//
			//	Room transition - yield until complete 
			//
			//		NB: currently ont waiting until fade in complete before calling OnEnterRoomAfterFade. Just waiting a couple of frames. Can't remember why this is.
			// 		Better would be to have OnEnterRoom() be coroutine, fade in the background during, and add a method: E.WaitForFade(), for handling things you want after the OnEnterRoom
			//		For this to work nicely, might want to have camera snap after the "OnEnterRoom" yields first time, so start the routine, do camera snap, etc, then continue it.
			/* EG:
			{	
				Coroutine onEnterGlobal = null;			
				Coroutine onEnterRoom = null;

				// Start global OnEnter
				onEnterGlobal = StartScriptInteractionCoroutine(GetScript(), "OnEnterRoom");
				SetAutoLoadScript( this, "OnEnterRoomAfterFade", onEnterGlobal != null );


				// Start room OnEnter
				if ( room != null && room.GetScript() != null )
				{
					onEnterRoom =  StartScriptInteractionCoroutine(room.GetScript(), "OnEnter");
					SetAutoLoadScript( room, "OnEnter", onEnterRoom != null );
				}

				// Do initial setup stuff (camera/regions)
				// update region collision to initial state (So don't get "OnEnter" on first frame of room)
				{
					UpdateRegions();
					room.GetInstance().GetRegionComponents().ForEach( item=>item.OnEnterRoom() );
				}
				yield return new WaitForEndOfFrame(); 
				m_cameraData.GetInstance().OnEnterRoom()

				// start fade in
				FadeInBG(m_settings.TransitionFadeTime/2.0f, "ENTER");
				yield return Wait(0.05f); // I guess this isn't necessary any more?

				m_transitioning = false;

				// Yield to global onEnter
				if ( onEnterGlobal != null )
					yield return onEnter;

				// Yield to room onEnter
				if ( onEnter != null )
					yield return onEnter;

				// Done
				Unblock();
				
			}*/

			FadeInBG(m_settings.TransitionFadeTime/2.0f, "ENTER");
			yield return Wait(0.05f); 

			m_transitioning = false;

			//
			// on enter room after fadein
			//

			Coroutine onEnter = StartScriptInteractionCoroutine(GetScript(), "OnEnterRoomAfterFade");
			SetAutoLoadScript( this, "OnEnterRoomAfterFade", onEnter != null );
			if ( onEnter != null )
				yield return onEnter;

			if ( room != null && room.GetScript() != null )
			{
				onEnter = StartScriptInteractionCoroutine(room.GetScript(), "OnEnterRoomAfterFade");
				SetAutoLoadScript( room, "OnEnterRoomAfterFade", onEnter != null );
				if ( onEnter != null )
					yield return onEnter;
			}
			Unblock();
			
			//
			// Debug save, so can restore to that state easily
			//
			//if ( Debug.isDebugBuild )
			//	Save(10,"Debug");
		}

		//
		// Main loop
		//
		m_coroutineMainLoop = StartCoroutine( MainLoop() );
		m_roomLoopStarted = true;
	}


	#endregion
	#region Coroutine: Main loop


	IEnumerator MainLoop()
	{
		while ( true )
		{
			//
			// Main Update
			//

			Block();
			bool yielded = false;

			if ( SystemTime.Paused == false)
			{

				//
				// Finish Current Sequence
				//
				if ( m_currentSequence != null )
				{
					yield return m_currentSequence;
					m_currentSequence = null;
				}

				ExtentionOnMainLoop();

				//
				// Run through any queued interactions. 
				// These happen when a non-yielding function like StopDialog() is called and results in a yielding function like IEnumerator OnStopDialog() being started
				//
				while ( m_queuedScriptInteractions.Count > 0 )
				{
					m_currentSequence = m_queuedScriptInteractions[0];
					m_queuedScriptInteractions.RemoveAt(0);

					if ( m_currentSequence != null )
					{
						yielded = true;
						yield return m_currentSequence;
						m_currentSequence = null;
					}
				}
				m_queuedScriptInteractions.Clear();

				//
				// Mouse triggered sequences
				//

				// Handle left/right click seperately here because the main loop may take more than 1 normal frame, so getmouseButtonDown won't cut it
				bool leftClick = m_leftClickPrev == false && Input.GetMouseButton(0);
				bool rightClick = m_rightClickPrev == false && Input.GetMouseButton(1);
				m_leftClickPrev = Input.GetMouseButton(0);
				m_rightClickPrev = Input.GetMouseButton(1);

				bool clickHandled = false;

				if ( GetModalGuiActive() )
					clickHandled = true;

				if ( SV.m_captureInputSources.Count > 0 )
					clickHandled = true;

				//
				// Handle holding down click to walk
				//
				if ( m_walkClickDown )
				{
					// Was holding click-to-walk
					if ( Input.GetMouseButton(0) == false || clickHandled )
					{
						m_walkClickDown = false;
					}
					else if ( (m_player.Position - m_mousePos).magnitude > 10 )
					{
						// Holding left click- keep walking			
						m_player.WalkToBG(m_mousePos);
					}
					clickHandled = true;
				}

				if ( clickHandled == false && (leftClick || rightClick) )
				{
					System.Reflection.MethodInfo method = null;
					if ( m_globalScript != null )
					{
						method = m_globalScript.GetType().GetMethod( "OnMouseClick", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
						if ( method != null ) 
							method.Invoke(m_globalScript,new object[]{ leftClick,rightClick });
						else 
							OnMouseClick(leftClick, rightClick);
					}
				}

				//
				// Run through any queued interactions. 
				// These would have been added in "OnMouseClick" from ProcessClick calls. This could optionally be added after "update" I guess, incase wanna do controls in update instead.
				//
				while ( m_queuedScriptInteractions.Count > 0 )
				{
					m_currentSequence = m_queuedScriptInteractions[0];
					m_queuedScriptInteractions.RemoveAt(0);

					if ( m_currentSequence != null )
					{
						yielded = true;
						yield return m_currentSequence;
						m_currentSequence = null;
					}
				}
				m_queuedScriptInteractions.Clear();

				//
				// Game Update Blocking
				//
				if ( StartScriptInteraction( this, "UpdateBlocking" ) )
				{
					yielded = true;
					yield return m_currentSequence;
				}

				//
				// Room Update Blocking
				//
				if ( m_currentRoom != null)
				{
					if ( StartScriptInteraction(m_currentRoom, "UpdateBlocking" ) )
					{
						yielded = true;
						yield return m_currentSequence;
					}
				}


				//
				// Run through any queued interactions, in case there were any ProcessClick calls in the update
				//
				while ( m_queuedScriptInteractions.Count > 0 )
				{
					m_currentSequence = m_queuedScriptInteractions[0];
					m_queuedScriptInteractions.RemoveAt(0);

					if ( m_currentSequence != null )
					{
						yielded = true;
						yield return m_currentSequence;
						m_currentSequence = null;
					}
				}
				m_queuedScriptInteractions.Clear();

				//
				// Region enter/exit blocking
				//

				if ( m_currentRoom != null)
				{
					// not using collision system, just looping over characters in the room and triggers in the room
					List<RegionComponent> regionComponents = m_currentRoom.GetInstance().GetRegionComponents();
					int regionCount = regionComponents.Count;
					RegionComponent region = null;
					for ( int charId = 0; charId < m_characters.Count; ++charId )
					{
						Character character = m_characters[charId];

						for ( int regionId = 0; regionId < regionCount; ++regionId )
						{					
							region = regionComponents[regionId];
							RegionComponent.eTriggerResult result = region.UpdateCharacterOnRegionState(charId);
							if ( result == RegionComponent.eTriggerResult.Enter )
							{
								if ( StartScriptInteraction( m_currentRoom, SCRIPT_FUNCTION_ENTER_REGION+region.GetData().ScriptName, new object[] {region.GetData(), character}, false ) )
								{
									yielded = true;
									yield return m_currentSequence;
								}
							} 
							else if ( result == RegionComponent.eTriggerResult.Exit )
							{
								if ( StartScriptInteraction( m_currentRoom, SCRIPT_FUNCTION_EXIT_REGION+region.GetData().ScriptName, new object[] {region.GetData(), character}, false ) )
								{
									yielded = true;
									yield return m_currentSequence;
								}
							} 
						}
					}

				}

			}


			//
			// end of the main loop sequences
			//

			Unblock();

			// Reset gui click flag
			m_guiConsumedClick = false;

			// if cutscene is being skipped, it's now finished, so reset it
			if ( m_skipCutscene == true )
			{
				OnEndCutscene();
			}


			if ( yielded == false && m_currentDialog != null )
			{
				// Show dialog gui again if one active
				GetGui(Settings.DialogTreeGui).Visible = true;
			}

			// Yield until the next frame
			if ( yielded == false )
				yield return new WaitForEndOfFrame();
		}			
	}

	#endregion
	#region Main sequence helpers

	// NB: This is the fallback, only used if GlobalScript doesn't have an OnMouseClick
	void OnMouseClick( bool leftClick, bool rightClick )
	{
		// Clear inventory on Right click, or left click on empty space
		if ( m_player.HasActiveInventory && ( rightClick || (GetMouseOverClickable() == null && leftClick ) || Cursor.NoneCursorActive ) )
		{
			SystemAudio.Play("InventoryCursorClear");						
			m_player.ActiveInventory = null;
			return;
		}

		// Don't do anything if clicking on something with "none" cursor
		if ( m_cursor.NoneCursorActive )
			return;

		if ( leftClick )
		{
			// Handle left click
			if ( GetMouseOverClickable() != null )
			{	
				// Left click something
				if ( m_player.HasActiveInventory && m_cursor.InventoryCursorOverridden == false )
				{
					ProcessClick( eQuestVerb.Inventory );
				}
				else
				{
					ProcessClick(eQuestVerb.Use);
				}
			}
			else 
			{
				// Left click empty space
				ProcessClick( eQuestVerb.Walk );
			}
		}
		else if ( rightClick && GetActionEnabled(eQuestVerb.Look) )
		{
			// Handle right click
			if ( GetMouseOverClickable() != null )
			{
				ProcessClick( eQuestVerb.Look );
			}
		}		
	}
	#endregion

}

}