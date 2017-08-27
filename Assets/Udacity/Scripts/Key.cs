using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    //Create a reference to the KeyPoofPrefab and Door
    public GameObject doorPrefab;
    public GameObject keyPoof;

    public Transform startMarker;
    public Transform endMarker;
    public float speed;

    private float _xRotation = -90;
    private Door doorScript;
    private GameManager _gameManager;

    private float startTime;
    private float journeyLength;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isMovingBack;

    void Start()
    {
        if(doorPrefab != null)
        {
            doorScript = doorPrefab.GetComponent<Door>();
        }

        _gameManager = GameObject.Find("PlayerUI").GetComponent<GameManager>();

        startTime = Time.time;
        // startPosition = new Vector3(0.47f, 6f, 25.25f);
        // endPosition = new Vector3(0.47f, 4f, 25.25f);

        startPosition = startMarker.position;
        endPosition = endMarker.position;

        journeyLength = Vector3.Distance(startPosition, endPosition);

        isMovingBack = false;
    }

	void Update()
	{
		//Not required, but for fun why not try adding a Key Floating Animation here :)
        KeyFloatAnim();
	}

    void KeyFloatAnim()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

        if(distCovered > journeyLength)
        {
            startTime = Time.time;

            if(!isMovingBack)
            {            
                startPosition = endMarker.position;
                endPosition = startMarker.position;
                isMovingBack = true;
            }
            else
            {
                startPosition = startMarker.position;
                endPosition = endMarker.position;
                isMovingBack = false;
            }
        }
    }

	public void OnKeyClicked()
	{
        // Instatiate the KeyPoof Prefab where this key is located
        // Make sure the poof animates vertically
        Instantiate(keyPoof, transform.position, Quaternion.Euler(_xRotation,0,0));
        
        // Call the Unlock() method on the Door
        doorScript.Unlock();

        // Set the Key Collected Variable to true
        _gameManager.getKey();
        
        // Destroy the key. Check the Unity documentation on how to use Destroy
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

}
