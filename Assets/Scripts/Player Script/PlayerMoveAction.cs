using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveAction : MonoBehaviour
{
	private CharacterController player_Controller;
    private Vector3 move_direction;
    public float velocity = 4f;
    private float gravity = 20f;
    public float jump_force = 10f;
    private float vertical_velocity;

    // Start is called before the first frame update
    void Start()
    { 
    }

    void Awake() {
        player_Controller = GetComponent<CharacterController>();
        if(SceneManager.GetActiveScene().name=="Level 1" && PlayerPrefs.HasKey("2-1-Coordinates")){
            var two_to_one_coordinates = PlayerPrefs.GetString("2-1-Coordinates").Split(","[0]);
            float x = float.Parse(two_to_one_coordinates[0]);
            float y = float.Parse(two_to_one_coordinates[1]);
            float z = float.Parse(two_to_one_coordinates[2]);
            GameObject.Find("Player").transform.position = new Vector3(x, y, z);
        }
        if(SceneManager.GetActiveScene().name=="Level 2" && PlayerPrefs.HasKey("3-2-Coordinates")){
            var two_to_one_coordinates = PlayerPrefs.GetString("3-2-Coordinates").Split(","[0]);
            float x = float.Parse(two_to_one_coordinates[0]);
            float y = float.Parse(two_to_one_coordinates[1]);
            float z = float.Parse(two_to_one_coordinates[2]);
            GameObject.Find("Player").transform.position = new Vector3(x, y, z);
        }
    }
    
    void Update () {
        move_direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= velocity * Time.deltaTime;
        Gravity();
        player_Controller.Move(move_direction);
    }

    void Gravity() {
        vertical_velocity -= gravity * Time.deltaTime;
        // jump
        if(player_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_velocity = jump_force;
        }
        move_direction.y = vertical_velocity * Time.deltaTime;
    }
}