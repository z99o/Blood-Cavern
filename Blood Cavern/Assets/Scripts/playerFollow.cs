using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Vector3 offset;
    public bool turn;
    private bool toggle;
    void Start()
    {
        turn = false;
        toggle = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if(!turn && toggle){ //turn left
            offset.x*= -1;
            turn = true;
            toggle = false;
        }
        else if(turn && !toggle){ //turn right
            offset.x*= -1;
            turn = true;
            toggle = true;
        }*/
        transform.position = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y + offset.y, offset.z);
    }
}
