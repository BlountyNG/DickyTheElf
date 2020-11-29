using UnityEngine;
using System.Collections;

namespace PowerTools.Quest
{

[System.Serializable]
public class RoomScript<T> : QuestScript where T : QuestScript
{
	// Allows access to specific room's script by calling eg. RoomKitchen.Script instead of E.GetScript<RoomKitchen<()
	public static T Script { get {return E.GetScript<T>(); } }
}
}