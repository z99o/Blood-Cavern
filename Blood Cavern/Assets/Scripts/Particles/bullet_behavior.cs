using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D bulletRB;
    public float despawnTime;
    private float timer = 0;
    public int damage;
    public float offset;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.AddForce(transform.right * speed,ForceMode2D.Impulse);
        bulletRB.AddForce(transform.up*offset,ForceMode2D.Impulse);
        timer = despawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward *Time.deltaTime * speed;
        timer -= Time.deltaTime;
        if(timer < 0)
            Destroy(this.gameObject);
    }
}
