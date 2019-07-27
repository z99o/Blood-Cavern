using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ant_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float moveSpeed;
    private bool toggleFlip;
    public bool faceRight;
    private float timer;
    private float timeToReorient = 0;
    private bool getUp;
    private Rigidbody2D rb;
    public float getUpStrength;
    public Vector3 com;
    public float jumpCooldown;
    private float jumpTimer = 0;
    public float jumpStrength;
    public int health;
    private int curHealth;
    public float despawnTime;
    private float despawnTimer = 0; 
    public Animator animator;
    private bool deathAction = false;
    public bool isDead = false;
    public GameObject bloodSpurt;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        curHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth <= 0)
            die();
        else{
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position,moveSpeed* Time.deltaTime);
            flip();
            jump();
            reOrient();
        }
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
        animator.SetBool("isWalking",false);
        Destroy(transform.GetChild(0).gameObject);
        animator.SetBool("isDead",true);
        rb.mass = 1f;
        deathAction = true;
    }

    void flip(){
        if(player.transform.position.x - transform.position.x < 0){ //if player behind ant
            faceRight = false;
        }
        else
            faceRight = true;
        //Debug.Log(transform.eulerAngles.y);
        if(faceRight && transform.eulerAngles.y < 179){
            transform.Rotate(0,180,0);
        }
        if(!faceRight && transform.eulerAngles.y > 1)
            transform.Rotate(0,180,0);
        }
    void jump(){
        jumpTimer += Time.deltaTime;
        if(player.transform.position.y - transform.position.y > 0 && jumpCooldown - jumpTimer < 0){
            rb.AddForce(transform.up*jumpStrength);
            jumpTimer = 0;
        }
    }
    void reOrient(){
        if(transform.rotation.eulerAngles.z > 220 && transform.rotation.eulerAngles.z < 359 || transform.rotation.eulerAngles.z > 1 && transform.rotation.eulerAngles.z < 200){
            timer += Time.deltaTime;
            if(timeToReorient - timer < 0){
                getUp = true;
                timer = 0;
            }
        }
        else{   
            getUp = false;
            timer = 0;
        }
        if(getUp){
            rb.centerOfMass = com;
            rb.AddForce(transform.right*getUpStrength);
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
