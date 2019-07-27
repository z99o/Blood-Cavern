using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveWall_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    //configure your own object by dragging and dropping
    public GameObject[] objectives;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var count = 0;
        for(int i = 0; i < objectives.Length; i++){
            if(objectives[i])
                break;
            else
                count++;
        }
        
        if(count == objectives.Length)
            Destroy(gameObject);
    }
}
