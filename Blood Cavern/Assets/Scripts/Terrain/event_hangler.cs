using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_hangler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject commander;
    public AudioClip clip;
    void Start()
    {
        commander = GameObject.FindGameObjectWithTag("Commander");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !commander.GetComponent<AudioSource>().isPlaying){
            commander.GetComponent<AudioSource>().PlayOneShot(clip,0.6f);
            Destroy(gameObject);
        }

    }
}
