using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jsky.Common;

namespace Jsky.Manager {

public class AudioManager : ManagerBase<AudioManager> {
  [SerializeField]
  private GameObject AudioSourcePrefab = null;

  [SerializeField]
  private GameObject BGMSource = null;
  [SerializeField]
  private AudioSource BGMAudioSource = null;
  [SerializeField]
  private string currentBGM = null;

  [SerializeField]
  private List<AudioSource> SEAudioSourceList;


  new void Awake() {
    base.Awake();

    BGMSource = Instantiate(AudioSourcePrefab);
    BGMSource.name = "BGM Player";
    BGMSource.transform.parent = this.transform;
    BGMAudioSource = BGMSource.GetComponent<AudioSource>();
    BGMAudioSource.loop = true;

    SEAudioSourceList = new List<AudioSource>();
  }


  public void PlayBGM(string name) {
    if (currentBGM != name) {
      currentBGM = name;
      BGMAudioSource.clip = DBManager.Instance.BGMDict[name];
      BGMAudioSource.Play();
    }
  }

  public void StopBGM() {
    BGMAudioSource.Stop();
  }

  public void StopSE() {
    // Find in pool
    foreach (var source in SEAudioSourceList) {
      source.Stop();
    }
  }

  public AudioSource PlaySE(string name) {
    AudioSource audioSource = null;
    bool found = false;

    // Find in pool
    foreach (var source in SEAudioSourceList) {
      if (source.isPlaying == false) {
        audioSource = source;
        found = true;
      }
    }

    // Not found, make one and add it to pool
    if (!found) {
      var SESource = Instantiate(AudioSourcePrefab) as GameObject;
      SESource.name = $"SE Player{SEAudioSourceList.Count}";
      SESource.transform.parent = this.transform;
      audioSource = SESource.GetComponent<AudioSource>();
      SEAudioSourceList.Add(audioSource);
    }

    audioSource.clip = DBManager.Instance.SEDict[name];
    audioSource.Play();
    return audioSource;
  }

  public void ChangeBGMVolume(float number) {
    BGMAudioSource.volume = number / 100f;
  }

  public void ChangeSEVolume(float number) {
    float res = number / 100f;
    AudioSourcePrefab.GetComponent<AudioSource>().volume = res;
    foreach (var source in SEAudioSourceList) {
      source.volume = res;
    }
  }
}
}