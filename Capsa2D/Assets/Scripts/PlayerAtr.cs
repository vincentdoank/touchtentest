using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtr : MonoBehaviour {
    
    private int totalChips;
    private int totalChipSpent;

    public static PlayerAtr instance;

    public int TotalChips
    {
        get
        {
            totalChips = PlayerPrefs.HasKey("chips") ? PlayerPrefs.GetInt("chips") : 10000;
            return totalChips;
        }

        set
        {
            totalChips = value;
            PlayerPrefs.SetInt("chips", totalChips);
            PlayerPrefs.Save();
        }
    }

    public string AvatarId { get; set; }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        instance = this;
    }
}
