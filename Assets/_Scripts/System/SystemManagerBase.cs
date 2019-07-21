using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemManagerBase<T> : MonoBehaviour where T : SystemManagerBase<T>{

  protected static SystemManagerBase<T> _Instance;

  void Awake() {
    if (_Instance == null) {
      _Instance = this;
      this.gameObject.transform.parent = null;
      DontDestroyOnLoad(this.gameObject);
    } else {
      Destroy(this.gameObject);
    }
  }

  public static T Instance => _Instance as T ;
}