using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ant;
    public GameObject bloodSpurt;
    private bool isDead = false;
    public Vector3 spawnOffset;
    public float spawnTime;
    private float spawnTimer = 0;
    public int maxAnts;
    public int maxHealth;
    private int curHealth;
    public float despawnTime;
    private float despawnTimer = 0; 
    //public Animator animator;
    private bool deathAction = false;
    public float detectDistance;
    private GameObject player;
    void Start()
    {
        curHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth <= 0)
            die();
        else
            spawnAnt();
    }
    void die(){
        if(!deathAction)
            deathRattle();
        despawnTimer += Time.deltaTime;
        if(despawnTime - despawnTimer < 0)
            Destroy(this.gameObject);
    }
    void deathRattle(){
        //death sound
        isDead = true;
        //animator.SetBool("isDead",true);
        deathAction = true;
    }

    void spawnAnt(){
        spawnTimer+= Time.deltaTime;
        if(spawnTime - spawnTimer < 0 && maxAnts > 0 && Vector3.Distance(transform.position,player.transform.position) <= detectDistance){
            spawnTimer = 0;
            maxAnts--;
            Instantiate(ant,transform.position+spawnOffset,new Quaternion(0,0,0,0));
        }    

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "bullet" && other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 35 && !isDead){ //check if bullet is fast enough
            curHealth -= other.gameObject.GetComponent<bullet_behavior>().damage;
            var spurt = Instantiate(bloodSpurt,other.transform);
            spurt.transform.parent = transform;
            Destroy(other.gameObject);
            //spurt blood
        }
    }

}
