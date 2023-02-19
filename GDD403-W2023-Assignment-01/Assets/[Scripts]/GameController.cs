using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<Transform> twoByFourLayout;
    public List<Transform> fourByFourLayout;
    public List<Transform> sixBySixLayout;
    public StandardDeck deck;

    // Start is called before the first frame update
    void Start()
    {
        deck = new StandardDeck(); // example of composition
        uiController = FindObjectOfType<UIController>();
    }


    private UIController uiController;
    private Card sel1, sel2;
    public bool IsDelayed;
    private int tries = 0;
    public int max_tries = 3;
    public int matches = 0;
    public int max_matches = 0;

    public void SelectCard(Card c)
    {
        if (sel1 == null)
        {
            sel1 = c;
        }
        else
        {
            sel2 = c;
            CheckMatch();
        }

    }

    public void CheckMatch()
    {
        if (sel1.gameObject.name == sel2.gameObject.name)
        {
            sel1.isMatched = true;
            sel2.isMatched = true;
            sel1 = null;
            sel2 = null;
            matches++;
            if(matches >= max_matches)
            {
                IsDelayed = true;
                uiController.SetWinLoss(true);
            }
            uiController.UpdateMatches();
        }
        else
        {
            IsDelayed = true;
            tries++;
            if (tries >= max_tries)
            {
                uiController.SetWinLoss(false);
                IsDelayed = true;
                tries = 0;
            }
            else
            {
                Invoke("Reflip", 3);
            }
        }
    }
    public void Reflip()
    {
        sel1.Flip();
        sel2.Flip();
        sel1 = null;
        sel2 = null;
        IsDelayed = false;
    }
}
