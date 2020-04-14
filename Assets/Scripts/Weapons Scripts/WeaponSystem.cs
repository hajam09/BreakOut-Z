using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimController {
    AIM
}

public enum FireController {
    SINGLE,
    MULTIPLE
}

public enum BulletController {
    BULLET
}

public class WeaponSystem : MonoBehaviour
{
	private Animator animator;
	public AimController aim;
	public FireController firecontroller;
	public BulletController bulletcontroller;
	public GameObject bullet_interception;

	[SerializeField]
    private AudioSource reloading_sound, shooting_sound;

	[SerializeField]
    private GameObject muzzle_flash;

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

    public void Shooting_Animation(){
        animator.SetTrigger("FireBullet");
    }

    void MuzzleFlashEngaged(){
        muzzle_flash.SetActive(true);
    }

    void MuzzleFlashDisengaged(){
        muzzle_flash.SetActive(false);
    }

    void AttackPointOn(){
        bullet_interception.SetActive(true);
    }

    void AttackPointOff(){
        if(bullet_interception.activeInHierarchy){
            bullet_interception.SetActive(false);
        }
    }

    void ShootingSound(){
        shooting_sound.Play();
    }

    void ReloadingSound(){
        reloading_sound.Play();
    } 
}
