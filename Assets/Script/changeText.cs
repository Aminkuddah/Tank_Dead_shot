using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
    private Text skorText;
    public Text tampungSkorText;
    // Start is called before the first frame update
    void Start()
    {
        skorText = GetComponent<Text>();
        tampungSkorText.text = "Skor = 0";
    }

    // Update is called once per frame
    void Update()
    {
        skorText.text = tampungSkorText.text;
    }
}
