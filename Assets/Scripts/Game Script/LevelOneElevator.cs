using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelOneElevator : MonoBehaviour
{
	private bool trigger_hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger_hit){
        	//Elevator_Bases
        	try{
        		GameObject the_elevator = GameObject.Find("Elevator_Bases");
        		the_elevator.transform.position += the_elevator.transform.up*Time.deltaTime;
        	}catch(Exception e){}
        }
    }

    public void change_trigger(){
    	trigger_hit = true;
    }
}
