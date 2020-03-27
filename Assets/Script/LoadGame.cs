using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public static bool IsLoad = false;

    public void BtnPressed(){
        IsLoad = true;
    }
    void Update(){
        if (IsLoad)
        {
            DataPlayer dataPlayer = SaveSystem.LoadDataPlayer();
        
            string level = dataPlayer.level;

            if (level == "GameplayEasy")
            {
                SceneManager.LoadScene("GameplayEasy");
            }
            else if(level == "GameplayMedium"){
                SceneManager.LoadScene("GameplayMedium");
            }
            else if(level == "GameplayHard"){
                SceneManager.LoadScene("GameplayHard");
            }
            else{
                Debug.Log("Scene tidak dapat ditemukan");
            }    
        }
    }
}
