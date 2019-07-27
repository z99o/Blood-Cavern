using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidShooter_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject acid;
    private GameObject player;
    public float timer = 0;
    public float fireRate;
    public int shots;
    private int curShots;
    private float burstSpeed = 0.2f;
    private float burstTimer = 0;
    public AudioClip acidShot;
    private AudioSource audioPlayer;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        curShots = shots;
    }

    // Update is called once per frame
    void Update()
    {
        if(!transform.parent.GetComponent<antMaleBehavior>().isDead)
            shootAcid();
    }
     void shootAcid(){
        if(fireRate - timer < 0){
            burstTimer+= Time.deltaTime;
            if(shots > 0 && burstSpeed-burstTimer < 0){
                transform.parent.gameObject.GetComponent<antMaleBehavior>().animator.SetTrigger("isAttacking");
                audioPlayer.PlayOneShot(acidShot,0.04f);
                var shot = Instantiate(acid,transform.position,transform.parent.transform.rotation);
                if(transform.parent.tag == "Boss")
                    shot.transform.localScale = new Vector3(4,4,1);
                shots--;
                burstTimer = 0;
            }
            else if(shots <= 0){
                timer = 0;
                shots = curShots;
            }
                
        }
        timer += Time.deltaTime;
    }
}
