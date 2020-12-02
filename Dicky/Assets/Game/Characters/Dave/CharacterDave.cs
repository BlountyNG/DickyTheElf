using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class CharacterDave : CharacterScript<CharacterDave>
{

	public IEnumerator OnUseInv( IInventory thisItem )
	{

		yield return E.Break;
	}

	public IEnumerator OnLookAt()
	{

		yield return E.Break;
	}

	public IEnumerator OnInteract()
	{

		yield return E.Break;
	}
}