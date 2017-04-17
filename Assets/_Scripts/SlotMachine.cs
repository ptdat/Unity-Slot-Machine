﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	private int playerMoney = 1000;
	private int winnings = 0;
	private int jackpot = 5000;
	private float turn = 0.0f;
	private int playerBet = 10; // default player bet value
	private float winNumber = 0.0f;
	private float lossNumber = 0.0f;
	private string[] spinResult;
	private string fruits = "";
	private float winRatio = 0.0f;
	private float lossRatio = 0.0f;
	private int grapes = 0;
	private int bananas = 0;
	private int oranges = 0;
	private int cherries = 0;
	private int bars = 0;
	private int bells = 0;
	private int sevens = 0;
	private int blanks = 0;

    void OnGUI()
    {
        // Display the amount of player bet
        GameObject.Find("BetAmount").GetComponent<Text>().text = this.playerBet.ToString();
    }


    /* Utility function to show Player Stats */
    private void showPlayerStats()
	{
		winRatio = winNumber / turn;
		lossRatio = lossNumber / turn;
		string stats = "";
		stats += ("Jackpot: " + jackpot + "\n");
		stats += ("Player Money: " + playerMoney + "\n");
		stats += ("Turn: " + turn + "\n");
		stats += ("Wins: " + winNumber + "\n");
		stats += ("Losses: " + lossNumber + "\n");
		stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
		stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
		Debug.Log(stats);
	}

	/* Utility function to reset all fruit tallies*/
	private void resetFruitTally()
	{
		grapes = 0;
		bananas = 0;
		oranges = 0;
		cherries = 0;
		bars = 0;
		bells = 0;
		sevens = 0;
		blanks = 0;
	}

	/* Utility function to reset the player stats */
	public void resetAll()
	{
		playerMoney = 1000;
		winnings = 0;
		jackpot = 5000;
		turn = 0;
		playerBet = 10; // default
		winNumber = 0;
		lossNumber = 0;
		winRatio = 0.0f;
	}

	/* Check to see if the player won the jackpot */
	private void checkJackPot()
	{
		/* compare two random values */
		var jackPotTry = Random.Range (1, 51);
		var jackPotWin = Random.Range (1, 51);
		if (jackPotTry == jackPotWin)
		{
			Debug.Log("You Won the $" + jackpot + " Jackpot!!");
			playerMoney += jackpot;
			jackpot = 1000;

		}
	}

	/* Utility function to show a win message and increase player money */
	private void showWinMessage()
	{
		playerMoney += winnings;
		Debug.Log("You Won: $" + winnings);
		resetFruitTally();
		checkJackPot();
	}

	/* Utility function to show a loss message and reduce player money */
	private void showLossMessage()
	{
		playerMoney -= playerBet;
		Debug.Log("You Lost!");
		resetFruitTally();
	}

	/* Utility function to check if a value falls within a range of bounds */
	private bool checkRange(int value, int lowerBounds, int upperBounds)
	{
		return (value >= lowerBounds && value <= upperBounds) ? true : false;

	}

	/* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
	private string[] Reels()
	{
		string[] betLine = { " ", " ", " " };
		int[] outCome = { 0, 0, 0 };

		for (var spin = 0; spin < 3; spin++)
		{
			outCome[spin] = Random.Range(1,65);

            if (checkRange(outCome[spin], 1, 27))
            {  // 41.5% probability
                betLine[spin] = "blank";
                blanks++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("blank", typeof(Sprite)) as Sprite;
            }
            else if (checkRange(outCome[spin], 28, 37))
            { // 15.4% probability
                betLine[spin] = "Grapes";
                grapes++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("grapes", typeof(Sprite)) as Sprite;
            }
            else if (checkRange(outCome[spin], 38, 46))
            { // 13.8% probability
                betLine[spin] = "Banana";
                bananas++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("banana", typeof(Sprite)) as Sprite;
            }
            else if (checkRange(outCome[spin], 47, 54))
            { // 12.3% probability
                betLine[spin] = "Orange";
                oranges++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("orange", typeof(Sprite)) as Sprite;
            }
            else if (checkRange(outCome[spin], 55, 59))
            { //  7.7% probability
                betLine[spin] = "Cherry";
                cherries++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("cherry", typeof(Sprite)) as Sprite;
            }
            else if (checkRange(outCome[spin], 60, 62))
            { //  4.6% probability
                betLine[spin] = "Bar";
                bars++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("bar", typeof(Sprite)) as Sprite;
            }
            else if (checkRange(outCome[spin], 63, 64))
            { //  3.1% probability
                betLine[spin] = "Bell";
                bells++;
                bars++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("bell", typeof(Sprite)) as Sprite;

            }
            else if (checkRange(outCome[spin], 65, 65))
            { //  1.5% probability
                betLine[spin] = "Seven";
                sevens++;
                bars++;
                GameObject.Find(spin == 0 ? "Scroll1" : (spin == 1 ? "Scroll2" : "Scroll3")).GetComponent<Image>().sprite = Resources.Load("seven", typeof(Sprite)) as Sprite;

            }

        }
		return betLine;
	}

	/* This function calculates the player's winnings, if any */
	private void determineWinnings()
	{
		if (blanks == 0)
		{
			if (grapes == 3)
			{
				winnings = playerBet * 10;
			}
			else if (bananas == 3)
			{
				winnings = playerBet * 20;
			}
			else if (oranges == 3)
			{
				winnings = playerBet * 30;
			}
			else if (cherries == 3)
			{
				winnings = playerBet * 40;
			}
			else if (bars == 3)
			{
				winnings = playerBet * 50;
			}
			else if (bells == 3)
			{
				winnings = playerBet * 75;
			}
			else if (sevens == 3)
			{
				winnings = playerBet * 100;
			}
			else if (grapes == 2)
			{
				winnings = playerBet * 2;
			}
			else if (bananas == 2)
			{
				winnings = playerBet * 2;
			}
			else if (oranges == 2)
			{
				winnings = playerBet * 3;
			}
			else if (cherries == 2)
			{
				winnings = playerBet * 4;
			}
			else if (bars == 2)
			{
				winnings = playerBet * 5;
			}
			else if (bells == 2)
			{
				winnings = playerBet * 10;
			}
			else if (sevens == 2)
			{
				winnings = playerBet * 20;
			}
			else if (sevens == 1)
			{
				winnings = playerBet * 5;
			}
			else
			{
				winnings = playerBet * 1;
			}
			winNumber++;
			showWinMessage();
		}
		else
		{
			lossNumber++;
			showLossMessage();
		}

	}

	public void OnSpinButtonClick()
	{

		if (playerMoney == 0)
		{
			/*
			if (Debug.Log("You ran out of Money! \nDo you want to play again?","Out of Money!",MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				resetAll();
				showPlayerStats();
			}
			*/
		}
		else if (playerBet > playerMoney)
		{
			Debug.Log("You don't have enough Money to place that bet.");
		}
		else if (playerBet < 0)
		{
			Debug.Log("All bets must be a positive $ amount.");
		}
		else if (playerBet <= playerMoney)
		{
			spinResult = Reels();
			fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
			Debug.Log(fruits);
			determineWinnings();
			turn++;
			showPlayerStats();
		}
		else
		{
			Debug.Log("Please enter a valid bet amount");
		}
	}

    public void setPlayerBet(int betAmount)
    {
        this.playerBet = betAmount;
    }
}
