using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadManager : MonoBehaviour
{
    public PressurePad red;
    public PressurePad blue;
    public string nextLevel;

    void Update()
    {
        if(red.touchingPad && blue.touchingPad)
        {
            Invoke(nameof(LoadLevelSelectScene), 0.5f);
        }
    }

    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
