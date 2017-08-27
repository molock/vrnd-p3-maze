using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    // Create a boolean value called "locked" that can be checked in OnDoorClicked() 
    // Create a boolean value called "opening" that can be checked in Update() 
    [Header("Sounds")]
    public AudioClip lockedSound;
    public AudioClip openSound;

    private const float MAX_HEIGHT = 7.5f;
    private bool _locked;
    private bool _opening;
    private AudioSource _audioSource;
    private GameObject _player;

    void Start()
    {
        _locked = true;
        _opening = false;
        _audioSource = gameObject.GetComponent<AudioSource>();
        _player = GameObject.Find("PlayerUI");
    }

    void Update() {
        // If the door is opening and it is not fully raised
        if(_opening && transform.position.y < MAX_HEIGHT)
        {
            // Animate the door raising up
            transform.Translate(new Vector3(0, Time.deltaTime * 10, 0));
        }
        // else
        // {
        //     _animator.SetBool("isOpening", false);
        // }
    }

    public void OnDoorClicked() {
        // If the door is clicked and unlocked
        if(!_locked)
        {
            // Set the "opening" boolean to true
            _opening = true;
            _player.GetComponent<GameManager>().OpenTheDoor();
            _audioSource.clip = openSound;
            _audioSource.Play();
        }
        // (optionally) Else
        else
        {
            // Play a sound to indicate the door is locked
            _audioSource.clip = lockedSound;
            _audioSource.Play();
        }
    }

    public void Unlock()
    {
        // You'll need to set "locked" to false here
        _locked = false;
    }
}
