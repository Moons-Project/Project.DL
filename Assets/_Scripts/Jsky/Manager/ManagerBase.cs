using UnityEngine;

namespace Jsky.Manager {

  public abstract class ManagerBase<T>:
    MonoBehaviour where T : ManagerBase<T> {

      protected static ManagerBase<T> _Instance;

      public void Awake() {
        if (_Instance == null) {
          _Instance = this;
        } else {
          Destroy(this.gameObject);
        }
      }

      public static T Instance => _Instance as T;
    }
}