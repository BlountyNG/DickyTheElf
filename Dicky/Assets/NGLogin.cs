using UnityEngine;
using UnityEngine.SceneManagement;


public class NGLoginButton : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("SceneRoomTitle");
    }

}