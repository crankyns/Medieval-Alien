using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject ThisPan;
    public void ExitButton()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {
        Cursor.visible = false;
        ThisPan.SetActive(false);
    }
}
