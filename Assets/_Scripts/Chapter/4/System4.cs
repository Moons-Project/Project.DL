﻿using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System4 : MonoBehaviour {

  public AssetName nextScene = null;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(FinishPart());
  }

  // Update is called once per frame
  void Update() {

  }

  IEnumerator FinishPart() {

    yield return new WaitForSeconds(7.0f);

    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}