using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class display_health : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Text healthText;
    private float timer;
    private Color baseColor;
    void Start()
    {
        baseColor = healthText.color;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + player.GetComponent<CharacterMovement>().curHealth;
        if(player.GetComponent<CharacterMovement>().curHealth < 5)
            alertColors();
        else   
            healthText.color = baseColor; 

    }
    void alertColors(){
        timer += Time.deltaTime;
        if(timer < .25){
            healthText.color = Color.red;
        }
        else if(timer > .5)
            timer = 0;
        else {
            healthText.color = Color.black;
        }
        
    }
}