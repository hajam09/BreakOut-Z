using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{//to do: to make weapon unloacble, create 3 variable for each gun and set it false, if the payer reached the gun, set the variable to true.
    //
	[SerializeField]
    private WeaponSystem[] weapon_list;

    private int index;
    private int revolver_ammo_max = 30;
    private int revolver_ammo_current;
    private int shot_gun_ammo_max = 20;
    private int shot_gun_ammo_current;
    private int assault_rifle_ammo_max = 120;
    private int assault_rifle_ammo_current;

    private int[] bullet_counter = new int[3];
    private PlayerStatistics player_statistics;//For instant ammo update to screen

    private bool assault_rifle_unlocked = true;
    private bool revolver_unlocked = true;
    private bool shot_gun_unlocked = true;

    private int score;

    void Awake(){
        player_statistics = GetComponent<PlayerStatistics>();//For instant ammo update to screen
    }
    
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        if(PlayerPrefs.HasKey("weaponindex")){
                index = PlayerPrefs.GetInt("weaponindex");
        }
        weapon_list[index].gameObject.SetActive(true);

        revolver_ammo_current = revolver_ammo_max;
        shot_gun_ammo_current = shot_gun_ammo_max;
        assault_rifle_ammo_current = assault_rifle_ammo_max;

        if(PlayerPrefs.HasKey("revolverammo")){
            bullet_counter[0] = PlayerPrefs.GetInt("revolverammo");
        }else{
            bullet_counter[0] = revolver_ammo_current;
        }
        if(PlayerPrefs.HasKey("shotgunammo")){
            bullet_counter[1] = PlayerPrefs.GetInt("shotgunammo");
        }else{
            bullet_counter[1] = shot_gun_ammo_current;
        }
        if(PlayerPrefs.HasKey("assaultammo")){
            bullet_counter[2] = PlayerPrefs.GetInt("assaultammo");
        }else{
            bullet_counter[2] = assault_rifle_ammo_current;
        }
        score = 0;
        if(PlayerPrefs.HasKey("score")){
            score = PlayerPrefs.GetInt("score");
        }
        // if(SceneManager.GetActiveScene().name=="Level 2" || SceneManager.GetActiveScene().name=="Level 3"){
        //     bullet_counter[0] = PlayerPrefs.GetInt("revolverammo");
        //     bullet_counter[1] = PlayerPrefs.GetInt("shotgunammo");
        //     bullet_counter[2] = PlayerPrefs.GetInt("assaultammo");
        // }else{
        //     bullet_counter[0] = revolver_ammo_current;
        //     bullet_counter[1] = shot_gun_ammo_current;
        //     bullet_counter[2] = assault_rifle_ammo_current;
        // }
        player_statistics.View_Ammo_Statistics(get_current_weapon_ammo().ToString());
        player_statistics.View_Score_Statistics(score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // TO unlock guns
        try{
            if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Assault Rifle Table").transform.position)<3 && assault_rifle_unlocked==false){
                assault_rifle_unlocked = true;
                PlayerPrefs.SetInt("rifle_availability",1);
                GameObject.Find("Assault Rifle Table").SetActive(false);
            }
        }catch (Exception e){
        }
        try{
            if(Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Shotgun Table").transform.position)<3 && shot_gun_unlocked==false){
                shot_gun_unlocked = true;
                PlayerPrefs.SetInt("shotgun_availability",1);
                GameObject.Find("Shotgun Table").SetActive(false);
            }
        }catch (Exception e){
        }
        string current_scene_name = SceneManager.GetActiveScene().name;
        try{//used to preveent object not found when the game restarts when the player dies
            if(Vector3.Distance(GameObject.Find("AmmoBox").transform.position, GameObject.Find("Player").transform.position)<1){
                bullet_counter[0] = revolver_ammo_current;
                bullet_counter[1] = shot_gun_ammo_current;
                bullet_counter[2] = assault_rifle_ammo_current;
                player_statistics.View_Ammo_Statistics(get_current_weapon_ammo().ToString());//For instant ammo update to screen
            }
        }
        catch (Exception e){}

        if(PlayerPrefs.HasKey("rifle_availability")){assault_rifle_unlocked = true;}
        if(PlayerPrefs.HasKey("shotgun_availability")){shot_gun_unlocked = true;}

        // print("this weapon ammo count: "+ bullet_counter[index]);
    	// for each condition check if index!=0 and so on to prevent weapon spawning.
        if(Input.GetKeyDown(KeyCode.Alpha1)&&revolver_unlocked==true){
        	weapon_list[index].gameObject.SetActive(false);
        	index = 0;
        	weapon_list[index].gameObject.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.Alpha2)&&shot_gun_unlocked==true){
        	weapon_list[index].gameObject.SetActive(false);
        	index = 1;
        	weapon_list[index].gameObject.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.Alpha3)&&assault_rifle_unlocked==true){
        	weapon_list[index].gameObject.SetActive(false);
        	index = 2;
        	weapon_list[index].gameObject.SetActive(true);
        }
        if(revolver_unlocked==true&&shot_gun_unlocked==true&&assault_rifle_unlocked==true){
            // Change Weapon on scroll which can be only used when all weapons unlocked
            if(Input.GetAxis("Mouse ScrollWheel")>0f){
                //forward or previous gun
                weapon_list[index].gameObject.SetActive(false);
                index = index-1;
                if(index==-1){
                    index = weapon_list.Length-1;
                }
                weapon_list[index].gameObject.SetActive(true);
            }else if(Input.GetAxis("Mouse ScrollWheel")<0f){
                //backward or next gun
                weapon_list[index].gameObject.SetActive(false);
                index = index+1;
                if(index==weapon_list.Length){
                    index = 0;
                }
                weapon_list[index].gameObject.SetActive(true);
            }
        }
        player_statistics.View_Ammo_Statistics(get_current_weapon_ammo().ToString());
        player_statistics.View_Score_Statistics(score.ToString());
    }

    public WeaponSystem getter_user_weapon(){
        return weapon_list[index];
    }

    public int get_current_weapon_ammo(){
        // if(index==0){
        //     return revolver_ammo_current;
        // } else if(index==1){
        //     return shot_gun_ammo_current;
        // } else if(index==2){
        //     return assault_rifle_ammo_current;
        // }
        return bullet_counter[index];//doubt this line will be ever reached
    }

    public void set_current_weapon_ammo(){
        bullet_counter[index] -= 1;
        //reduce the bullet count for each left click
    }

    public int get_weapon_ammo(int val){
        return bullet_counter[val];
    }

    public int return_weapon_index(){
        return index;
    }

    public void set_score(){
        score+=5;
    }

    public int get_score(){
        return score;
    }
}