using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//starrt here, if level one and then spawn and so on, wimilar
public class SurvivorSpawner : MonoBehaviour
{
	[SerializeField]
    private GameObject survivor_instance;

    public Transform[] survivor_spawn_points;

    [SerializeField]
    private int survivor_count;
    // Start is called before the first frame update
    void Start()
    {
        string current_scene_name = SceneManager.GetActiveScene().name;
        if(current_scene_name=="Level 1"){
            int ii = 0;
            for(int i = 0; i < survivor_count; i++){
                Instantiate(survivor_instance, survivor_spawn_points[ii].position, Quaternion.identity);
                ii++;
            }
        }else if(current_scene_name=="Level 2"){
            int ii = 0;
            for(int i = 0; i < survivor_count; i++){
                Instantiate(survivor_instance, survivor_spawn_points[ii].position, Quaternion.identity);
                ii++;
            }
            if(PlayerPrefs.HasKey("Level_1_to_2_Survivors")){
                var level_1_survivors = PlayerPrefs.GetInt("Level_1_to_2_Survivors");
                for(int i = 0; i < level_1_survivors; i++){
                    Instantiate(survivor_instance, GameObject.Find("Player").transform.position+Vector3.right * Random.Range(-2,2), Quaternion.identity);
                }
            }
        }else if(current_scene_name=="Level 3"){
            int ii = 0;
            for(int i = 0; i < survivor_count; i++){
                Instantiate(survivor_instance, survivor_spawn_points[ii].position, Quaternion.identity);
                ii++;
            }
            if(PlayerPrefs.HasKey("Level_2_to_3_Survivors")){
                var level_2_1_survivors = PlayerPrefs.GetInt("Level_2_to_3_Survivors");
                print(level_2_1_survivors+" poeple in level 3");
                for(int i = 0; i < level_2_1_survivors; i++){
                    Instantiate(survivor_instance, GameObject.Find("Player").transform.position+Vector3.right * Random.Range(-3,3), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
