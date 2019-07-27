using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magazine_drop : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dropMag;
    private Rigidbody2D rb;
    private float despawnTime = 30;
    private float timer = 0;
    public AudioClip magRemove;
    AudioSource audioSource;
    void Start()
    {
        dropMag = false;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dropMag){
            audioSource.PlayOneShot(magRemove,1f);
            transform.parent = null;
            rb.bodyType = RigidbodyType2D.Dynamic;
            timer += Time.deltaTime;
            dropMag = false;
        }
        if(despawnTime - timer < 0)
            Destroy(this.gameObject);
    }
}
