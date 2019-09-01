using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine.SceneManagement;

public class System14 : MonoBehaviour {

  public AssetName nextScene = null;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(InitialPart());
  }

  // Update is called once per frame
  void Update() {

  }

  
  IEnumerator InitialPart() {
    yield return new WaitForSeconds(8.0f);

    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}