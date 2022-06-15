using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {

    const int BLACK_CHIP = 100;
    const int BLUE_CHIP = 50;
    const int GREEN_CHIP = 25;
    const int RED_CHIP = 5;
    const int WHITE_CHIP = 1;

    public const string SPADE = "SPADE";
    public const string CLUB = "CLUB";
    public const string HEART = "HEART";
    public const string DIAMOND = "DIAMOND";

    //---- CARD LIST ----
    #region cardlist
    public const string ACE_SPADE = "ACE_SPADE";
    public const string TWO_SPADE = "2_SPADE";
    public const string THREE_SPADE = "3_SPADE";
    public const string FOUR_SPADE = "4_SPADE";
    public const string FIVE_SPADE = "5_SPADE";
    public const string SIX_SPADE = "6_SPADE";
    public const string SEVEN_SPADE = "7_SPADE";
    public const string EIGHT_SPADE = "8_SPADE";
    public const string NINE_SPADE = "9_SPADE";
    public const string TEN_SPADE = "10_SPADE";
    public const string JACK_SPADE = "J_SPADE";
    public const string QUEEN_SPADE = "Q_SPADE";
    public const string KING_SPADE = "K_SPADE";

    public const string ACE_CLUB = "ACE_CLUB";
    public const string TWO_CLUB = "2_CLUB";
    public const string THREE_CLUB = "3_CLUB";
    public const string FOUR_CLUB = "4_CLUB";
    public const string FIVE_CLUB = "5_CLUB";
    public const string SIX_CLUB = "6_CLUB";
    public const string SEVEN_CLUB = "7_CLUB";
    public const string EIGHT_CLUB = "8_CLUB";
    public const string NINE_CLUB = "9_CLUB";
    public const string TEN_CLUB = "10_CLUB";
    public const string JACK_CLUB = "J_CLUB";
    public const string QUEEN_CLUB = "Q_CLUB";
    public const string KING_CLUB = "K_CLUB";

    public const string ACE_HEART = "ACE_HEART";
    public const string TWO_HEART = "2_HEART";
    public const string THREE_HEART = "3_HEART";
    public const string FOUR_HEART = "4_HEART";
    public const string FIVE_HEART = "5_HEART";
    public const string SIX_HEART = "6_HEART";
    public const string SEVEN_HEART = "7_HEART";
    public const string EIGHT_HEART = "8_HEART";
    public const string NINE_HEART = "9_HEART";
    public const string TEN_HEART = "10_HEART";
    public const string JACK_HEART = "J_HEART";
    public const string QUEEN_HEART = "Q_HEART";
    public const string KING_HEART = "K_HEART";

    public const string ACE_DIAMOND = "ACE_DIAMOND";
    public const string TWO_DIAMOND = "2_DIAMOND";
    public const string THREE_DIAMOND = "3_DIAMOND";
    public const string FOUR_DIAMOND = "4_DIAMOND";
    public const string FIVE_DIAMOND = "5_DIAMOND";
    public const string SIX_DIAMOND = "6_DIAMOND";
    public const string SEVEN_DIAMOND = "7_DIAMODN";
    public const string EIGHT_DIAMOND = "8_DIAMOND";
    public const string NINE_DIAMOND = "9_DIAMOND";
    public const string TEN_DIAMOND = "10_DIAMOND";
    public const string JACK_DIAMOND = "J_DIAMOND";
    public const string QUEEN_DIAMOND = "Q_DIAMOND";
    public const string KING_DIAMOND = "K_DIAMOND";

    public const string RED_JOKER = "RED_JOKER";
    public const string BLACK_JOKER = "BLACK_JOKER";
    #endregion
    //---- END CARD LIST ----

    public static string GetShortenedCurrency(int currency)
    {
        string result = "";
        string currencyText = currency.ToString();
        if (currencyText.Length > 9)
        {
            currencyText = currencyText.Remove(currencyText.Length - 8);
            if (currencyText.Length == 2)
            {
                result = currencyText[0] + ".";
                for (int i = 1; i < currencyText.Length; i++)
                {
                    result += currencyText[i];
                }
            }
            else
            {
                result = currencyText.Substring(0, currencyText.Length - 1);
            }
            result += "B";
        }
        else if (currencyText.Length > 6)
        {
            currencyText = currencyText.Remove(currencyText.Length - 5);
            Debug.Log("temp : " + currencyText);
            if (currencyText.Length == 2)
            {
                result = currencyText[0] + ".";
                for (int i = 1; i < currencyText.Length; i++)
                {
                    result += currencyText[i];
                }
            }
            else
            {
                result = currencyText.Substring(0, currencyText.Length - 1);
            }
            result += "M";
        }
        else if (currencyText.Length > 3)
        {
            currencyText = currencyText.Remove(currencyText.Length - 2);
            Debug.Log("temp : " + currencyText);
            if (currencyText.Length == 2)
            {
                result = currencyText[0] + ".";
                for (int i = 1; i < currencyText.Length; i++)
                {
                    result += currencyText[i];
                }
            }
            else
            {
                result = currencyText.Substring(0, currencyText.Length - 1);
            }
            result += "K";
        }
        return result;
    }

    public static string GetShortenedCurrency(string currency)
    {
        return "";
    }

    public static Dictionary<int, int> ConvertMoneyToChip(int value)
    {
        Debug.Log("value : " + value);
        Dictionary<int, int> chipDict = new Dictionary<int, int>();
        int blackCount = 0;
        int blueCount = 0;
        int greenCount = 0;
        int redCount = 0;
        int whiteCount = 0;

        blackCount = Mathf.FloorToInt(value / BLACK_CHIP);
        value = value % BLACK_CHIP;
        blueCount = Mathf.FloorToInt(value / BLUE_CHIP);
        value = value % BLUE_CHIP;
        greenCount = Mathf.FloorToInt(value / GREEN_CHIP);
        value = value % GREEN_CHIP;
        redCount = Mathf.FloorToInt(value / RED_CHIP);
        value = value % RED_CHIP;
        whiteCount = Mathf.FloorToInt(value / WHITE_CHIP);

        Debug.Log("black : " + blackCount);
        Debug.Log("blue : " + blueCount);
        Debug.Log("green : " + greenCount);
        Debug.Log("red : " + redCount);
        Debug.Log("white : " + whiteCount);
        Debug.Log("value : " + value);

        chipDict.Add(BLACK_CHIP, blackCount);
        chipDict.Add(BLUE_CHIP, blueCount);
        chipDict.Add(GREEN_CHIP, greenCount);
        chipDict.Add(RED_CHIP, redCount);
        chipDict.Add(WHITE_CHIP, whiteCount);

        return chipDict;
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
