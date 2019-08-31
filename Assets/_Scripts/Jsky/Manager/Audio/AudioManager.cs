using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jsky.Common;

namespace Jsky.Manager {

public class AudioManager : ManagerBase<AudioManager> {
  [SerializeField]
  private GameObject AudioSourcePrefab = null;
  [SerializeField]
  private AssetPath BGMFolder = null;
  [SerializeField]
  private AssetPath SEFolder = null;

  [SerializeField]
  private GameObject BGMSource = null;
  [SerializeField]
  private AudioSource BGMAudioSource = null;
  [SerializeField]
  private string currentBGM = null;

  [SerializeField]
  private List<AudioSource> SEAudioSourceList;

  [SerializeField]
  private Dictionary<string, AudioClip> BGMDict =
    new Dictionary<string, AudioClip>();
  [SerializeField]
  private Dictionary<string, AudioClip> SEDict =
    new Dictionary<string, AudioClip>();

  new void Awake() {
    base.Awake();
    ImportResources();

    BGMSource = Instantiate(AudioSourcePrefab);
    BGMSource.name = "BGM Player";
    BGMSource.transform.parent = this.transform;
    BGMAudioSource = BGMSource.GetComponent<AudioSource>();
    BGMAudioSource.loop = true;

    SEAudioSourceList = new List<AudioSource>();
  }

  void ImportResources() {
    Object[] bgms = Resources.LoadAll(
      BGMFolder.ResourcesPath, typeof(AudioClip));
    Object[] ses = Resources.LoadAll(
      SEFolder.ResourcesPath, typeof(AudioClip));

    foreach (var clip in bgms) {
      BGMDict.Add(clip.name, clip as AudioClip);
      Debug.Log($"BGMDict add {clip.name}");
    }

    foreach (var clip in ses) {
      SEDict.Add(clip.name, clip as AudioClip);
      Debug.Log($"SEDict add {clip.name}");
    }
  }

  public void PlayBGM(string name) {
    if (currentBGM != name) {
      currentBGM = name;
      BGMAudioSource.clip = BGMDict[name];
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

    audioSource.clip = SEDict[name];
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