using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System21 : MonoBehaviour {

  public AssetName nextScene = null;
  public AssetName talk_3_1 = null;
  public GameObject tableGameObject = null;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(InitialPart());
  }

  // Update is called once per frame
  void Update() {

  }

  IEnumerator InitialPart() {
    yield return new WaitForSeconds(0.5f);

    UIManager.Instance.talkTachie.FinishedEvent += RoadMovePart;
    UIManager.Instance.talkTachie.PlayScript(talk_3_1.assetName);
  }

  void RoadMovePart() {
    StartCoroutine(RoadMovePartAsync());
  }

  IEnumerator RoadMovePartAsync() {
    UIManager.Instance.talkTachie.FinishedEvent -= RoadMovePart;
    yield return new WaitForSeconds(1.0f);

    tableGameObject.SetActive(true);
    yield return new WaitForSeconds(3.0f);
    StartCoroutine(FinishPart());
  }

  IEnumerator FinishPart() {

    tableGameObject.SetActive(false);

    yield return new WaitForSeconds(1.0f);

    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}