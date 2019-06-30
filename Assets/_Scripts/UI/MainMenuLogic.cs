using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuLogic : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void NaviToFirstScene() {
    SceneManager.LoadScene("Scene0", LoadSceneMode.Single);
  }
}