using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System31 : MonoBehaviour {

  public AssetName nextScene = null;
  public AssetName talk_3_1 = null;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(InitialPart());
  }

  // Update is called once per frame
  void Update() {

  }

  void FinishPart() {
    StartCoroutine(FinishPartAsync());
  }

  IEnumerator InitialPart() {
    yield return new WaitForSeconds(0.5f);

    UIManager.Instance.talkTachie.FinishedEvent += FinishPart;
    UIManager.Instance.talkTachie.PlayScript(talk_3_1.assetName);
  }

  IEnumerator FinishPartAsync() {

    UIManager.Instance.talkTachie.FinishedEvent -= FinishPart;

    yield return new WaitForSeconds(1.0f);

    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}