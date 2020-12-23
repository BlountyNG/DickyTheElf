using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class InventoryCandyCane : InventoryScript<InventoryCandyCane>
{


	public IEnumerator OnInteractInventory( IInventory item )
	{
		I.CandyCane.SetActive();
		yield return E.Break;
	}

	public IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		yield return C.Dicky.Say("It's the candy cane I found");
		yield return E.Break;
	}

	public IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{

		yield return E.Break;
	}
}