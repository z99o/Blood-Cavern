using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGun : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public int maxAmmo;
    private int curAmmo;
    public GameObject bullet;
    public GameObject spentCase;
    public GameObject playerhead;
    public float fireRate;
    private float lastShot = 0;
    void Start()
    {
        curAmmo = maxAmmo;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
            shootGun();
        else 
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        if(Input.GetMouseButton(1) && curAmmo < maxAmmo)
            reloadGun();
    }
    void shootGun(){
        if(fireRate - lastShot < 0){ 
            Debug.Log("firing gun");
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            Instantiate(bullet,transform.position+offset,playerhead.transform.rotation);
            Instantiate(spentCase,transform.position+offset,playerhead.transform.rotation);
            curAmmo--;
            lastShot = 0;
        }
        lastShot += Time.deltaTime;
    }
    void reloadGun(){
        Debug.Log("Reloading gun");
        curAmmo = maxAmmo;
    }
}
