using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comic0 : MonoBehaviour {
  public AssetName BGMAudio;
  public AssetName PeopleSEAudio;
  public Color hintTextColor;



  private bool isSlideRightFinished = false;
  private TipsLayer tipsLayer => UIManager.Instance.tipsLayer;
  private CommicPlay commicPlay => this.gameObject.GetComponent<CommicPlay>();


  public void _PlayBGMAudio() {
    AudioManager.Instance.PlayBGM(BGMAudio.assetName);
  }

  public void _PlayPeopleSEAudio() {
    AudioManager.Instance.PlaySE(PeopleSEAudio.assetName);
  }

  public void _SlideRightFinished() {
    isSlideRightFinished = true;
  }

  public void _ShowSlideTips(float dealy) {
    tipsLayer.HintTextColor = hintTextColor;
    tipsLayer.ShowHintText("滑动以推进漫画", CommicPlay.kMaxPauseTime, dealy);
    StartCoroutine(WaitUnitilSlideRightFinished(() => {
      StartCoroutine(tipsLayer.SetActiveHintText(false, 0.0f));
    }));
  }

  public IEnumerator WaitUnitilSlideRightFinished(Action action) {
    yield return new WaitForSeconds(Time.deltaTime);
    isSlideRightFinished = false;
    yield return new WaitUntil(() => isSlideRightFinished);
    action.Invoke();
  }

  void Start() {
    _PlayBGMAudio();
  }
}