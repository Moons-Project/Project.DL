using Jsky.Common;
using UnityEngine;


namespace Jsky.Manager {

  public class Manager {

    public static UIManager UI => UIManager.Instance;
    public static AudioManager Audio => AudioManager.Instance;
    public static CameraManager Camera => CameraManager.Instance;
    public static InputManager Input => InputManager.Instance;



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Main() {
      var prefab = Resources.Load(StaticValues.SystemManagerPath);
      var systemManager = GameObject.Instantiate(prefab) as GameObject;
      GameObject.DontDestroyOnLoad(systemManager.gameObject);
    }
  }

}