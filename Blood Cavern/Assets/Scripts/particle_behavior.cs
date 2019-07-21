using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float despawnTime;
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(despawnTime - timer < 0)
            Destroy(this.gameObject);
    }
}
