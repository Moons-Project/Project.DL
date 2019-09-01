using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using UnityEngine;

namespace Jsky.Manager {

  public interface IDBManager {
    
  }

  public class DBManager : ManagerBase<DBManager>, IDBManager{
    [SerializeField]
    private AssetPath BGMFolder = null;
    [SerializeField]
    private AssetPath SEFolder = null;
    [SerializeField]
    private AssetPath TachieFolder = null;
    [SerializeField]
    private AssetPath ScriptsFolder = null;

    public Dictionary<string, AudioClip> BGMDict =
    new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> SEDict =
    new Dictionary<string, AudioClip>();
    public Dictionary<string, Sprite> TachieDict =
    new Dictionary<string, Sprite>();

    new void Awake() {
      base.Awake();
      ImportResources();
    }

    void ImportResources() {
      Object[] bgms = Resources.LoadAll(
        BGMFolder.ResourcesPath, typeof(AudioClip));
      Object[] ses = Resources.LoadAll(
        SEFolder.ResourcesPath, typeof(AudioClip));
      Object[] tachies = Resources.LoadAll(
        TachieFolder.ResourcesPath, typeof(Sprite));

      foreach (var clip in bgms) {
        BGMDict.Add(clip.name, clip as AudioClip);
        Debug.Log($"BGMDict add {clip.name}");
      }

      foreach (var clip in ses) {
        SEDict.Add(clip.name, clip as AudioClip);
        Debug.Log($"SEDict add {clip.name}");
      }

      foreach (var img in tachies) {
        TachieDict.Add(img.name, img as Sprite);
        Debug.Log($"TachieDict add {img.name}");
      }
    }

    public void LoadScripts(string fileName, out TextAsset asset) {
      Load<TextAsset>(ScriptsFolder.ResourcesPath, fileName, out asset);
    }

    public void Load<T>(string folderName, string fileName, out T asset)
    where T : class {
      Load<T>(folderName + "/" + fileName, out asset);
    }

    public void Load<T>(string path, out T asset)
    where T : class {
      asset = Resources.Load(path, typeof(T)) as T;
    }
  }
}