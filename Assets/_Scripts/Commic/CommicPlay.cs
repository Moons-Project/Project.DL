using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class CommicPlay : MonoBehaviour {
  [System.Serializable]
  public struct ComicMessage {
    public GameObject gameObject;
    public float waitTime;
  }

  public ComicMessage[] messages;
  public AssetName nextScene;


  private Vector3 targetPostion;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(PlayVideo());
  }

  IEnumerator PlayVideo() {
    foreach (var message in messages) {
      message.gameObject.SetActive(true);
      Debug.Log( message.gameObject.name);
      var player = message.gameObject.GetComponent<VideoPlayer>();
      // player.Play();
      yield return new WaitForSeconds(message.waitTime);
      // player.Pause();
    }
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(nextScene.assetName, LoadSceneMode.Single);
  }
}