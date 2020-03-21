using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour
{
    // Start is called before the first frame update
    public void MENU_ACTION_GotoPage(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
