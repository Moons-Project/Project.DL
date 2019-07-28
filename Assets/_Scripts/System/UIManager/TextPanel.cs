using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPanel : MonoBehaviour {
  public TalkTachie talkTachie;

  void OnMouseDown() {
    talkTachie.OnDialogPress();
  }
}