using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenuMovie : MonoBehaviour {

  // [Header("一开始就播放的视频请拖到这里")]
  // [Tooltip("一开始就播放的视频哒")]
  // public VideoClip firstVideo;
  [Header("循环的视频请拖到这里")]
  [Tooltip("循环的视频哒")]
  public VideoPlayer secondVideo;

  private VideoPlayer vp;

  // Start is called before the first frame update
  void Start() {
    vp = GetComponent<VideoPlayer>();
    vp.loopPointReached += PlayLoopVideo;
  }

  void PlayLoopVideo(VideoPlayer vp) {
    vp.loopPointReached -= PlayLoopVideo;
    secondVideo.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
    StartCoroutine(Wait1SecondToPlay());
  }

IEnumerator Wait1SecondToPlay() {
    yield return new WaitForSeconds(0.1f);
    secondVideo.Play();
    yield return new WaitForSeconds(1f);
    Destroy(vp.gameObject);
  }
}