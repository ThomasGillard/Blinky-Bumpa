using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    private static int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LevelComplete()
    {
        if(currentLevel < (SceneManager.sceneCountInBuildSettings - 1))
        {
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            print("You've won the game");
        }
    }
}
