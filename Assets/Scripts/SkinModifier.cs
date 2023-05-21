using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinModifier : MonoBehaviour
{
    public string SkinName;
    public Image ImageUI;
    public Sprite SpriteUI;
    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ReneverseManager.SkinStats.ContainsKey(SkinName))
        { 
            ImageUI.sprite = SpriteUI;    
            for(int i = 0; i < Buttons.Length; i++)
            {
                if (i == 0 && Buttons[i] != null) Buttons[i].SetActive(false);
                if (i == 1 && Buttons[i] != null) Buttons[i].SetActive(true);
            } 
        }
    }
}
