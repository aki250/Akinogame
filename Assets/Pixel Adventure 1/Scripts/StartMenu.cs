using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartGame()
    {
        //采用同步加载方式切换UI
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
