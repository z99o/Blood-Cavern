using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGun : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public int maxAmmo;
    public int curAmmo;
    public GameObject bullet;
    public GameObject spentCase;
    public GameObject playerhead;
    public GameObject magazine;
    private GameObject player;
    private GameObject curMag;
    public float reloadTime;
    private float timer = 0;
    public float fireRate;
    private float lastShot = 0;
    private bool reloading = false;
    public Vector3 magOffset;
    public AudioClip gunShot;
    private AudioSource audioPlayer;
    void Start()
    {
        curAmmo = maxAmmo;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        audioPlayer = GetComponent<AudioSource>();
        player = transform.parent.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<CharacterMovement>().curHealth > 0){
            if(Input.GetMouseButton(0))
                shootGun();

            if(Input.GetKeyDown("r") && curAmmo < maxAmmo || reloading)
                reloadGun();
        }
    }
    void shootGun(){
        if(fireRate - lastShot < 0 && curAmmo > 0 && !reloading){ 
            audioPlayer.PlayOneShot(gunShot,0.04f);
            Debug.Log("firing gun");
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            Instantiate(bullet,transform.position+offset,playerhead.transform.rotation);
            Instantiate(spentCase,transform.position+offset,playerhead.transform.rotation);
            curAmmo--;
            lastShot = 0;
        }
        else{
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            //play empty mag sound
        }
        lastShot += Time.deltaTime;
    }
    void reloadGun(){   

        if(reloading == false){
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            reloading = true;
            transform.GetChild(1).GetComponent<magazine_drop>().dropMag = true;
        }

        timer += Time.deltaTime;
        Debug.Log("Reloading gun");
        if(reloadTime - timer < 0){
            curAmmo = maxAmmo;
            curMag = Instantiate(magazine,transform.position + magOffset,playerhead.transform.rotation);
            curMag.transform.parent = transform;
            curMag.transform.localScale = new Vector3(1,1,1);
            reloading = false;
            timer = 0;
        }
        
    }
}
