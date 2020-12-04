using UnityEngine;
using UnityEngine.SceneManagement;


public class NGLogin: MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("SceneRoomTitle");
    }
}