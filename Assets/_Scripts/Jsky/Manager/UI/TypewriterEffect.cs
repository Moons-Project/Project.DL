using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jsky.Manager {

  [RequireComponent(typeof(Text))]
  public class TypewriterEffect : MonoBehaviour {
    public Text textComponent;
    public float delay = 0.2f;

    private string currentText = "";
    private int currentIndex = 0;
    private bool skip = false;

    [HideInInspector]
    public bool typing = false;

    public void SetText(string text, bool append = false) {
      SetText(text, append, () => {});
    }

    public void SetText(string text, bool append, System.Action callback) {
      StopAllCoroutines();

      if (!append)
        textComponent.text = "";
      currentText = text;
      currentIndex = 0;
      typing = true;
      skip = false;
      StartCoroutine(Type(callback));
    }

    IEnumerator Type(System.Action callback) {
      if (skip) {
        skip = false;
        typing = false;
        textComponent.text += currentText.Substring(currentIndex);
        callback();
        yield return null;
      } else {
        textComponent.text += currentText[currentIndex++];
        yield return new WaitForSeconds(delay);
        if (currentIndex >= currentText.Length) {
          // Stop Here
          typing = false;
          callback();
        } else {
          StartCoroutine(Type(callback));
        }
      }
    }

    public void Skip() {
      skip = true;
    }
  }
}