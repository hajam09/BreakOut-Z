using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public GameObject move_platform;
	private bool inTrigger = false;
    private bool isAtBottom = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Elevator_Trigger").transform.position)<2f){
            if(isAtBottom && GameObject.Find("Elevator_Trigger").transform.position.y<5.6f){
                move_platform.transform.position += move_platform.transform.up * Time.deltaTime;
            }else{
                isAtBottom = false;
            }
            if(!isAtBottom && GameObject.Find("Elevator_Trigger").transform.position.y>1f){
                move_platform.transform.position -= move_platform.transform.up * Time.deltaTime;
            }else{
                isAtBottom = true;
            }
        }
        // print(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Elevator_Trigger").transform.position));
    	// Vector3 elevator_coordinates = GameObject.Find("Elevator_Platform").transform.position;

    	// if(inTrigger){
    	// 	move_platform.transform.position += move_platform.transform.up * Time.deltaTime;
    	// }else{
    	// 	if(elevator_coordinates.y>1f){
    	// 		move_platform.transform.position -= move_platform.transform.up * Time.deltaTime;
    	// 	}	
    	// }
    	// inTrigger = false;
    }

    // private void OnTriggerStay(){
    // 	inTrigger = true;
    // }
}
