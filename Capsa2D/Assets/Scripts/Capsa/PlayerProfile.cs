using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour {

    public Avatar avatar;
    public Text playerNameText;
    public Text playerChipsText;

    public void Init()
    {
        if (playerNameText != null)
        {
            playerNameText.text = "Player";
        }
        if (playerChipsText != null)
        {
            SetPlayerChips(PlayerAtr.instance.TotalChips);
        }
        avatar.Init(PlayerAtr.instance.AvatarId);
    }

    public void Init(string playerName, int chips, int level, int vipLevel)
    {
        LoadOtherPlayer();
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
        if (playerChipsText != null)
        {
            SetPlayerChips(chips);
        }


    }

    public void LoadOtherPlayer(/*PlayerData data*/)
    {
        List<string> randNameList1 = new List<string>();
        List<string> randNameList2 = new List<string>();
        randNameList1.Add("Bolo");
        randNameList1.Add("Bala");
        randNameList1.Add("Bili");
        randNameList1.Add("Bulu");
        randNameList1.Add("Kepala");
        randNameList1.Add("Tangan");
        randNameList1.Add("Kaki");

        randNameList2.Add("Bau");
        randNameList2.Add("Harum");
        randNameList2.Add("Sakti");
        randNameList2.Add("Sakit");
        randNameList2.Add("Bolo");
        randNameList2.Add("Kakek");
        randNameList2.Add("Nenek");

        playerNameText.text = randNameList1[Random.Range(0, randNameList1.Count)] + " " + randNameList2[Random.Range(0, randNameList2.Count)];
        playerChipsText.text = "12389012";
    }

    public void SetPlayerChips(int value)
    {
        playerChipsText.text = value.ToString();
    }

    public void SetAvatarImage(string avatarId)
    {
        avatar.Init(avatarId);
    }

}
