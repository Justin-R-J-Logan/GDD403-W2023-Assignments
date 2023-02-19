using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;
    public Difficulty difficulty;
    public GameController gameController;
    public Transform cardParent;
    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GetComponent<GameController>();
        difficultyDropdown = FindObjectOfType<TMP_Dropdown>();
        difficulty = Difficulty.EASY;
        cardParent = GameObject.Find("[CARDS]").transform;
        MyInit();
    }

    public void OnDifficulty_Changed()
    {
        difficulty = (Difficulty)difficultyDropdown.value;
    }

    public void OnStartButton_Pressed()
    {
        
        switch (difficulty)
        {
            case Difficulty.EASY: 
                Deal(gameController.twoByFourLayout, 8);
                gameController.max_matches = 4;
                gameController.matches = 0;
                break;
            case Difficulty.NORMAL:
                Deal(gameController.fourByFourLayout, 16);
                gameController.max_matches = 8;
                gameController.matches = 0;
                break;
            case Difficulty.HARD:
                Deal(gameController.sixBySixLayout, 36);
                gameController.max_matches = 18;
                gameController.matches = 0;
                break;
        }

        startButton.SetActive(false);
        UpdateMatches();
        gameController.audioController.PlaySound(CLIPS.SHUFFLE);
    }

    public void OnResetButton_Pressed()
    {
        gameController.deck.Clean();

        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }

        gameController.deck.Initialize();
        gameController.IsDelayed = false;

        startButton.SetActive(true);

        winloss.gameObject.SetActive(false);
    }

    private void Deal(List<Transform> layout, int cardNumber)
    {
        for (var i = 0; i < layout.Count; i++)
        {
            var randomIndex = Random.Range(0, layout.Count);
            if (i != randomIndex)
            {
                (layout[i], layout[randomIndex]) = (layout[randomIndex], layout[i]);
            }
        }

        for (var i = 0; i < cardNumber; i++)
        {
            if (i == 0 || i % 2 == 0)
            {
                var firstCard = gameController.deck.Pop();
                firstCard.SetActive(true);
                firstCard.GetComponent<Card>().Flip();
                var secondCard = Instantiate(firstCard);
                secondCard.transform.SetParent(cardParent);
                firstCard.transform.position = layout[i].position;
                secondCard.transform.position = layout[i + 1].position;
            }
        }
    }

    private TMP_Text winloss;
    private TMP_Text matches;

    private void MyInit()
    {
        winloss = GameObject.Find("WLL").GetComponent<TMP_Text>();
        winloss.gameObject.SetActive(false);
        matches = GameObject.Find("Progress").GetComponent<TMP_Text>();
        matches.gameObject.SetActive(false);
    }

    public void UpdateMatches()
    {
        matches.text = "" + gameController.matches + " of " + gameController.max_matches + " matches!";
        matches.gameObject.SetActive(true);
    }

    public void SetWinLoss(bool won) 
    {
        if(won)
        {
            winloss.text = "You've won!";
            gameController.audioController.PlaySound(CLIPS.WIN);
        }
        else
        {
            winloss.text = "You've lost!";
            gameController.audioController.PlaySound(CLIPS.LOSE);
        }
        winloss.gameObject.SetActive(true);
    }
}
