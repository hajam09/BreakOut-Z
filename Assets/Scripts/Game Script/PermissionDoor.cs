using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PermissionDoor : MonoBehaviour
{
	private bool requires_key = true;
	private bool key_gathered = false;
    public GameObject message_object;
    // Start is called before the first frame update
    void Start()
    {
        message_object.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        try{
            if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Key_To_Door").transform.position)<2 && key_gathered==false){
                key_gathered = true;
                message_object.SetActive(true);
                print("key aquired");
                StartCoroutine("WaitFunction");
            }
        }catch(Exception e){}
        try{
            if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Armory_Door").transform.position)<5 && key_gathered==true){
                GameObject the_door = GameObject.Find("Armory_Door");
                if(the_door.transform.position.x>24){
                    the_door.transform.position -= the_door.transform.forward*Time.deltaTime;
                }
                if(the_door.transform.position.x<4){
                    the_door.SetActive(false);
                }

            }
        }catch(Exception e){}
        
            
    }

    IEnumerator WaitFunction(){
        yield return new WaitForSeconds(5);
        Destroy(message_object);
    }
}
