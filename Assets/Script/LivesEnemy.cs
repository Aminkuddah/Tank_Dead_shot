using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesEnemy : MonoBehaviour
{
    
    public Image[] lives;
    public Sprite iconStarYellow;
    public Sprite iconStarGrey;

    public void OnChangeLife(int health)
    {
        // Debug.Log("Ini Health " + health);
        for (int i = 0; i <= lives.Length; i++)
        {
            if (i < health)
                lives[i].sprite = iconStarYellow;
            else
                lives[i].sprite =  iconStarGrey;
        }
    }

}
