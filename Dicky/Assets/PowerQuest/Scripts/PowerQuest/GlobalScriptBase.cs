using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerTools.Quest;


public partial class GlobalScript : GlobalScriptBase<GlobalScript>
{
	#region Funcs: Legacy functions- Moved to PowerQuest
	//[System.Obsolete("This function is obsolete, Use E.DisableAllClickablesExcept() instead")]
	//public void DisableAllClickablesExcept(params string[] exceptions) {}
	[System.Obsolete("This function is obsolete, Use E.RestoreAllClickables() instead")]
	public void RestoreAllClickables() {}
	[System.Obsolete("This function is obsolete, Use E.SetAllClickableCursors() instead")]
	public void SetAllClickableCursors( string cursor, params string[] exceptions) {}
	[System.Obsolete("This function is obsolete, Use E.RestoreAllClickableCursors() instead")]
	public void RestoreAllClickableCursors() {}

	#endregion
	#region Legacy functions for ambient sounds. Included so there's no compile errors when upgrading

	#pragma warning disable 414
	protected string m_ambientSoundName = null; // this is legacy, included so there's no compile errors when upgrading
	#pragma warning restore 414

	/// Helper function for playing ambient sounds (will probably move to PowerQuest system)
	[System.Obsolete("This function is obsolete, Use SystemAudio.PlayAmbientSound() instead")]
	public void PlayAmbientSound( string name, float fadeTime = 0.4f ) {}

	/// Helper function for stopping ambient sounds (will probably move to PowerQuest system)
	[System.Obsolete("This function is obsolete, Use SystemAudio.StopAmbientSound() instead")]
	public void StopAmbientSound(float overTime = 0.4f) {}

	#endregion

}

public class GlobalScriptBase<T> : QuestScript where T : QuestScript
{
	// Allows access to specific room's script by calling eg. GlobalScript.Script instead of E.GetScript<GlobalScript>()
	// This is needed when hotloading
	public static T Script { get {return E.GetScript<T>(); } }


}
