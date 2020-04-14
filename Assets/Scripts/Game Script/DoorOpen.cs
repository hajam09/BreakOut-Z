﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
	public GameObject left_door;
	public GameObject right_door;

    private AudioSource audio_source;

    [SerializeField]
    private AudioClip door_sound;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("DoorOpenGate").transform.position)<3){
    		if(right_door.transform.position.z<-5){
    			right_door.transform.position -= right_door.transform.right*Time.deltaTime;
                if(!audio_source.isPlaying){
                    audio_source.clip = door_sound;
                    audio_source.Play();
                }
    		}else{audio_source.Stop();}
    		if(left_door.transform.position.z>-11){
    			left_door.transform.position += left_door.transform.right*Time.deltaTime;
                if(!audio_source.isPlaying){
                    audio_source.clip = door_sound;
                    audio_source.Play();
                }
    		}else{audio_source.Stop();}
    	}else{
    		if(right_door.transform.position.z>-8){
    			right_door.transform.position += right_door.transform.right*Time.deltaTime;
                if(!audio_source.isPlaying){
                    audio_source.clip = door_sound;
                    audio_source.Play();
                }
    		}else{audio_source.Stop();}
    		if(left_door.transform.position.z<-8){
    			left_door.transform.position -= left_door.transform.right*Time.deltaTime;
                if(!audio_source.isPlaying){
                    audio_source.clip = door_sound;
                    audio_source.Play();
                }
    		}else{audio_source.Stop();}
    	}
    }
}
