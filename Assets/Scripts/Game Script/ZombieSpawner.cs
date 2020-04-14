using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
	public static ZombieSpawner zombie_spawner;

	[SerializeField]
    private GameObject zombie_instance;

    public Transform[] zombie_spawn_points;

    [SerializeField]
    private int zombie_count;

    private int starting_zombie_counter;
    public float spawning_delay = 20f;//10f
    // Start is called before the first frame update
    void Start()
    {
        starting_zombie_counter = zombie_count;

        SpawnZombies();

        // StartCoroutine("CheckToSpawnZombies"); //to prevent zomibe from spawning
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        if(zombie_spawner==null){zombie_spawner = this;}
	}

	void SpawnZombies(){
		int ii = 0;
		for(int i = 0; i < zombie_count; i++){
			if(ii >= zombie_spawn_points.Length){ii = 0;}
			Instantiate(zombie_instance, zombie_spawn_points[ii].position, Quaternion.identity);
			ii++;
		}
		zombie_count = 0;
	}

	public void Zombie_is_Dead(){
		zombie_count++;
		if(zombie_count > starting_zombie_counter){
			zombie_count = starting_zombie_counter;
		}
	}

	IEnumerator CheckToSpawnZombies(){
		yield return new WaitForSeconds(spawning_delay);
        SpawnZombies();
        StartCoroutine("CheckToSpawnZombies");
	}

	public void StopSpawning(){
        StopCoroutine("CheckToSpawnZombies");//to prevent zomibe from spawning
    }
}
