using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitialSystemObject : MonoBehaviour {

  public AssetPath systemManagerPrefabPath;

  void Awake() {
    var prefabPath = systemManagerPrefabPath.ResourcesPath;
    Debug.Log($"AssetsPath is {prefabPath}");
    var prefabGameObject = Resources.Load(prefabPath) as GameObject;
    var systemManager = Instantiate(prefabGameObject);

    // // move this part logic to SystemManager class
    // Enumerable.Range(0, systemManager.transform.childCount).ToList().ForEach(
    //   new Action<int>(i => {
    //     var x = systemManager.transform.GetChild(0).gameObject;
    //     x.transform.parent = null;
    //      DontDestroyOnLoad(x);
    //   }));

    Destroy(systemManager);
  }
}