using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsaCard : Card {

    private int position;

    public int Position
    {
        set { position = value; }
        get { return position; }
    }
}
