using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
	private int rescued_survivors;
	private int score;

	[SerializeField]
    private Text survivor_data;

    [SerializeField]
    private Text score_data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Awake(){
        if(PlayerPrefs.HasKey("survivors")){
            rescued_survivors = PlayerPrefs.GetInt("survivors");
        }else{
            rescued_survivors = 0;
        }
        if(PlayerPrefs.HasKey("score")){
            score = PlayerPrefs.GetInt("score");
        }else{
            score = 0;
        }

        survivor_data.text = "Total Rescue: "+rescued_survivors.ToString();
        score_data.text = "Final Score: "+score.ToString();
    }
}
