using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isStart;
    public bool isQuit;
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseUp(){
        if(isStart)
        {
            SceneManager.LoadScene("playLevel");
        }
        if (isQuit)
        {
            Application.Quit();
        }
} 
}
