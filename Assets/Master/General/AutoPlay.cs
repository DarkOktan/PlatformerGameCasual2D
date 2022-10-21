using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoPlay : MonoBehaviour
{
    float timer = 0;
    public Text info;
    
    // Use this for initialization
    void Start()
    {
        if (EnemyController.EnemyKilled == 3)
        {
            info.text = "Congratulations \n You Win!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Data.score = 0;
            EnemyController.EnemyKilled = 0;
            SceneManager.LoadScene("Gameplay 1");
        }
    }
}
