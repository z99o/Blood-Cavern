using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_aiming : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform reticle;
    public Transform player;
    public Quaternion shootAngle;
    public float offset;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var dir = reticle.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        shootAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion shootAngle2 =  new Quaternion(0,0,shootAngle.z,shootAngle.w);
        //Debug.Log(shootAngle2);
        transform.rotation = shootAngle2;

        if(angle > 90 || angle < -90){
            player.GetComponent<CharacterMovement>().facingRight = false;
            transform.Rotate(180,0,0);
        }
        else {
            player.GetComponent<CharacterMovement>().facingRight = true;
        }
        //Debug.Log(transform.rotation.x*Mathf.Rad2Deg);
    }
}
