using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoop : MonoBehaviour {

  public MovingFadeOut[] movingFadeOuts;

  public delegate void StoopDelegate();
  public StoopDelegate StoopFinishEvent;
  [Header("以下为私有属性")]
  public int stoopStatus = 0;
  public bool needStoop = false;
  public static int kMaxStatus = 3;

  private Animator animator => this.gameObject.GetComponent<Animator>();

  // Start is called before the first frame update
  void Start() {
    foreach( var outs in movingFadeOuts) {
      outs.FadeEvent += MovingFadeOutFinish;
    }
    StoopFinishEvent += () => {};
  }

  // Update is called once per frame
  void Update() {
    if (needStoop) PlayStoopAnimation();
    if (stoopStatus >= kMaxStatus) StoopFinishEvent();
  }

  void PlayStoopAnimation() {
    animator.Play("stoop_" + stoopStatus.ToString());
    needStoop = false;
  }

  void MovingFadeOutFinish() {
    needStoop = true;
    stoopStatus += 1;
  }
}