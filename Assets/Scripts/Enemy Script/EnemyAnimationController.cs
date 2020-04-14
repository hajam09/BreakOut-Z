using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
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

    public void RunningMotion(bool condition) {
        animator.SetBool("Run", condition);
    }

    public void AttackingMotion(){
        animator.SetTrigger("Attack");
    }

    public void DeadMotion() {
        animator.SetTrigger("Dead");
    }
}
