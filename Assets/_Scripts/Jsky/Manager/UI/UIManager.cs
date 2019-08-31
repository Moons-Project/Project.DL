using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jsky.Manager {

public class UIManager : ManagerBase<UIManager> {

  public TalkTachie talkTachie = null;
  public TipsLayer tipsLayer = null;


  // [SerializeField]
  // private GameObject talkTachieGameObject;

  new void Awake() {
    base.Awake();
    talkTachie.activeDialogPanel();
  }

  // void Start() {
  // }

  // public void ShowHintText(string hintText, 
  //   float delay = 5.0f, float afterTime = 0.0f) {
  //   tipsLayer.ShowHintText(hintText, delay, afterTime);
  // }

  // public void PlayScript(string scriptName) {
  //   talkTachie.PlayScript(scriptName);
  // }
}
}