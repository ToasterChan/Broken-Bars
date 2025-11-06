using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    public void GoToScene(string sceneName)
    { SceneManager.LoadScene(sceneName); }
    public void quitApp()
    {
        Application.Quit();
        Debug.Log("application has quit");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
