using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class display_ammo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currentGun;
    public Text ammoText;
    private float timer;
    private Color baseColor;
    void Start()
    {
        baseColor = ammoText.color;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "" + currentGun.GetComponent<fireGun>().curAmmo;
        if(currentGun.GetComponent<fireGun>().curAmmo < 5)
            alertColors();
        else   
            ammoText.color = baseColor; 

    }
    void alertColors(){
        timer += Time.deltaTime;
        if(timer < .25){
            ammoText.color = Color.red;
        }
        else if(timer > .5)
            timer = 0;
        else {
            ammoText.color = Color.black;
        }
        
    }
    
}
