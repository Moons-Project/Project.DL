using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using UnityEngine;

namespace Jsky.Manager {

  public interface IAudioManager {
    void PlayBGM(string name);
    void StopBGM();
    void PlaySE(string name);
    void StopSE();
    void ChangeBGMVolume(float number);
    void ChangeSEVolume(float number);
  }

  public class AudioManager : ManagerBase<AudioManager>, IAudioManager {
    [SerializeField]
    private GameObject AudioSourcePrefab = null;

    private GameObject BGMSource = null;
    private AudioSource BGMAudioSource = null;
    private string currentBGM = null;
    private List<AudioSource> SEAudioSourceList;

    new void Awake() {
      base.Awake();

      BGMSource = Instantiate(AudioSourcePrefab);
      BGMAudioSource = BGMSource.GetComponent<AudioSource>();

      BGMSource.name = "BGM Player";
      BGMSource.transform.parent = this.transform;
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

    public void PlaySE(string name) {
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
        audioSource = SESource.GetComponent<AudioSource>();
        SEAudioSourceList.Add(audioSource);

        SESource.name = $"SE Player{SEAudioSourceList.Count}";
        SESource.transform.parent = this.transform;
      }

      audioSource.clip = DBManager.Instance.SEDict[name];
      audioSource.Play();
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