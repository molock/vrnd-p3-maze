using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    //Create a reference to the KeyPoofPrefab and Door
    public GameObject doorPrefab;
    public GameObject keyPoof;
    private float _xRotation = -90;
    private Door doorScript;
    private GameManager _gameManager;

    void Start()
    {
        if(doorPrefab != null)
        {
            doorScript = doorPrefab.GetComponent<Door>();
        }

        _gameManager = GameObject.Find("PlayerUI").GetComponent<GameManager>();
        
    }

	void Update()
	{
		//Not required, but for fun why not try adding a Key Floating Animation here :)
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
