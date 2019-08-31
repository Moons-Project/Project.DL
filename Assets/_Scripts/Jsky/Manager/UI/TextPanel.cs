using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPanel : MonoBehaviour {
  [SerializeField]
  private TalkTachie talkTachie = null;

  void OnMouseDown() {
    talkTachie.OnDialogPress();
  }
}