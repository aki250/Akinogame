using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartGame()
    {
        //����ͬ�����ط�ʽ�л�UI
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
