using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float damage_value = 2f;
	public float boundary = 2f;
	public LayerMask layer_mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //The Attack pointer needs to follow the zombie hand
        // Vector3 parent_coordinate = GameObject.Find("Z_R_ArmPalm").transform.position;
        // transform.position = new Vector3(parent_coordinate.x, parent_coordinate.y, parent_coordinate.z);
        // OR

        // if(transform.parent.position != transform.position){
        //     transform.parent.position = transform.position;
        //     transform.localPosition = Vector3.zero;
        // }
        // 

        Collider[] interceptions = Physics.OverlapSphere(transform.position, boundary, layer_mask);
        if(interceptions.Length > 0){
        	// An interception is made with a game object.
            interceptions[0].gameObject.GetComponent<HealthController>().MakeDamage(damage_value);
        	gameObject.SetActive(false);
        }
    }
}