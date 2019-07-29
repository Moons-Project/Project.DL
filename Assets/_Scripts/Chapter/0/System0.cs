using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System0 : MonoBehaviour {

  [SerializeField]
  private AssetName firstScript = null;

  // Start is called before the first frame update
  void Start() {
    UIManager.Instance.PlayScript(firstScript.assetName);
    StartCoroutine(
      UIManager.Instance.WaitUnitilScriptFinished(()=>{
        Debug.Log("Finished");
    }));
  }

  // // Update is called once per frame
  // void Update() {
  //   Debug.Log("Update");
  // }
}