using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectives;
    public GameObject commander;
    public AudioClip clip;
    private bool won;
    private float timer = 20;

    void Start()
    {
        Cursor.visible = false;
        won = false;
        commander = GameObject.FindGameObjectWithTag("Commander");
    }

    // Update is called once per frame
    void Update()
    {
        if(won){
            timer -= Time.deltaTime;
            if(timer < 0)
                SceneManager.LoadScene("Main Menu");
        }
        var count = 0;
        for(int i = 0; i < objectives.Length; i++){
            if(objectives[i])
                break;
            else
                count++;
        }
        
        if(count == objectives.Length && !won){
            won = true;
            win();
        }
    }
    void win(){
        commander.GetComponent<AudioSource>().PlayOneShot(clip,0.6f);
    }
    
}
