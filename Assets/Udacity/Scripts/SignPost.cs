using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignPost : MonoBehaviour
{
    public GameObject player;


    public void ResetScene()
    {

        // Reset the scene when the user clicks the sign post
	player.GetComponent<GameManager>().ResetScene();
    }
}