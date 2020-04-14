using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorFollow : MonoBehaviour
{
	private NavMeshAgent nav_mesh_agent;
	private Transform player_target;
	public bool is_following = false;
    private SurvivorAnimator survivor_animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	// if(Vector3.Distance(GameObject.Find("Player").transform.position, gameObject.transform.position)<1.3f && Vector3.Distance(GameObject.Find("Player").transform.position, gameObject.transform.position)>1f){
    	// 	nav_mesh_agent.isStopped = false;
    	// 	nav_mesh_agent.speed = 1f;
    	// 	nav_mesh_agent.SetDestination(player_target.position);//enemy run towards player
    	// }

		var dist = Vector3.Distance(GameObject.Find("Player").transform.position, gameObject.transform.position);
		var lookDir = GameObject.Find("Player").transform.position - gameObject.transform.position;
		lookDir.y = 0;

		if(dist < 3f){
			gameObject.transform.rotation = Quaternion.Slerp( gameObject.transform.rotation, Quaternion.LookRotation(lookDir), 5*Time.deltaTime);

			if(dist > 2f){
				gameObject.transform.position += gameObject.transform.forward * 5 * Time.deltaTime;
                survivor_animator.WalkingMotion(true);
			}
            else{
                survivor_animator.WalkingMotion(false);
            }
			is_following = true;
		}else{
            is_following=false;
            survivor_animator.WalkingMotion(false);
        }
	}

    void Awake(){
    	// nav_mesh_agent = GetComponent<NavMeshAgent>();
    	// player_target = GameObject.FindWithTag("Player").transform;
        survivor_animator = GetComponent<SurvivorAnimator>();
    }

    public bool check_following(){
    	return is_following;
    }
}
