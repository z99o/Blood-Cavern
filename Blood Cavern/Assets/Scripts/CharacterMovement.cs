using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private bool turned;
    private float lastHorizontal;
    public float grounded;
    public Rigidbody2D rb;
    public float GroundOffset = .2f;
    public Vector3 offset;
    public LayerMask groundLayer;
    public bool facingRight;
    public float jumpStrength;
    public float jumpOffset;
    public float runSpeed;
    public Animator legAnimator;
    public playerFollow playerFollow;
    public GameObject playerHead;
    void Start()
    {
        lastHorizontal = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update(){
        float horizontal = Input.GetAxis("Horizontal");
        
        Vector2 position = transform.position;
        position.x = position.x + runSpeed * horizontal* Time.deltaTime;
        transform.position = position;
        animate(horizontal);
        flip();
        jump();
    }
    void jump(){
        float vertical = Input.GetAxis("Vertical");
        if(isGrounded() && vertical > 0)
            rb.AddForce(vertical*transform.up*jumpStrength);
    }
    void flip(){
          if(facingRight && transform.rotation.y < 0){//turn left
            //Vector3 theScale = transform.localScale;
            transform.Rotate(0,180,0);
            playerFollow.offset.x *= -1;
            //transform.localScale = theScale;
            Debug.Log("rotating left");
        }
        if(!facingRight && transform.rotation.y >= 0 ){ //turn right
            //Vector3 theScale = transform.localScale;
            transform.Rotate(0,180,0);
            playerFollow.offset.x *= -1;
           // transform.localScale = theScale;
            Debug.Log("rotating right");
        }
    }
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
    private bool isGrounded(){
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, jumpOffset, groundLayer);
        Debug.DrawRay(position, direction, Color.green);
        //Debug.Log(hit.collider.gameObject.tag);
        if (hit.collider != null) {
            return true;
        }

            return false;
        }

}
