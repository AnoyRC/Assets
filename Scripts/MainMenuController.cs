using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI[] HighScores;
    public GameObject SkinMenu;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <= HighScores.Length; i++)
        {
            HighScores[i-1].text = PlayerPrefs.GetInt("Track"+i.ToString(),0).ToString()+"%";
        }

        SkinMenu.SetActive(toggle);
    }

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void hasToggled()
    {
        toggle = !toggle;
    }
}
