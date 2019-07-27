using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D acidRB;
    public float despawnTime;
    public int damage;
    public float offset;
    public GameObject acidSpurt;

    void Start()
    {
        acidRB = GetComponent<Rigidbody2D>();
        var dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        dir = dir.normalized;
        acidRB.AddForce( dir.normalized*speed,ForceMode2D.Impulse);
        acidRB.centerOfMass = new Vector2(-.2f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "terrain"){ //check if acid is fast enough
            var spurt = Instantiate(acidSpurt,other.transform);
            spurt.transform.parent = transform;
            Destroy(gameObject);
        }
    }


}
