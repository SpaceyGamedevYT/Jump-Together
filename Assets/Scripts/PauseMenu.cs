using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamePaused)
            {
                gamePaused = true;
                menu.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        gamePaused = false;
        menu.SetActive(false);
    }

    public void Leave()
    {
        SceneManager.LoadScene("Menu Level Select");
    }
}
