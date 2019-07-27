using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float attackCooldown;
    public int attackStrength;
    public float knockback;
    public float attackTimer = 0;
    public Animator attackAnim;
    public GameObject player;
    public AudioClip slash;
    public AudioSource audioSource;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    void attack(){
        if(attackCooldown - attackTimer < 0 && !transform.parent.GetComponent<ant_behavior>().isDead){
            player.GetComponent<CharacterMovement>().curHealth -= attackStrength;
            attackTimer = 0;
            audioSource.PlayOneShot(slash,0.2f);
            attackAnim.SetTrigger("Attacked");
            player.GetComponent<Rigidbody2D>().AddForce(transform.up*knockback);

        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            attack();
    }
}
