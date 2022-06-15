using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour {

    private const float timeDuration = 5f;
    private float curTimeDuration = timeDuration;

    public PlayerCardController playerController;
    public List<Button> betListButton;
    private List<Text> betListText;
    public Text totalBetText;
    public Image timeBar;

    private bool isRunTimebar;

    public void Init(PlayerCardController playerController, List<int> betList)
    {
        SetTotalBet(0);
        isRunTimebar = false;
        betListText = new List<Text>();
        foreach (Button btn in betListButton)
        {
            betListText.Add(btn.GetComponentInChildren<Text>());
        }
        this.playerController = playerController;
        for (int i = 0; i < betListText.Count; i++)
        {
            if (i < betList.Count)
            {
                betListText[i].text = "$" + betList[i].ToString();
                betListText[i].gameObject.SetActive(true);
            }
            else
            {
                betListText[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnRunTimeBar()
    {
        curTimeDuration = timeDuration;
        isRunTimebar = true;
    }

    public void OnBetButtonClicked(int index)
    {
        OnRunTimeBar();
        playerController.OnPlaceBet(index);
    }

    public void SetTotalBet(int bet)
    {
        Debug.Log("bet : " + bet);
        totalBetText.text = "Total Bet : " + bet.ToString();
    }

    public void OnConfirmButtonClicked()
    {
        if (playerController.CurrentBet > 0)
        {
            playerController.isReady = true;
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isRunTimebar)
        {
            curTimeDuration -= Time.deltaTime;
           
            if (curTimeDuration < 0)
            {
                curTimeDuration = 0;
                isRunTimebar = false;
                playerController.isReady = true;
                
                gameObject.SetActive(false);
            }
            timeBar.fillAmount = curTimeDuration / timeDuration;
        }
    }
}
