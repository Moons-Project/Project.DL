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
  public AssetName BGMAudio;
  public AssetName PeopleSEAudio;
  public PlayableDirector TimeLine;

  [SerializeField]
  private bool isPlayVideoPaused => !InputManager.Instance.SlideRightAction;

  private static float kPauseTime = 1.0f;
  private static float kMaxPauseTime = 5.0f;

  public void _PlayBGMAudio() {
    AudioManager.Instance.PlayBGM(BGMAudio.assetName);
  }

  public void _PlayPeopleSEAudio() {
    AudioManager.Instance.PlaySE(PeopleSEAudio.assetName);
  }

  // Start is called before the first frame update
  void Start() {
    AudioManager.Instance.PlayBGM(BGMAudio.assetName);
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
        while (timeCount + (kPauseTime - playTimeCount) < kMaxPauseTime && 
               kPauseTime - playTimeCount > 0) {
          if (isPlayVideoPaused) {
            TimeLine.Pause();
          } else {
            TimeLine.Play();
            playTimeCount += Time.deltaTime;
          }
          timeCount += Time.deltaTime;
          yield return null;
        }
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