using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // this will get called whenever a medal gets unlocked via unlockMedal()
    void onMedalUnlocked(io.newgrounds.results.Medal.unlock result)
    {
        io.newgrounds.objects.medal medal = result.medal;
        Debug.Log("Medal Unlocked: " + medal.name + " (" + medal.value + " points)");
    }

    // Gets called when the player is signed in.
    void onLoggedIn()
    {
        // Do something. You can access the player's info with:
        io.newgrounds.objects.user player = ngio_core.current_user;
    }

    // Gets called if there was a problem with the login (expired sessions, server problems, etc).
    void onLoginFailed()
    {
        // Do something. You can access the login error with:
        io.newgrounds.objects.error error = ngio_core.login_error;
    }

    // Gets called if the user cancels a login attempt.
    void onLoginCancelled()
    {
        // Do something...
    }

    // When the user clicks your log-in button
    void requestLogin()
    {
        // This opens passport and tells the core what to do when a definitive result comes back.
        ngio_core.requestLogin(onLoggedIn, onLoginFailed, onLoginCancelled);
    }

    /*
     * You should have a 'cancel login' button in your game and have it call this, just to be safe.
     * If the user simply closes the browser tab rather than clicking a button to cancel, we won't be able to detect that.
     */
    void cancelLogin()
    {
        /*
         * This will call onLoginCancelled because you already added it as a callback via ngio_core.requestLogin()
         * for ngio_core.onLoginCancelled()
         */
        ngio_core.cancelLoginRequest();
    }

    // Check if the user has a saved login when your game starts
    void Start()
    {
        // Do this after the core has been initialized
        ngio_core.onReady(() =>
        {

            // Call the server to check login status
            ngio_core.checkLogin((bool logged_in) =>
            {

                if (logged_in)
                {
                    onLoggedIn();
                }
                else
                {
                    /*
                     * This is where you would ask them if the want to sign in.
                     * If they want to sign in, call requestLogin()
                     */
                }
            });
        });
    }

    // And finally, have your 'sign out' button call this
    void logOut()
    {
        ngio_core.logOut();
    }
}