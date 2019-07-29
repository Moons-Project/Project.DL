using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SystemManagerBase<UIManager> {

  [SerializeField]
  private TalkTachie talkTachie = null;

  // [SerializeField]
  // private GameObject talkTachieGameObject;

  void Start() {
    // HideDialog();
  }

  // void TalkTachie_Hide() {
  //   talkTachie.HideDialog();
  // }

  public void PlayScript(string scriptName) {
    talkTachie.PlayScript(scriptName);
  }

  public IEnumerator WaitUnitilScriptFinished(Action action) {
    var trigger = false;
    Action changeTrigger = () => trigger = true;
    talkTachie.FinishedEvent += changeTrigger.Invoke;
    yield return new WaitUntil(() => trigger);
    talkTachie.FinishedEvent -= changeTrigger.Invoke;
    action.Invoke();
  }
}