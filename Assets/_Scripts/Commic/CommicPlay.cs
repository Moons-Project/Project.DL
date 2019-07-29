using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CommicPlay : MonoBehaviour {
  [System.Serializable]
  public struct ComicMessage {
    public GameObject gameObject;
    public float waitTime;
    public bool needSlideRight;
    public UnityEvent unityEvent;
  }

  public ComicMessage[] messages;
  public AssetName nextScene;

  public PlayableDirector TimeLine;

  [SerializeField]
  private bool isPlayVideoPaused => !InputManager.Instance.SlideRightAction;

  public static float kPauseTime = 1.0f;
  public static float kPlayerPauseTime = 0.5f;
  public static float kMaxPauseTime = 5.0f;


  // Start is called before the first frame update
  void Start() {
    StartCoroutine(PlayVideo());
  }

  void Update() {

  }

  IEnumerator PlayVideo() {
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
            TimeLine.Pause();
          } else {
            TimeLine.Play();
            playTimeCount += Time.deltaTime;
          }
          timeCount += Time.deltaTime;
          yield return null;
        }
        // 当滑动时间不够动画时间
        if (kPauseTime - playTimeCount > 0) {
          TimeLine.Play();
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