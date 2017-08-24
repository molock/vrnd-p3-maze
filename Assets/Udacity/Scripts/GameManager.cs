using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	private const string TERM1_DAY_TEXT_FORMAT = "Coins: {0} / {1} \n DayTimeLeft: {2}";
	private const string TERM1_NIGHT_TEXT_FORMAT = "Coins: {0} / {1} \n NightTimeLeft: {2}";
	private const string FINAL_TEXT_FORMAT = "Time Left: {0} \n Key: {1} / {2}";
	private const string TO_WIN_HINT = "You got the Key! \n Go Open the Day!";

	private const float DAY_TIME = 120f;
  	private const int COIN_MAX_COUNT = 5;
	private const int KEY_MAX_COUNT = 1;
	private int _coinCount;

  	private int _keyCount;

  	private Text textField;

	private Camera cam;

	private bool isFinal;

	private enum GameSection {Term1, Final, GotKey, Win};
	private int currentSection;

	private GameObject dayMaze;

	private GameObject nightMaze;

	private float _finalTimeLeft;

	private float _dayTimeLeft;

	private GameObject _sunLight;

	private GameObject _moonLight;

	private bool _isDayTime;

	private Vector3 _originPosition;

	// Use this for initialization
	void Start () {
		currentSection = (int)GameSection.Term1;
		_coinCount = 0;
		_keyCount = 0;
		isFinal = false;
		cam = Camera.main;
		transform.SetParent(cam.GetComponent<Transform>(), true);
		textField = GetComponentInChildren<Text>();

		dayMaze = GameObject.Find("Day_maze");

		_finalTimeLeft = 120f;

		_dayTimeLeft = DAY_TIME;

		_sunLight = GameObject.Find("SunLight");
		_moonLight = GameObject.Find("MoonLight");

		_sunLight.SetActive(true);
		_moonLight.SetActive(false);

		_isDayTime = true;

		_originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		switch(currentSection)
		{
			case (int)GameSection.Term1:
				if(_isDayTime)
				{
					textField.text = string.Format(TERM1_DAY_TEXT_FORMAT, _coinCount, COIN_MAX_COUNT, Math.Round(_dayTimeLeft, 0));
				}
				else
				{
					textField.text = string.Format(TERM1_NIGHT_TEXT_FORMAT, _coinCount, COIN_MAX_COUNT, Math.Round(_dayTimeLeft, 0));
				}


				if(_dayTimeLeft > 0)
				{
					_dayTimeLeft = _dayTimeLeft - Time.deltaTime;
				}
				else
				{
					changeDayNight();
				}


				if(_coinCount >= COIN_MAX_COUNT)
				{
					_sunLight.SetActive(true);
					_moonLight.SetActive(false);

					currentSection = (int)GameSection.Final;
					
					Debug.Log("the final maze emerged!");
				}
				break;

			case (int)GameSection.Final:
				textField.text = string.Format(FINAL_TEXT_FORMAT,  _finalTimeLeft, _keyCount, KEY_MAX_COUNT);

				if(_finalTimeLeft > 0)
				{
					_finalTimeLeft = _finalTimeLeft - Time.deltaTime;
				}
				else
				{
					resetFinalMaze();
				}

				

				if(_keyCount >= 1)
				{
					currentSection = (int)GameSection.GotKey;
					Debug.Log("the final maze collapsed!");
				}

				break;

			case (int)GameSection.GotKey:
				textField.text = TO_WIN_HINT;
				break;
		}    

	}

	private void changeDayNight()
	{
		if(_isDayTime)
		{
			_isDayTime = false;
			_sunLight.SetActive(false);
			_moonLight.SetActive(true);
			dayMaze.SetActive(false);
			//set nightMaze to active
		}
		else
		{
			_isDayTime = true;
			_sunLight.SetActive(true);
			_moonLight.SetActive(false);
			dayMaze.SetActive(true);
			//set nightMaze to inactive
		}

		_dayTimeLeft = DAY_TIME;
		cam.transform.position = new Vector3(_originPosition.x, _originPosition.y, _originPosition.z);

	}

	public void getOneCoin()
	{
		if(_coinCount < COIN_MAX_COUNT)
		{
			_coinCount = _coinCount + 1;
		}
	}

	public void getKey()
	{
		if(_keyCount < KEY_MAX_COUNT)
		{
			_keyCount = _keyCount + 1;
		}
	}

	private void resetFinalMaze()
	{
		// 1、把人传送回初始点
		// 2、rebuild a key that is randomly chosen from 3 specific position
	}
}
