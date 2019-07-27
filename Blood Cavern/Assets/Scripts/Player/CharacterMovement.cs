using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private bool turned;
    public float grounded;
    public Rigidbody2D rb;
    public float GroundOffset = .2f;
    public Vector3 offset;
    public LayerMask groundLayer;
    public bool facingRight;
    public float jumpStrength;
    public float jumpOffset;
    public float runSpeed;
    public float timeToReorient;
    private float timer;
    public Animator legAnimator;
    public playerFollow playerFollow;
    public GameObject playerHead;
    public bool getUp;
    public float getUpStrength;
    public Vector3 com;
    private float jumpCoolDown = 0.25f;
    private float jumpTimer;
    private bool jumping;
    public float maxJumpHeight;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip powerOn;
    public AudioClip powerOff;
    public AudioClip acidHurt;
    AudioSource audioSource;
    public int curHealth;
    public int maxHealth;
    public float leanStrength;
    private bool leaning = false;
    private bool deathRattle = false;
    public GameObject acidSpurt;
    private float deathTimer = 10;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        facingRight = true;
        timer = 0;
        jumping = false;
    }

    // Update is called once per frame
    void Update(){
        if(curHealth <= 0)
            die();
        else{
            rb.centerOfMass = new Vector2(0,0);
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 position = transform.position;
            position.x = position.x + runSpeed * horizontal* Time.deltaTime;
            transform.position = position;
            animate(horizontal);
            flip();
            jump();
            reOrient();
            if(Input.GetMouseButton(1))
                lean(); 
            else if(leaning){
                leaning = false;
                audioSource.PlayOneShot(powerOn,0.01f);
            }
        }
        
    }
    void die(){
        if(!deathRattle){
            gameObject.GetComponent<Rigidbody2D>().mass = 1;
            audioSource.PlayOneShot(deathSound,0.2f);
            deathRattle = true;
        }
        deathTimer -= Time.deltaTime;
            if(deathTimer < 0)
                SceneManager.LoadScene("Main Menu");
    }   

    //lean forwards
    void lean(){
        if(!leaning)
            audioSource.PlayOneShot(powerOff,0.01f);
        rb.AddForce(transform.right*(leanStrength));
        leaning = true;
    }
    //Makes Character Jump
    void jump(){
        float vertical = Input.GetAxis("Vertical");
        if(isGrounded() && vertical > 0 || jumping){
            audioSource.PlayOneShot(jumpSound,0.05f);
            vertical = 1;
            rb.AddForce(vertical*transform.up*jumpStrength);
            jumping = true;
            jumpTimer = 0; //reset timer
        }
    }
    //If character is fallen over or falling, reorient
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
        if(getUp && !leaning){
            rb.centerOfMass = com;
            rb.AddForce(transform.right*(getUpStrength));
            //Debug.Log((Mathf.Sqrt(Mathf.Abs(transform.rotation.z))));
        }
        //Debug.Log(transform.rotation.eulerAngles.z);
    }
    //Flips character
    void flip(){
          if(facingRight && transform.rotation.y < 0){//turn left
            transform.Rotate(0,180,0);
            playerFollow.offset.x *= -1;
            Debug.Log("rotating left");
        }
        if(!facingRight && transform.rotation.y >= 0 ){ //turn right
            transform.Rotate(0,180,0);
            playerFollow.offset.x *= -1;
            Debug.Log("rotating right");
        }
    }
    //Flips bools that control animations
    void animate(float horizontal){
        if(horizontal > 0 && facingRight || horizontal < 0 && !facingRight){
            legAnimator.SetBool("walkingForwards",true);
        }
        else if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight){
            legAnimator.SetBool("walkingBackward",true);
        }
        else{
            legAnimator.SetBool("walkingBackward",false);
            legAnimator.SetBool("walkingForwards",false);
        }
    }
    //detects if player is on the ground
    private bool isGrounded(){  
        Vector2 position = transform.position;
        Vector2 direction = -transform.up;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, jumpOffset, groundLayer);
        Debug.DrawRay(position, direction, Color.green);
        //Debug.Log(hit.collider.gameObject.tag);
        if (hit.collider != null) {
            jumpTimer += Time.deltaTime; //add to timer
            if(jumpCoolDown -jumpTimer < 0)
                return true;
            jumping = false;
        }
        //check if the character is too high off the ground
        RaycastHit2D tooHigh = Physics2D.Raycast(position, direction, maxJumpHeight, groundLayer);
        if(tooHigh.collider != null)
            jumping = false;

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "acid"){ //check if bullet is fast enough
            curHealth -= other.gameObject.GetComponent<acidBehavior>().damage;
            var spurt = Instantiate(acidSpurt,other.transform);
            spurt.transform.parent = transform;
            audioSource.PlayOneShot(acidHurt,0.05f);
            Destroy(other.gameObject);
            //spurt blood
        }
    }

}
