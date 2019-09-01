using System;
using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Jsky.Manager {

  public class StoryBoard : MonoBehaviour {

    public Image bgImage;
    public GameObject[] texts;

    void Awake() {
      ResetStoryBoard();
    }

    void ResetStoryBoard() {
      bgImage.color = Color.clear;
      foreach (var text in texts) {
        text.SetActive(false);
      }
    }

    public void PlayStoryText(int index, Action callback) {
      bgImage.color = Color.black;
      var targetText = texts[index];
      targetText.SetActive(true);
      for (int i = 0; i < targetText.transform.childCount; i++) {
        var subWords = targetText.transform.GetChild(i);
        subWords.gameObject.SetActive(false);
      }
      StartCoroutine(ShowSubWords(targetText, 2.0f, callback));
    }

    IEnumerator ShowSubWords(GameObject targetText, float time, Action callback) {
      for (int i = 0; i < targetText.transform.childCount; i++) {
        var subWords = targetText.transform.GetChild(i);
        subWords.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
      }
      yield return new WaitForSeconds(time);

      ResetStoryBoard();
      callback();
    }
  }
}