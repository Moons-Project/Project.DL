using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SystemManagerBase<UIManager> {

  [SerializeField]
  private TalkTachie talkTachie;
  // [SerializeField]
  // private GameObject talkTachieGameObject;

  void Start() {
    // HideDialog();
  }

  void TalkTachie_Hide() {
    talkTachie.HideDialog();
  }

  

}