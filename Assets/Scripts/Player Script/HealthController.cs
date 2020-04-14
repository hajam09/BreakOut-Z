using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
	private EnemyAnimationController enemy_animation_controller;
	private NavMeshAgent nav_mesh_agent;
	private EnemyContoller enemy_controller;

	public float health_value = 100f;
	public bool check_player;
	public bool check_zombie;
	private bool check_dead;
    private EnemySound enemy_sound;
    private PlayerStatistics player_statistics;
    private WeaponController weapon_controller;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string current_scene_name = SceneManager.GetActiveScene().name;
        try{
            //used to preveent object not found when the game restarts whenthe palaer dies
            if(Vector3.Distance(GameObject.Find("First_aid_kit_4_Mat").transform.position, GameObject.Find("Player").transform.position)<1.5 && check_player){
                health_value=100f;
                player_statistics.View_Health_Statistics(health_value);//For instant health update to screen
            }
        }
        catch (Exception e){
            print("error HealthController");
        }
    }

    void Awake(){
    	if(check_zombie){
    		enemy_animation_controller = GetComponent<EnemyAnimationController>();
    		nav_mesh_agent = GetComponent<NavMeshAgent>();
    		enemy_controller = GetComponent<EnemyContoller>();
    		// get enemy audio
            enemy_sound = GetComponentInChildren<EnemySound>();
    	}
    	if(check_player){
            weapon_controller = GetComponent<WeaponController>();
            player_statistics = GetComponent<PlayerStatistics>();
            if(PlayerPrefs.GetFloat("playerhealth")>0){
                health_value = PlayerPrefs.GetFloat("playerhealth");
                player_statistics.View_Health_Statistics(health_value);
            }
    	}
    }

    public void MakeDamage(float damage_value){
        if(health_value <= 0f){
            KillCharacter();
            check_dead = true;
        }
    	if(check_dead){return;}
    	health_value -= damage_value;

    	// Need to update the UI that displays users stats
    	if(check_player){
            // show the stats(display the health UI value)
            // player_Stats.Display_HealthStats(health);
            player_statistics.View_Health_Statistics(health_value);
        }
        if(check_zombie){
            // if(enemy_controller.Zombie_State == ZombieState.PATROL){}
            if(enemy_controller.get_current_zombie_state() == ZombieState.PATROL){
                enemy_controller.max_proximity = 25f;//higher means enemy will change you even if you are very far.
                // here figuring out why zombie state is attack at the beginning
            }
        }
    }

    void KillCharacter(){
        if(check_player){
            // delete all enemeies because the player died
            GameObject[] game_object_list = GameObject.FindGameObjectsWithTag("Enemy");
            for (int ii = 0; ii < game_object_list.Length; ii++) {
                game_object_list[ii].GetComponent<EnemyContoller>().enabled = false;
            }
            // call enemy manager to stop spawning enemis
            ZombieSpawner.zombie_spawner.StopSpawning();

            GetComponent<PlayerMoveAction>().enabled = false;
            GetComponent<FireBulletController>().enabled = false;
            GetComponent<WeaponController>().getter_user_weapon().gameObject.SetActive(false);

            // Making the player fall down.
            GameObject player_object = GameObject.Find("Player");
            player_object.transform.position -= player_object.transform.up*Time.deltaTime;
        }
        if(check_zombie){
            // GetComponent<Animator>().enabled = false;
            // GetComponent<BoxCollider>().isTrigger = false;
            // enemy_animation_controller.DeadMotion();
            // GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);//check if this is needed after testing
            // enemy_controller.enabled = false;
            // nav_mesh_agent.enabled = false;
            // enemy_animation_controller.enabled = false;


            // StartCoroutine(DeadSound());
            // EnemyManager spawn more enemies
            // EnemyManager.instance.EnemyDied(true);

            // Better use this since zombie has dead animation
            nav_mesh_agent.velocity = Vector3.zero;
            nav_mesh_agent.isStopped = true;
            enemy_controller.enabled = false;
            enemy_animation_controller.DeadMotion();

            StartCoroutine(Dying_Sound_Trigger());

            // Notifying ZombieSpawner to spawn more enemies
            ZombieSpawner.zombie_spawner.Zombie_is_Dead();// This may be the code that respawns the zomibe automatcally.
        }

        // used to stop or restart the game
        if(tag == "Player"){
            Invoke("Reset_Game", 3f);
        }else{
            Invoke("Turn_Off_Game", 3f);
        }
    }

    void Reset_Game(){
        PlayerPrefs.SetInt("score",weapon_controller.get_score());
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");//Used to be Level 1
        // Application.LoadLevel("Base");
    }

    void Turn_Off_Game(){
        gameObject.SetActive(false);
    }

    IEnumerator Dying_Sound_Trigger() {
        yield return new WaitForSeconds(0.3f);
        enemy_sound.Dying_Sound();
    }

    public bool return_is_player(){
        if(check_player){
            return true;
        }
        return false;
    }

    public float return_player_health(){
        return health_value;
    }
}
