using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void Easy(){
        SceneManager.LoadScene("GameplayEasy");
    }
    public void Medium(){
        SceneManager.LoadScene("GameplayMedium");
    }
    public void Hard(){
        SceneManager.LoadScene("GameplayHard");
    }
}
