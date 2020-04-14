using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAction : MonoBehaviour
{
	private PlayerMoveAction character_movement;
	public float sprinting_velocity= 8f;//10f
    public float moving_velocity = 4;//5f

    private Transform look_Position;
    private float standing_Height = 1.3f;//1.6f

    private MovementSound player_movement;
    private float sprint_volume = 1f;
    private float walk_sound_minimum = 0.2f;
    private float walk_sound_maximum = 0.6f;

    private float distance_sound_walking = 0.4f;//used to prevent making immediate sound
    private float distance_sound_sprinting = 0.25f;

    private PlayerStatistics player_statistics;
    private float spring_max = 100f;
    private float sprint_per_run = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player_movement.minimum_sound = walk_sound_minimum;
        player_movement.maximum_sound = walk_sound_maximum;
        player_movement.walk_distance = distance_sound_walking;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && spring_max>0f){
    		character_movement.velocity = sprinting_velocity;
            player_movement.minimum_sound = sprint_volume;
            player_movement.maximum_sound = sprint_volume;
            player_movement.walk_distance = distance_sound_sprinting;
    	}
    	if(Input.GetKeyUp(KeyCode.LeftShift)){
    		character_movement.velocity = moving_velocity;
            player_movement.minimum_sound = walk_sound_minimum;
            player_movement.maximum_sound = walk_sound_maximum;
            player_movement.walk_distance = distance_sound_walking;
    	}
        if(Input.GetKey(KeyCode.LeftShift)){
            if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.DownArrow)||Input.GetKey("w")||Input.GetKey("a")||Input.GetKey("s")||Input.GetKey("d")){
                // To check if user is actaully sprinting or not.
                spring_max -= sprint_per_run * Time.deltaTime;
                if(spring_max <= 0f){
                    spring_max = 0f;

                    // Cannot run anymore, therefore user has to wait
                    character_movement.velocity = moving_velocity;
                    player_movement.minimum_sound = walk_sound_minimum;
                    player_movement.maximum_sound = walk_sound_maximum;
                    player_movement.walk_distance = distance_sound_walking;
                }
            }
            // spring_max -= sprint_per_run * Time.deltaTime;
            // if(spring_max <= 0f){
            //     spring_max = 0f;

            //     // Cannot run anymore, therefore user has to wait
            //     character_movement.velocity = moving_velocity;
            //     player_movement.minimum_sound = walk_sound_minimum;
            //     player_movement.maximum_sound = walk_sound_maximum;
            //     player_movement.walk_distance = distance_sound_walking;
            // }
        }else{
            if(spring_max!=100f){
                spring_max += (sprint_per_run / 2f) * Time.deltaTime;
                if(spring_max > 100f){
                    spring_max = 100f;
                }
            }
        }
    }

    void Awake(){
    	character_movement = GetComponent<PlayerMoveAction>();
    	look_Position = transform.GetChild(0);
        player_movement = GetComponentInChildren<MovementSound>();
        player_statistics = GetComponent<PlayerStatistics>();
    }
}