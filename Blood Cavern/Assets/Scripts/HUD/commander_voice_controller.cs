using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class commander_voice_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public AudioClip missionBrief_c;
    private bool mB = false;
    public AudioClip equipmentBreif_c;
    private bool eB = false;
    public AudioClip giantAnts_c;
    private bool gA = false;
    public AudioClip spelunkerOne_c;
    private bool sO = false;
    public AudioClip spelunkerTwo_c;
    private bool mT = false;
    public AudioClip newMission_c;
    private bool nM = false;
    public AudioClip playerDeath_c;
    private bool pD = false;
    public AudioClip winGame_c;
    private bool wG = false;
    public AudioClip menu_in;
    public AudioClip menu_out;
    private AudioSource[] audioSource;
    private float menuVolume = 0.2f;
    public float voiceVolume;
    private RawImage image;
    public float showSpeed;
    private bool toggleUI = false;
    private float waitTime = 1;
    private float waitTimer = 0;


    void Start()
    {
        image = GetComponent<RawImage>();
        image.transform.localScale = new Vector3(0,3,1);
        audioSource = GetComponents<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer += Time.deltaTime;
        if(player.GetComponent<CharacterMovement>().curHealth <= 0 && !pD){
            toggleUI = true;
            audioSource[0].PlayOneShot(playerDeath_c,voiceVolume);
            pD = true;
            waitTimer = 0;
        }
        if(audioSource[0].isPlaying)
            toggleUI = true;

        if(!audioSource[0].isPlaying && waitTime - waitTimer < 0){
            toggleUI = false;
        }
        showUI();
    }
    public void playClip(AudioClip clip){
        toggleUI = true;
    }
    private void showUI(){
        if(toggleUI && image.transform.localScale.x <3){
            audioSource[1].PlayOneShot(menu_out,menuVolume);
            var scale = image.transform.localScale;
            scale.x += Time.deltaTime*showSpeed;
            image.transform.localScale = scale;
        }
            
        else if(!toggleUI && image.transform.localScale.x > 0){
            audioSource[1].PlayOneShot(menu_in,menuVolume);
            var scale = image.transform.localScale;
            scale.x -= Time.deltaTime*showSpeed;
            image.transform.localScale = scale;
        }
    }

}
