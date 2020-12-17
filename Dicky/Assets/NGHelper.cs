using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NGHelper : MonoBehaviour

{
    public io.newgrounds.core ngio_core;


    // call this method whenever you want to unlock a medal.
    public void unlockMedal(int medal_id)
    {
        // create the component
        io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();

        // set required parameters
        medal_unlock.id = medal_id;

        // call the component on the server, and tell it to fire onMedalUnlocked() when it's done.
        medal_unlock.callWith(ngio_core, onMedalUnlocked);
        Debug.Log("Sent a message to server to unlock medal");
    }

    // this will get called whenever a medal gets unlocked via unlockMedal()
    public void onMedalUnlocked(io.newgrounds.results.Medal.unlock result)
    {
        io.newgrounds.objects.medal medal = result.medal;
        Debug.Log("Medal Unlocked: " + medal.name + " (" + medal.value + " points)");
    }

}