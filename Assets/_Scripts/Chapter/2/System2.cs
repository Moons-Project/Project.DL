using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System2 : MonoBehaviour {

  public AssetName nextScene = null;
  public AssetName talk_3_1 = null;
  public AssetName talk_3_2 = null;
  public Animator roadAnimator = null;
  public AssetName roadAnimation1 = null;
  

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
    roadAnimator.Play(roadAnimation1.assetName);
    yield return new WaitForSeconds(6.0f);




    UIManager.Instance.talkTachie.FinishedEvent += FinishPart;
    UIManager.Instance.talkTachie.PlayScript(talk_3_2.assetName);
  }

    void FinishPart() {
    StartCoroutine(FinishPartAsync());
  }


  IEnumerator FinishPartAsync() {
    UIManager.Instance.talkTachie.FinishedEvent -= FinishPart;

    yield return new WaitForSeconds(1.0f);

    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}