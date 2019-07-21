using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousePos;
    public float lookSpeed;
    public GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool facingRight = player.GetComponent<CharacterMovement>().facingRight = false;
        mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector2.Lerp(transform.position, mousePos, lookSpeed);

        
    }
}
