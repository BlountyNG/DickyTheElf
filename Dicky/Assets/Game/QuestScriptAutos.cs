using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerTools.Quest;

namespace PowerScript
{	
	// Shortcut access to SystemAudio.Get
	public class Audio : SystemAudio
	{
	}

	public static partial class C
	{
		// Access to specific characters (Auto-generated)
		public static ICharacter Dave		{ get{return PowerQuest.Get.GetCharacter("Dave"); } }
		public static ICharacter Barney		{ get{return PowerQuest.Get.GetCharacter("Barney"); } }
		public static ICharacter Dicky		{ get{return PowerQuest.Get.GetCharacter("Dicky"); } }
		// #CHARS# - Do not edit this line, it's used by the system to insert characters
	}

	public static partial class I
	{		
		// Access to specific Inventory (Auto-generated)
		public static IInventory Bucket		{ get{return PowerQuest.Get.GetInventory("Bucket"); } }
		// #INVENTORY# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	public static partial class G
	{
		// Access to specific gui (Auto-generated)
		public static IGui DisplayBox		{ get{return PowerQuest.Get.GetGui("DisplayBox"); } }
		public static IGui InfoBar		{ get{return PowerQuest.Get.GetGui("InfoBar"); } }
		public static IGui Toolbar		{ get{return PowerQuest.Get.GetGui("Toolbar"); } }
		public static IGui Inventory		{ get{return PowerQuest.Get.GetGui("Inventory"); } }
		public static IGui DialogTree		{ get{return PowerQuest.Get.GetGui("DialogTree"); } }
		public static IGui SpeechBox		{ get{return PowerQuest.Get.GetGui("SpeechBox"); } }
		// #GUI# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	public static partial class R
	{
		// Access to specific room (Auto-generated)
		public static IRoom Title		{ get{return PowerQuest.Get.GetRoom("Title"); } }
		public static IRoom Forest		{ get{return PowerQuest.Get.GetRoom("Forest"); } }
		public static IRoom MailRoom		{ get{return PowerQuest.Get.GetRoom("MailRoom"); } }
		public static IRoom Elevator		{ get{return PowerQuest.Get.GetRoom("Elevator"); } }
		public static IRoom StaffRoom		{ get{return PowerQuest.Get.GetRoom("StaffRoom"); } }
		public static IRoom Ending		{ get{return PowerQuest.Get.GetRoom("Ending"); } }
		// #ROOM# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	// Dialog
	public static partial class D
	{
		// Access to specific dialog trees (Auto-generated)
		public static IDialogTree ChatWithBarney		{ get{return PowerQuest.Get.GetDialogTree("ChatWithBarney"); } }
		// #DIALOG# - Do not edit this line, it's used by the system to insert rooms for easy access	    	    
	}


}
