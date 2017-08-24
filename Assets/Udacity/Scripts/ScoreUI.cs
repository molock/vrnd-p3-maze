using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour {
  private const string DISPLAY_TEXT_FORMAT = "Coins: {0} \n Key: {1}";

  private const int COIN_MAX_COUNT = 5;

  private int _coinCount;

  private int _keyCount;

  private Text textField;

  public Camera cam;

  void Awake() {
    textField = GetComponent<Text>();
  }

  void Start() {
    if (cam == null) {
       cam = Camera.main;
    }

    if (cam != null) {
      // Tie this to the camera, and do not keep the local orientation.
      transform.SetParent(cam.GetComponent<Transform>(), true);
    }

    _coinCount = 0;
    _keyCount = 0;
  }

  void LateUpdate() {    
    textField.text = string.Format(DISPLAY_TEXT_FORMAT,
        _coinCount, _keyCount);
  }
}
