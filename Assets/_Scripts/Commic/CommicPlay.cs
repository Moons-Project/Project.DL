using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CommicPlay : MonoBehaviour {

  public GameObject[] movies;

  private Vector3 targetPostion;

  // Start is called before the first frame update
  void Start() {
    StartCoroutine(PlayVideo());
  }

  IEnumerator PlayVideo() {
    foreach (var movie in movies) {
      Debug.Log(movie.name);
      var player = movie.GetComponent<VideoPlayer>();
      player.Play();
      yield return new WaitForSeconds(1);
      player.Pause();

    }
  }

  // Update is called once per frame
  void Update() {

  }
}