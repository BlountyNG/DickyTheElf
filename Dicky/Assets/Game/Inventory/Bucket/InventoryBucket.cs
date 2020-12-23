using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;

public class InventoryBucket : InventoryScript<InventoryBucket>
{


	public IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		yield return C.Dicky.Say("It's a cup of elfbucks coffee");
		yield return E.Break;
		
	}


	public IEnumerator OnInteractInventory( IInventory item )
	{
		I.Bucket.SetActive();
		yield return E.Break;
	}

	public IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{
		
		yield return E.Break;
	}
}