using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography.X509Certificates;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using Unity.VisualScripting;

public class GameTimer : MonoBehaviour {

	private enum watchType
	{
		Digtial,
		Analogue
	}
    
    [SerializeField] 
	private watchType typeOfWatch;
	[SerializeField]
	private float durationOfGameInMin = 15.0f;

    [Header("Analogue Clock")]

    [SerializeField]
	private Transform hourHand;
	[SerializeField]
	private Transform minuteHand;
	[SerializeField]
	private Transform secondHand;

    [Header("Digital Clock")]
	[SerializeField]
	private GameObject digiClock;
	[SerializeField]
	private TMPro.TextMeshPro digiDisplay;

	private DateTime startTime;
	// sets the gametime to 6, this however means the game will prop break if you play it between 24:00 and 6:00... 
	private DateTime gameBeginTime = System.DateTime.Today.AddHours(6);
    // Use this for initialization
    void Start () {
		// startTimer();
		
		// sets up the gameobject to be a digtial or analogue clock
		switch(typeOfWatch)
		{
			case watchType.Digtial: 
				digiClock.SetActive(true); 
				break;
			case watchType.Analogue: 
				digiClock.SetActive(false); 
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//gets the realTime
		DateTime realTime = System.DateTime.Now;
		// if a startTime has been set, start updating the clock
		if(startTime != null)
		{
			// the difference between realtime and the time you started the clock
			TimeSpan duration = realTime.Subtract(startTime);
			// updates the gametimer
			DateTime gameTime = gameBeginTime.Add(duration);
			// updates the clock
            updateClock(typeOfWatch, gameTime);
			
			//ends the game if the duration is over
			if(duration.Minutes > durationOfGameInMin)
			{
				timerFinished();
			}
		}

	}
	//sets the starttimer, to let the clock know it should begin counting (Should maybe subscripe to event?)
	public void startTimer()
	{
		startTime = System.DateTime.Now;
	}
	//lets the game know when the time has run out (Should maybe fire an event? )
	public void timerFinished()
	{
		//put endGameLogic Here
	}
	// updates the duration of the game in case it should be done from within the game
	public void setDurationOfGame(float durationInMin)
	{
		durationOfGameInMin = durationInMin;
	}
	// gets the duration of the game
	public float getDurationOfGameInMin()
	{
		return durationOfGameInMin;
	}

	private void updateClock(watchType currentType, DateTime gameTime)
	{
		// if the clock is analogue rotate the hands
		if(currentType == watchType.Analogue)
		{
            //we want these hands to move smoothly, not jump every time the hour/minute changes. 
            float hour = gameTime.Hour + gameTime.Minute / 60f;
            float minute = gameTime.Minute + gameTime.Second / 60f;

            //The second hand is fine jumping, but if you want a smooth rotation on that just uncomment this code:  
            //second = second + millisecond / 1000f;
            hourHand.localRotation = Quaternion.Euler(0, 0, hour / 12 * 360);
            minuteHand.localRotation = Quaternion.Euler(0, 0, minute / 60 * 360);
            secondHand.localRotation = Quaternion.Euler(0, 0, (float)gameTime.Second / 60 * 360);

        }
		// if the clock is digtal update the digital display
		else if (currentType == watchType.Digtial)
		{
			// gets the timespan between the "end of the game" and the current gametime
			TimeSpan timeLeft = gameBeginTime.AddMinutes(durationOfGameInMin).Subtract(gameTime);
			// displays said timespan
			digiDisplay.SetText($"{timeLeft.Minutes}:{timeLeft.Seconds}");
		}
    }
}
