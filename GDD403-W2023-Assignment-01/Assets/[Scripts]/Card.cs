using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    //public Suit suit = Suit.CLUBS;

    [Header( "Card Properties" )]
    public string rankName;
    public string suit;
    public int value;
    public bool isFaceUp;

    public bool startFacing;

    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (startFacing != isFaceUp)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.position = new Vector3(transform.position.x, 7.5f, transform.position.z);
        transform.localRotation = Quaternion.Euler(0, 0, Convert.ToInt32(isFaceUp) * 180);
        isFaceUp = !isFaceUp;
    }

    string toString()
    {
        return rankName + " of " + suit ;
    }

    private void Initialize()
    {
        var split = name.Split('_');

        suit = split[0];

        String[] numberWords = { "Zero", "Ace", "Deuce", "Three", "Four", "Five", "Six",
                                "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};

        switch (split[1])
        {
            case "A":
                value = 1;
                break;
            case "J":
                value = 11;
                break;
            case "Q":
                value = 12;
                break;
            case "K":
                value = 13;
                break;
            default:
                rankName = split[1];
                Int32.TryParse(rankName, out value);
                break;
        }

        rankName = numberWords[value];

        isFaceUp = true;
        startFacing = isFaceUp;
    }
}

