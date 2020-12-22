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
		Camera.Shake(0.05f, 0.1f);
		yield return C.Dicky.Say("Hey quit it!");
		
		
		yield return E.Break;
	}

	public IEnumerator OnUseInv( IInventory thisItem )
	{
		if ( I.Bucket.Active )
		{
			yield return C.Display("You drink the coffee");
					//Unlock Coffee Drinker medal
			GameObject.Find("NGHelper").GetComponent<NGHelper>().unlockMedal(61557);
			I.Bucket.Remove();
		}
		
		yield return E.Break;
	}
}