using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class CharacterDicky : CharacterScript<CharacterDicky>
{


	public IEnumerator OnLookAt()
	{
		yield return C.Dicky.Say("It's me Dicky the Elf");
		yield return E.Break;
	}

	public IEnumerator OnInteract()
	{
		yield return C.Dicky.Say("Hey quit it!");
		yield return E.Break;
	}
}