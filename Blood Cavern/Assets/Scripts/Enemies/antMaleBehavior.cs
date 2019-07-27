using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antMaleBehavior : MonoBehaviour
{
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
    public float flyStrength;
    public int health;
    private int curHealth;
    public float despawnTime;
    private float despawnTimer = 0; 
    public Animator animator;
    private bool deathAction = false;
    public bool isDead = false;
    public GameObject bloodSpurt;
    public LayerMask groundLayer;
    public float minHeight;
    public float maxHeight;
    
    // Start is called before the first frame update
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
            isHighEnough();
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
        animator.SetBool("isDead",true);
        rb.mass = 10f;
        rb.gravityScale = 2f;
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
            rb.AddForce(-transform.up*getUpStrength);
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
    private void isHighEnough(){  
        Vector2 position = transform.position;
        Vector2 direction = -transform.up;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, minHeight, groundLayer);
        Debug.DrawRay(position, direction, Color.green);
        //Debug.Log(hit.collider.gameObject.tag);
        if (hit.collider != null) {
           rb.AddForce(transform.up*flyStrength);
        }
        //check if the character is too high off the ground
        RaycastHit2D tooHigh = Physics2D.Raycast(position, direction, maxHeight, groundLayer);
        if(tooHigh.collider == null)
            rb.AddForce(-transform.up*flyStrength);
        }
}
