using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoard : MonoBehaviour {
  public Image bgImage;
  public GameObject[] texts;

  public delegate void FinishDelegate();
  public FinishDelegate FinishEvent;

  void Awake() {
    ResetStoryBoard();
    FinishEvent += ResetStoryBoard;
  }

  void ResetStoryBoard() {
    bgImage.color = Color.clear;
    foreach(var text in texts) {
      text.SetActive(false);
    }
  }

  public void PlayStoryText(int index) {
    bgImage.color = Color.black;
    var targetText = texts[index];
    targetText.SetActive(true);
    for(int i = 0; i < targetText.transform.childCount; i++) {
      var subWords = targetText.transform.GetChild(i);
      subWords.gameObject.SetActive(false);
    }
    StartCoroutine(ShowSubWords(targetText, 2.0f));
  }

  IEnumerator ShowSubWords(GameObject targetText, float time) {
    for(int i = 0; i < targetText.transform.childCount; i++) {
      var subWords = targetText.transform.GetChild(i);
      subWords.gameObject.SetActive(true);
      yield return new WaitForSeconds(time);
    }
    yield return new WaitForSeconds(time);

    FinishEvent();
  }
}