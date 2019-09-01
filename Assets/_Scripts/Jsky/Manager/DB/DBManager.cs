using System.Collections;
using System.Collections.Generic;
using Jsky.Common;
using UnityEngine;

namespace Jsky.Manager {

  public interface IDBManager {

  }

  public class DBManager : ManagerBase<DBManager>, IDBManager {

    [System.Serializable]
    public enum FolderTypes {
      BGMFolder,
      SEFolder,
      TachieFolder,
      ScriptsFolder
    }

    [System.Serializable]
    public class FolderInfo {
      public FolderTypes type;
      public AssetPath folder;
    }

    public List<FolderInfo> folders;

    public string ResourcesPath(FolderTypes type) {
      return folders.Find((info) => info.type == type).folder.ResourcesPath;
    }

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
        ResourcesPath(FolderTypes.BGMFolder), typeof(AudioClip));
      Object[] ses = Resources.LoadAll(
        ResourcesPath(FolderTypes.SEFolder), typeof(AudioClip));
      Object[] tachies = Resources.LoadAll(
        ResourcesPath(FolderTypes.TachieFolder), typeof(Sprite));

      foreach (var clip in bgms) {
        BGMDict.Add(clip.name, clip as AudioClip);
        // Debug.Log($"BGMDict add {clip.name}");
      }

      foreach (var clip in ses) {
        SEDict.Add(clip.name, clip as AudioClip);
        // Debug.Log($"SEDict add {clip.name}");
      }

      foreach (var img in tachies) {
        TachieDict.Add(img.name, img as Sprite);
        // Debug.Log($"TachieDict add {img.name}");
      }
    }

    public void LoadScripts(string fileName, out TextAsset asset) {
      Load<TextAsset>(ResourcesPath(FolderTypes.ScriptsFolder), fileName, out asset);
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