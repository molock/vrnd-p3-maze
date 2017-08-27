using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Material skyboxDay;
	public Material skyboxNight;
	//private Skybox currentSkybox;
	private const string TERM1_DAY_TEXT_FORMAT = "Coins: {0} / {1} \n DayTimeLeft: {2}";
	private const string TERM1_NIGHT_TEXT_FORMAT = "Coins: {0} / {1} \n NightTimeLeft: {2}";
	private const string FINAL_TEXT_FORMAT = "Key: {0} / {1}";
	private const string DAY_HINT = "Hint: Find enough coins before DARK!";
	private const string NIGHT_HINT = "Hint: You are in a new MAZE. \n Go get enough coins!";
	private const string FINAL_HINT = "Hint: Just go get the key!";
	private const string TO_WIN_HINT = "You got the Key! \n Go Open the Door!";
	private const string HAS_WON_HINT = "You Win! \n Click the title to restart!";
	private const float DAY_TIME = 120f;
  	private const int COIN_MAX_COUNT = 5;
	private const int KEY_MAX_COUNT = 1;
	private int _coinCount;

  	private int _keyCount;

  	private Text textField;
	private Text hintText;

	private Camera cam;


	private enum GameSection {Term1, Final, GotKey, Win};
	private GameSection currentSection;

	private GameObject _dayMaze;

	private GameObject _nightMaze;
	private GameObject _finalMaze;

	//private float _finalTimeLeft;

	private float _dayTimeLeft;

	//private GameObject _sunLight;

	//private GameObject _moonLight;

	private bool _isDayTime;

	private Vector3 _originPosition;
	private bool _hasWon;

	void Awake()
	{
		RenderSettings.skybox = skyboxDay;
	}

	// Use this for initialization
	void Start () 
	{
		InitScene();
	}

	void InitScene()
	{
		_hasWon = false;
		currentSection = GameSection.Term1;
		// currentSection = GameSection.Final;
		cam = Camera.main;
		transform.SetParent(cam.GetComponent<Transform>(), true);
		
		_coinCount = 0;
		_keyCount = 0;

		textField = transform.Find("ScoreText").GetComponent<Text>();
		hintText = transform.Find("HintText").GetComponent<Text>();

		//_finalTimeLeft = 120f;
		//currentSkybox = cam.GetComponent<Skybox>();
		//currentSkybox.material = skyboxDay;

		//_sunLight = GameObject.Find("SunLight");
		// _moonLight = GameObject.Find("MoonLight");

		// _sunLight.SetActive(true);
		// _moonLight.SetActive(false);

		_isDayTime = true;

		_originPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);

		InitMaze();
	}

	public void ResetScene()
	{
		_coinCount = 0;
		_keyCount = 0;

		currentSection = GameSection.Term1;

		_isDayTime = true;

		// _sunLight.SetActive(true);
		// _moonLight.SetActive(false);

		_dayTimeLeft = DAY_TIME;

		_dayMaze.SetActive(true);
		_nightMaze.SetActive(false);
		_finalMaze.SetActive(false);

		cam.transform.position = new Vector3(_originPosition.x, _originPosition.y, _originPosition.z);
	}

	void InitMaze()
	{
		_dayMaze = GameObject.Find("DayMaze");
		_nightMaze = GameObject.Find("NightMaze");
		_finalMaze = GameObject.Find("FinalMaze");

		_dayTimeLeft = DAY_TIME;

		_dayMaze.SetActive(true);
		_nightMaze.SetActive(false);
		_finalMaze.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		switch(currentSection)
		{
			case GameSection.Term1:
				if(_isDayTime)
				{
					textField.text = string.Format(TERM1_DAY_TEXT_FORMAT, _coinCount, COIN_MAX_COUNT, Math.Round(_dayTimeLeft, 0));
					hintText.text = DAY_HINT;
				}
				else
				{
					textField.text = string.Format(TERM1_NIGHT_TEXT_FORMAT, _coinCount, COIN_MAX_COUNT, Math.Round(_dayTimeLeft, 0));
					hintText.text = NIGHT_HINT;
				}


				if(_dayTimeLeft > 0)
				{
					_dayTimeLeft = _dayTimeLeft - Time.deltaTime;
				}
				else
				{
					changeDayNight();
				}

				// if coins are enough switch to final
				if(_coinCount >= COIN_MAX_COUNT)
				{
					// _sunLight.SetActive(true);
					// _moonLight.SetActive(false);

					_dayMaze.SetActive(false);
					_nightMaze.SetActive(false);
					_finalMaze.SetActive(true);

					cam.transform.position = new Vector3(0.7f, 3.4f, 32.7f);

					currentSection = GameSection.Final;
					
				}
				break;

			case GameSection.Final:
				textField.text = string.Format(FINAL_TEXT_FORMAT, _keyCount, KEY_MAX_COUNT);
				hintText.text = FINAL_HINT;
				// if(_finalTimeLeft > 0)
				// {
				// 	_finalTimeLeft = _finalTimeLeft - Time.deltaTime;
				// }
				// else
				// {
				// 	resetFinalMaze();
				// }

				if(_keyCount >= 1)
				{
					currentSection = GameSection.GotKey;
				}

				break;

			case GameSection.GotKey:
				hintText.text = TO_WIN_HINT;
				if (_hasWon)
				{
					currentSection = GameSection.Win;
				}
				break;

			case GameSection.Win:
				hintText.text = HAS_WON_HINT;
				break;
		}    

	}

	private void changeDayNight()
	{
		if(_isDayTime)
		{
			_isDayTime = false;
			// _sunLight.SetActive(false);
			// _moonLight.SetActive(true);
			_dayMaze.SetActive(false);
			_nightMaze.SetActive(true);

			//currentSkybox.material = skyboxNight;
			RenderSettings.skybox = skyboxNight;
			DynamicGI.UpdateEnvironment();

			textField.color = Color.white;
			hintText.color = Color.white;
		}
		else
		{
			_isDayTime = true;
			// _sunLight.SetActive(true);
			// _moonLight.SetActive(false);
			_dayMaze.SetActive(true);
			_nightMaze.SetActive(false);

			//currentSkybox.material = skyboxDay;
			RenderSettings.skybox = skyboxDay;
			DynamicGI.UpdateEnvironment();

			textField.color = Color.black;
			hintText.color = Color.black;
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

	public void OpenTheDoor()
	{
		_hasWon = true;
	}

	// private void resetFinalMaze()
	// {
	// 	// 1、reset camera position
	// 	// 2、rebuild a key that is randomly chosen from 3 specific position
	// }
}
