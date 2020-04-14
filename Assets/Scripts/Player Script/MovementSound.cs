using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
	private AudioSource sound;

	[SerializeField]
    private AudioClip[] audio_clip;

    private CharacterController player_Controller;

    [HideInInspector]
    public float minimum_sound, maximum_sound;

    private float covered_distance;

    [HideInInspector]
    public float walk_distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player_Controller.velocity.sqrMagnitude>0 && player_Controller.isGrounded){
            covered_distance = covered_distance+Time.deltaTime;
            if(covered_distance > walk_distance){
                sound.volume = Random.Range(minimum_sound, maximum_sound);
                sound.clip = audio_clip[Random.Range(0, audio_clip.Length)];
                sound.Play();
                covered_distance = 0f;
            }
        }else{covered_distance = 0f;}
    }

    void Awake(){
    	sound = GetComponent<AudioSource>();
    	player_Controller = GetComponentInParent<CharacterController>();
    }
}
