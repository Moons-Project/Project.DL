using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jsky.Common;

public class InitialSystemObject : MonoBehaviour {

  // // public AssetPath systemManagerPrefabPath;
  // public GameObject systemManagerPrefab;

  // void Awake() {
  //   // var prefabPath = systemManagerPrefabPath.ResourcesPath;
  //   if (Manager.Instance != null) return;
  //   // Debug.Log($"Initial SystemManager, AssetsPath is {prefabPath}");
  //   // var prefabGameObject = Resources.Load(prefabPath) as GameObject;
  //   var systemManager = Instantiate(systemManagerPrefab);
  //   DontDestroyOnLoad(systemManager.gameObject);

  //   // // move this part logic to SystemManager class
  //   // // 最后还是去掉了X
  //   // Enumerable.Range(0, systemManager.transform.childCount).ToList().ForEach(
  //   //   new Action<int>(i => {
  //   //     var x = systemManager.transform.GetChild(0).gameObject;
  //   //     x.transform.parent = null;
  //   //      DontDestroyOnLoad(x);
  //   //   }));

  //   // Destroy(systemManager);
  // }
}