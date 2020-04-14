using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Transform player_position, player_look;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool cursor_status = true;

    [SerializeField]
    private float mouse_sensivity = 5f;

    [SerializeField]
    private int steps = 10;

    [SerializeField]
    private float weight = 0.4f;

    [SerializeField]
    private float angle = 10f;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private Vector2 vision = new Vector2(-70f, 80f);

    private Vector2 vision_angles;

    private Vector2 mouse_vision;
    private Vector2 move;

    private float current_angle;

    private int recent_screen;

    // Start is called before the first frame update
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void Update () {

        LockAndUnlockCursor();
        if(Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }

    }

    void LockAndUnlockCursor() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    } // lock and unlock

    void LookAround(){
        mouse_vision = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        vision_angles.x += mouse_vision.x * mouse_sensivity * (invert ? 1f : -1f);
        vision_angles.y += mouse_vision.y * mouse_sensivity;

        vision_angles.x = Mathf.Clamp(vision_angles.x, vision.x, vision.y);
        player_look.localRotation = Quaternion.Euler(vision_angles.x, 0f, 0f);
        player_position.localRotation = Quaternion.Euler(0f, vision_angles.y, 0f);
    }
} // class