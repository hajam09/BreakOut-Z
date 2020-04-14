using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorAnimator : MonoBehaviour
{
	private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
    	animator = GetComponent<Animator>();
    }

    public void WalkingMotion(bool condition) {
        animator.SetBool("Walk", condition);
    }
}
