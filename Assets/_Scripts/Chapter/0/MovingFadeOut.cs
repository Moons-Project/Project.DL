using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFadeOut : MonoBehaviour {

  public SpriteRenderer[] sprites;
  public float totalTime;

  public delegate void AllFadeDelegate();
  public AllFadeDelegate FadeEvent;

  [Header("以下为私有变量")]
  public float passTime = 0.0f;
  public Vector3 lastPosition;
  public bool isMoving => Vector3.Distance(this.lastPosition, this.transform.position) > 0;
  public bool fadeOutFinished = false;

  void Start() {
    lastPosition = this.transform.position;

    FadeEvent += () => {
      fadeOutFinished = true;
    };
  }

  // Update is called once per frame
  void Update() {
    if (isMoving && !fadeOutFinished) FadeOut();
  }

  void FadeOut() {
    if (passTime >= totalTime) {
      FadeEvent();
    } else {
      int index = Mathf.FloorToInt(passTime / totalTime * sprites.Length);
      float changeValue = Time.deltaTime / totalTime * sprites.Length;
      var color = sprites[index].color;
      color.a -= changeValue;
      sprites[index].color = color;
      passTime += Time.deltaTime;
      lastPosition = this.transform.position;
    }
  }
}