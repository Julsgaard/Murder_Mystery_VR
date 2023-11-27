using UnityEngine;
using System;


public class GameTimer : MonoBehaviour
{

	[SerializeField] private GameManager gameManager;
	private enum WatchType
	{
		Digtial,
		Analogue
	}
    
    [SerializeField] 
	private WatchType typeOfWatch;
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

	private bool _startCountdown = false;
	private DateTime _startTime;
	private DateTime _gameBeginTime = System.DateTime.Today.AddHours(6);
    // Use this for initialization
    void Start () {
		//StartTimer();
		
		// sets up the gameobject to be a digtial or analogue clock
		switch(typeOfWatch)
		{
			case WatchType.Digtial: 
				digiClock.SetActive(true); 
				break;
			case WatchType.Analogue: 
				digiClock.SetActive(false); 
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//gets the realTime
		DateTime realTime = System.DateTime.Now;
		// if a startTime has been set, start updating the clock
		if(_startCountdown)
		{
			// the difference between realtime and the time you started the clock
			TimeSpan duration = realTime.Subtract(_startTime);
			// updates the gametimer
			DateTime gameTime = _gameBeginTime.Add(duration);
			// updates the clock
            UpdateClock(typeOfWatch, gameTime);
			
			//ends the game if the duration is over
			if(duration.TotalMinutes > durationOfGameInMin)
			{
				TimerFinished();
			}
		}
	}
	//sets the _startTime, to let the clock know it should begin counting (Should maybe subscripe to event?)
	public void StartTimer()
	{
		_startTime = System.DateTime.Now;
		
		_startCountdown = true;
	}
	//lets the game know when the time has run out (Should maybe fire an event? )
	private void TimerFinished()
	{
		// Sets the endText
		string endText = "Time has run out and everyone has been arrested by the police";
		
		// Ends the game
		gameManager.EndScreen(endText);
		
		// Stops the clock
		_startCountdown = false;
	}
	// updates the duration of the game in case it should be done from within the game
	public void SetDurationOfGame(float durationInMin)
	{
		durationOfGameInMin = durationInMin;
	}
	// gets the duration of the game
	public float GetDurationOfGameInMin()
	{
		return durationOfGameInMin;
	}

	private void UpdateClock(WatchType currentType, DateTime gameTime)
	{
		// if the clock is analogue rotate the hands
		if(currentType == WatchType.Analogue)
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
		else if (currentType == WatchType.Digtial)
		{
			// gets the timespan between the "end of the game" and the current gametime
			TimeSpan timeLeft = _gameBeginTime.AddMinutes(durationOfGameInMin).Subtract(gameTime);
			// displays said timespan
			digiDisplay.SetText($"{timeLeft.Minutes}:{timeLeft.Seconds}");
		}
    }
}
