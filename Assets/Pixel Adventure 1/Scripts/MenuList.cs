using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    public GameObject menuList;
    [SerializeField] private bool menuKeys = true;

    // Start is called before the first frame update
    void Start()
    {
        menuList.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuKeys) {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                menuList.SetActive(true);
                menuKeys = false;
                Time.timeScale = 0;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuList.SetActive(false) ;
            menuKeys = true;
            Time.timeScale = 1;
        }

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);  // 重新加载当前场景
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
