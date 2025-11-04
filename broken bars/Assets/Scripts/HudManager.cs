using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    public Text Maintext;
    public Button Option1;
    // Start is called before the first frame update
    void Start()
    {
        Maintext.text = "NOt me";
        Option1.GetComponentInChildren<Text>().text = "hello";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
