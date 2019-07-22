using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SystemManagerBase<AudioManager> {
  [SerializeField] 
  private GameObject AudioSourcePrefab;
  [SerializeField] 
  private AssetPath BGMFolder;
  [SerializeField] 
  private AssetPath SEFolder;

  [SerializeField] 
  private GameObject BGMSource;
  [SerializeField] 
  private AudioSource BGMAudioSource;
  [SerializeField] 
  private string currentBGM = null;

  [SerializeField] 
  private List<AudioSource> SEAudioSourceList;

  [SerializeField] 
  private Dictionary<string, AudioClip> BGMDict, SEDict;

  new void Awake() {
    base.Awake();
    BGMDict = new Dictionary<string, AudioClip>();
    SEDict = new Dictionary<string, AudioClip>();

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
