using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System1 : MonoBehaviour {
  public AssetName nextScene = null;
  public AssetName bgm = null;
  public AssetName talk_2_1 = null;
  public AssetName talk_2_2 = null;
  public Animator cameraAnimator = null;
  public AssetName cameraAnimation1 = null;
  public AssetName cameraAnimation2 = null;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(InitialPart());
  }

  // Update is called once per frame
  void Update() {

  }

  IEnumerator InitialPart() {
    yield return new WaitForSeconds(0.5f);

    AudioManager.Instance.PlayBGM(bgm.assetName);
    cameraAnimator.Play(cameraAnimation1.assetName);
    yield return new WaitForSeconds(2.5f);

    UIManager.Instance.talkTachie.FinishedEvent += CameraMovePart;
    UIManager.Instance.talkTachie.PlayScript(talk_2_1.assetName);
  }

  void CameraMovePart() {
    StartCoroutine(CameraMovePartAsync());
  }

  IEnumerator CameraMovePartAsync() {
    UIManager.Instance.talkTachie.FinishedEvent -= CameraMovePart;
    cameraAnimator.Play(cameraAnimation2.assetName);
    yield return new WaitForSeconds(4.0f);

    UIManager.Instance.talkTachie.FinishedEvent += FinishPart;
    UIManager.Instance.talkTachie.PlayScript(talk_2_2.assetName);
  }

  void FinishPart() {
    StartCoroutine(FinishPartAsync());
  }

  IEnumerator FinishPartAsync() {
    UIManager.Instance.talkTachie.FinishedEvent -= FinishPart;

    yield return new WaitForSeconds(0);

    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}