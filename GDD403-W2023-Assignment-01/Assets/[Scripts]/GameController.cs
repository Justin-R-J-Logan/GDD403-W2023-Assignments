using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Singleton instance of this controller. Multiples will glitch the system.
    public static GameController Instance { get; private set; }

    public List<Transform> twoByFourLayout;
    public List<Transform> fourByFourLayout;
    public List<Transform> sixBySixLayout;

    public StandardDeck deck;

    //For match attempts
    private Card selectedCard1, selectedCard2;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        deck = new StandardDeck(); // example of composition
    }

    //Delay variable to wait for timer
    public bool beingDelayed = false;

    //Fail logic and win logic counters
    private int currentFails = 0, maxFails = 3;
    public int currentMatches = 0, maxMatches = 0;

    public void SelectCard(Card c)
    {
        if (selectedCard1 == null)
        {
            selectedCard1 = c;
        }
        else
        {
            selectedCard2 = c;
            CheckMatch();
        }
    }

    public void CheckMatch()
    {
        if (selectedCard1.gameObject.name == selectedCard2.gameObject.name)
        {
            //deselect the cards, we found a match!
            selectedCard1 = null;
            selectedCard2 = null;

            //Add one to the score
            currentMatches++;

            //Play match stcore
            AudioController.Instance.PlaySound(CLIPS.MATCH);

            //Check for win
            if (currentMatches >= maxMatches)
            {
                beingDelayed = true;
                UIController.Instance.SetOutcome(true);
            }

            //Update score UI;
            UIController.Instance.UpdateScoreUI();
        }
        else
        {
            //Set being delayed, this will allow 3 seconds for the user to see the cards.
            beingDelayed = true;

            //Add one to current fails and check if we are over the maximum
            currentFails++;
            if (currentFails >= maxFails)
            {
                UIController.Instance.SetOutcome(false);
                beingDelayed = true;
                currentFails = 0;
            }
            else
            {
                //Tell the player they flipped wrong and wait 3 seconds to flip them back.
                AudioController.Instance.PlaySound(CLIPS.MFAIL);
                Invoke("waitReset", 3);
            }
        }
    }
    //This flips all cards back, resets all selected cards, and turns off the delay
    //This invoke was learned in a previous project. It is NOT external code.
    public void waitReset()
    {
        selectedCard1.Flip();
        selectedCard2.Flip();
        selectedCard1 = null;
        selectedCard2 = null;
        beingDelayed = false;
    }
}
