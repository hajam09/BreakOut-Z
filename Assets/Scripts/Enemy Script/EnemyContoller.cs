using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState{
    ATTACK,
    PATROL,
    CHASE
}

public class EnemyContoller : MonoBehaviour
{
	private EnemyAnimationController enemy_animation_controller;
	private NavMeshAgent nav_mesh_agent;
	private ZombieState zombie_state;
	public float walking_velocity = 0.5f;
    public float running_velocity = 4f;
    public float max_proximity = 7f;//distance the player has to be near zombie in order for the attack
    private float current_proximity;//attack when player shoots
    public float attack_distance = 1.3f;//used to be 1.8f;
    public float gap_distance_chase = 2f;
    public float minimum_patrol_radius = 20f;
    public float maximum_patrol_radius = 60f;
    public float patrolling_duration = 15f;
    private float patrolling_timer;
    public float attack_delay = 2f;
    private float attack_timer;
    private Transform enemy_target;
    public GameObject attack_point;
    private EnemySound enemy_sound;
    // Start is called before the first frame update
    void Start()
    {
        zombie_state = ZombieState.PATROL;
        patrolling_timer = patrolling_duration;
        // when the enemy first gets to the player attack right away
        attack_timer = attack_delay;
        // memorize the value of chase distance so that we can put it back
        current_proximity = max_proximity;
        enemy_sound = GetComponentInChildren<EnemySound>();
    }

    // Update is called once per frame
    void Update()
    {
        if(zombie_state==ZombieState.PATROL){
	        nav_mesh_agent.isStopped = false;
	        nav_mesh_agent.speed = walking_velocity;
	        patrolling_timer += Time.deltaTime;

	        if(patrolling_timer > patrolling_duration){
	            // SetNewRandomDestination();
	            float random_radius = Random.Range(minimum_patrol_radius, maximum_patrol_radius);
	            Vector3 random_direction = Random.insideUnitSphere * random_radius;
	            random_direction = random_direction+transform.position;

	            NavMeshHit navigational_hit_point;
		        NavMesh.SamplePosition(random_direction, out navigational_hit_point, random_radius, -1);
		        nav_mesh_agent.SetDestination(navigational_hit_point.position);
	            // end of SetNewRandomDestionation function
	            patrolling_timer = 0f;
	        }

	        if(nav_mesh_agent.velocity.sqrMagnitude > 0) {
	            enemy_animation_controller.WalkingMotion(true);
	        } else {
	            enemy_animation_controller.WalkingMotion(false);
	        }

	        // test the distance between the player and the enemy
	        if(Vector3.Distance(transform.position, enemy_target.position) <= max_proximity){
	        	enemy_animation_controller.WalkingMotion(false);
	        	zombie_state = ZombieState.CHASE;
	        	// play spotted audio
	        	enemy_sound.Screaming_Sound();
	        }
        }
        if(zombie_state==ZombieState.CHASE){
        	nav_mesh_agent.isStopped = false;
        	nav_mesh_agent.speed = running_velocity;
        	nav_mesh_agent.SetDestination(enemy_target.position);//enemy run towards player

        	if(nav_mesh_agent.velocity.sqrMagnitude > 0) {
	            enemy_animation_controller.RunningMotion(true);
	        } else {
	            enemy_animation_controller.RunningMotion(false);
	        }

	        if(Vector3.Distance(transform.position, enemy_target.position) <= attack_distance){
	        	enemy_animation_controller.RunningMotion(false);
	        	enemy_animation_controller.WalkingMotion(false);
	        	zombie_state = ZombieState.ATTACK;
	        	if(max_proximity != current_proximity){
	        		max_proximity = current_proximity;
	        	}
	        } else if(Vector3.Distance(transform.position, enemy_target.position) > max_proximity){
	        	enemy_animation_controller.RunningMotion(false);
	        	zombie_state = ZombieState.PATROL;
	        	patrolling_timer = patrolling_duration;
	        	if(max_proximity != current_proximity){
	        		max_proximity = current_proximity;
	        	}
	        }
        }
        if(zombie_state==ZombieState.ATTACK){
        	nav_mesh_agent.isStopped = true;
        	nav_mesh_agent.velocity = Vector3.zero;
        	attack_timer += Time.deltaTime;
        	if(attack_timer > attack_delay){
        		enemy_animation_controller.AttackingMotion();
        		attack_timer = 0f;
        		// play attack sound
        		enemy_sound.Attacking_Sound();
        	}
        	// To give spacing for the player to make a run if zombie is next to the player.
        	float player_run_distance = attack_distance;//+gap_distance_chase;
        	if(Vector3.Distance(transform.position, enemy_target.position)>player_run_distance){
        		zombie_state = ZombieState.CHASE;
        	}

            // this.gameObject.transform.LookAt(2 * this.gameObject.transform.position - GameObject.Find("Player").transform.position);

            // print("zombie y angle is: "+this.gameObject.transform.rotation.eulerAngles.y);
            // print("player y angle is: "+GameObject.Find("Player").transform.rotation.eulerAngles.y);
            // this.gameObject.transform.eulerAngles = new Vector3(this.gameObject.transform.eulerAngles.x, 180+GameObject.Find("Player").transform.rotation.eulerAngles.y, this.gameObject.transform.eulerAngles.z);

            // Works but the enemy slightly leans off the player.
            // Vector3 lookVector = enemy_target.transform.position - transform.position;
            // lookVector.y = transform.position.y;
            // Quaternion rot = Quaternion.LookRotation(lookVector);
            // transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }
    }

    void Awake(){
        enemy_animation_controller = GetComponent<EnemyAnimationController>();
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        enemy_target = GameObject.FindWithTag("Player").transform;
        // enemy_Audio = GetComponentInChildren<EnemyAudio>();
    }

    void AttackPointOn(){
        attack_point.SetActive(true);
    }

    void AttackPointOff(){
        if(attack_point.activeInHierarchy){
            attack_point.SetActive(false);
        }
    }

    public ZombieState Zombie_State{
        get; set;
    }

    public ZombieState get_current_zombie_state(){
        return zombie_state;
    }
}
