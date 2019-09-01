using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jsky.Manager {

  public class TextPanel : MonoBehaviour {
    [SerializeField]
    private TalkTachie talkTachie = null;

    void OnMouseDown() {
      talkTachie.OnDialogPress();
    }
  }
}