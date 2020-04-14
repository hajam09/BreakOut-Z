using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelController : MonoBehaviour
{
    private HealthController health_controller;
    private WeaponController weapon_controller;

    private GameObject[] all_people;
    private SurvivorFollow survivor_follow;

    public Image black;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
        health_controller = GetComponent<HealthController>();
        weapon_controller = GetComponent<WeaponController>();
    }

    IEnumerator Fading(string scene_name){
        animator.SetBool("Fade",true);
        yield return new WaitUntil(()=>black.color.a==1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene_name);

    }

    // Update is called once per frame
    void Update(){
    //     PlayerPrefs.DeleteKey("Level_1_to_2_Survivors");
    //     PlayerPrefs.DeleteKey("Level_2_to_3_Survivors");
    //     PlayerPrefs.DeleteKey("2-1-Coordinates");
    //     PlayerPrefs.DeleteKey("3-2-Coordinates");
        PlayerPrefs.DeleteKey("weaponindex");
        PlayerPrefs.DeleteKey("revolverammo");
        PlayerPrefs.DeleteKey("shotgunammo");
        PlayerPrefs.DeleteKey("assaultammo");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("survivors");
        PlayerPrefs.SetFloat("playerhealth",0f);
        string current_scene_name = SceneManager.GetActiveScene().name;
        if(current_scene_name=="Level 1"){
        	// Level 1
        	// print("Level 1");
            // if(Vector3.Distance(GameObject.Find("Elevator_Bases").transform.position, GameObject.Find("To_Level_2_Wall").transform.position)<48.03039){
        	if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("To_Level_2_Wall").transform.position)<2){
        		if(health_controller.return_is_player()){
                    PlayerPrefs.SetFloat("playerhealth",health_controller.return_player_health());
                }
                PlayerPrefs.SetInt("revolverammo",weapon_controller.get_weapon_ammo(0));
                PlayerPrefs.SetInt("shotgunammo",weapon_controller.get_weapon_ammo(1));
                PlayerPrefs.SetInt("assaultammo",weapon_controller.get_weapon_ammo(2));
                PlayerPrefs.SetInt("weaponindex",weapon_controller.return_weapon_index());
                PlayerPrefs.SetInt("score",weapon_controller.get_score());

                int counter= 0;
                all_people = GameObject.FindGameObjectsWithTag("Survivor");
                foreach (GameObject respawn in all_people){
                    survivor_follow = respawn.GetComponent<SurvivorFollow>();
                    if(survivor_follow.check_following()){
                        counter = counter+1;
                    }
                }
                PlayerPrefs.SetInt("Level_1_to_2_Survivors",counter);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
                // StartCoroutine(Fading("Level 2"));
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        	}
    	} else if(current_scene_name=="Level 2"){
    		// Level 2
    		// print("Level 2");
    		if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Wall_To_Level_3").transform.position)<2){
    			if(health_controller.return_is_player()){
                    PlayerPrefs.SetFloat("playerhealth",health_controller.return_player_health());
                }
                PlayerPrefs.SetInt("revolverammo",weapon_controller.get_weapon_ammo(0));
                PlayerPrefs.SetInt("shotgunammo",weapon_controller.get_weapon_ammo(1));
                PlayerPrefs.SetInt("assaultammo",weapon_controller.get_weapon_ammo(2));
                PlayerPrefs.SetInt("weaponindex",weapon_controller.return_weapon_index());
                PlayerPrefs.SetInt("score",weapon_controller.get_score());

                int counter= 0;
                all_people = GameObject.FindGameObjectsWithTag("Survivor");
                foreach (GameObject respawn in all_people){
                    survivor_follow = respawn.GetComponent<SurvivorFollow>();
                    if(survivor_follow.check_following()){
                        counter = counter+1;
                    }
                }
                PlayerPrefs.SetInt("Level_2_to_3_Survivors",counter);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3");
                // StartCoroutine(Fading("Level 3"));
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    		}
    		// if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Wall_To_Level_1").transform.position)<2.5){
      //           if(health_controller.return_is_player()){
      //               PlayerPrefs.SetFloat("playerhealth",health_controller.return_player_health());
      //           }
      //           PlayerPrefs.SetInt("revolverammo",weapon_controller.get_weapon_ammo(0));
      //           PlayerPrefs.SetInt("shotgunammo",weapon_controller.get_weapon_ammo(1));
      //           PlayerPrefs.SetInt("assaultammo",weapon_controller.get_weapon_ammo(2));
      //           PlayerPrefs.SetInt("weaponindex",weapon_controller.return_weapon_index());
      //           PlayerPrefs.SetString("2-1-Coordinates","45.8,0.8,1.2");
      //           UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
      //   	}
    	} else if(current_scene_name=="Level 3"){//Wall_To_Level_2
    		// Level 3
            // print("Level 3");
            // if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Wall_To_Level_2").transform.position)<2.5){
            //     if(health_controller.return_is_player()){
            //         PlayerPrefs.SetFloat("playerhealth",health_controller.return_player_health());
            //     }
            //     PlayerPrefs.SetInt("revolverammo",weapon_controller.get_weapon_ammo(0));
            //     PlayerPrefs.SetInt("shotgunammo",weapon_controller.get_weapon_ammo(1));
            //     PlayerPrefs.SetInt("assaultammo",weapon_controller.get_weapon_ammo(2));
            //     PlayerPrefs.SetInt("weaponindex",weapon_controller.return_weapon_index());
            //     PlayerPrefs.SetString("3-2-Coordinates","34.4,4.1,1.3");
            //     UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            // }
            if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Finish_Line").transform.position)<3.5f){
                // need to count the buber of cubes who are just rotating,
                int counter= 0;
                all_people = GameObject.FindGameObjectsWithTag("Survivor");
                foreach (GameObject respawn in all_people){
                    survivor_follow = respawn.GetComponent<SurvivorFollow>();
                    if(survivor_follow.check_following()){
                        counter = counter+1;
                    }
                }
                PlayerPrefs.SetInt("survivors",counter);
                PlayerPrefs.SetInt("score",weapon_controller.get_score());
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
                // StartCoroutine(Fading("Result"));
            }
    	}
    }
}
