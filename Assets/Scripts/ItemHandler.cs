using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public int index;
    private int isSelected;
    public TextMeshProUGUI SelectText;
    
    // Update is called once per frame
    void Update()
    {
        isSelected = PlayerPrefs.GetInt("Skin", 1);
        if (index == isSelected)
        {
            SelectText.text = "Selected";
        }
        else
        {
            SelectText.text = "Select";
        }
    }

    public void SetSkin()
    {
        PlayerPrefs.SetInt("Skin", index);
    }
}
