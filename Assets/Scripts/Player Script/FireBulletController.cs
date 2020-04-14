using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletController : MonoBehaviour
{
	private WeaponController weaponcontroller;
	public float assault_rifle_shot_rate = 15f;
	private float sleep;
	public float penetration_value = 20f;
    private GameObject red_dot_site;
    private Camera main_camera;
    private PlayerStatistics player_statistics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponcontroller.getter_user_weapon().firecontroller == FireController.MULTIPLE){
        	// assault rifle
        	if(Input.GetMouseButton(0) && Time.time>sleep && weaponcontroller.get_current_weapon_ammo()>0){
        	//to prevent 60 bulltes from being fired for every frame if problem occurs change it to GetMouseButton(0)
        		sleep = Time.time + 1f / assault_rifle_shot_rate;
        		weaponcontroller.getter_user_weapon().Shooting_Animation();
        		Shoot();
                string new_ammount_value = weaponcontroller.get_current_weapon_ammo().ToString();
                player_statistics.View_Ammo_Statistics(new_ammount_value);
        	}
        }
        else{
        	// for shotgun and revolver
        	if(Input.GetMouseButtonDown(0)){
        		if(weaponcontroller.getter_user_weapon().bulletcontroller == BulletController.BULLET && weaponcontroller.get_current_weapon_ammo()>0){
        			weaponcontroller.getter_user_weapon().Shooting_Animation();
        			Shoot();
                    string new_ammount_value = weaponcontroller.get_current_weapon_ammo().ToString();
                    player_statistics.View_Ammo_Statistics(new_ammount_value);
        		}else{
        			print("spear or arrow");
        		}
        	}
        }
    }

    void Awake(){
    	weaponcontroller = GetComponent<WeaponController>();
        red_dot_site = GameObject.FindWithTag("RedDotSite");
        main_camera = Camera.main;
        player_statistics = GetComponent<PlayerStatistics>();
    }

    void Shoot(){
        weaponcontroller.set_current_weapon_ammo();
    	RaycastHit ray_cast_hit;
    	if(Physics.Raycast(main_camera.transform.position, main_camera.transform.forward, out ray_cast_hit)){
            if(ray_cast_hit.transform.tag=="Enemy"){
                ray_cast_hit.transform.GetComponent<HealthController>().MakeDamage(penetration_value);
                weaponcontroller.set_score();
            }
            // if(ray_cast_hit.transform.name=="ElevatorTrigger"){
            //     ray_cast_hit.transform.GetComponent<LevelOneElevator>().change_trigger();
            // }
    		// print("we hit: "+ray_cast_hit.transform.gameObject.name);
        }
    }
}
