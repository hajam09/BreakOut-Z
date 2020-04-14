using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
	[SerializeField]
    private Image health_data;
    // Start is called before the first frame update
    
    [SerializeField]
    private Text ammo_data;

    [SerializeField]
    private Text score_data;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void View_Health_Statistics(float new_health_value){
        new_health_value = new_health_value/100f;//we divide this by 100 because the max health is 100 and the fill amount
        // value is between 0 and 1 . therfore divide by 100 would change the health ratio to suit the fill amount.
        health_data.fillAmount = new_health_value;
    }

    public void View_Ammo_Statistics(string new_ammo_value){
        ammo_data.text = new_ammo_value;
    }

    public void View_Score_Statistics(string new_score_value){
        score_data.text = new_score_value;
    }
}
