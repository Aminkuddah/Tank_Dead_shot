using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void MENU_ACTION_GotoPage(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
