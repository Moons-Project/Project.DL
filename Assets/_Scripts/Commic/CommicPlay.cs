using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using Jsky.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[System.Serializable]
public struct ComicMessage {
  public GameObject gameObject;
  public float waitTime;
  public bool needSlideRight;
  public UnityEvent unityEvent;
}

public class CommicPlay : MonoBehaviour {

  public AssetName nextScene;
  public PlayableDirector timeLine;
  public ComicMessage[] messages;

  public int storyBoardIndex = -1;
  [Header("一些常量")]
  public static float kPauseTime = 1.0f;
  public static float kPlayerPauseTime = 0.5f;
  public static float kMaxPauseTime = 5.0f;

  private bool isPlayVideoPaused => !InputManager.Instance.SlideRightAction;

  // Start is called before the first frame update
  void Start() {
    timeLine.Pause();
    if (storyBoardIndex >= 0) {
      UIManager.Instance.storyBoard.PlayStoryText(storyBoardIndex, StartPlayVideo);
    } else {
      StartPlayVideo();
    }
  }

  void StartPlayVideo() {
    StartCoroutine(PlayVideo());
  }

  IEnumerator PlayVideo() {
    timeLine.Play();
    foreach (var message in messages) {
      // var player = message.gameObject.GetComponent<VideoPlayer>();
      // player.Play(); // player.Pause();

      // pasu logic
      if (!message.needSlideRight) {
        if (!message.Equals(messages[0]))
          yield return new WaitForSeconds(kPauseTime);
      } else {
        // // One way to pause
        // while(isPaused){ yield return null; }
        var timeCount = 0.0f;
        var playTimeCount = 0.0f;
        // 当有足够的剩余时间且滑动时间不满足
        while (timeCount + (kPauseTime - playTimeCount) < kMaxPauseTime &&
          kPlayerPauseTime - playTimeCount > 0) {
          if (isPlayVideoPaused) {
            timeLine.Pause();
          } else {
            timeLine.Play();
            playTimeCount += Time.deltaTime;
          }
          timeCount += Time.deltaTime;
          yield return null;
        }
        // 当滑动时间不够动画时间
        if (kPauseTime - playTimeCount > 0) {
          timeLine.Play();
          yield return new WaitForSeconds(kPauseTime - playTimeCount);
        }
      }

      message.gameObject.SetActive(true);
      message.unityEvent.Invoke();
      Debug.Log("Play " + message.gameObject.name);
      yield return new WaitForSeconds(message.waitTime);
    }

    yield return new WaitForSeconds(2);
    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}