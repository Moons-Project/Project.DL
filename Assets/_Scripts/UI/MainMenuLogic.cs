using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour {

  [Header("点不动的按钮们")]
  public GameObject[] disableButtons;
  // [Header("跨场景存在的物体们")]
  // public GameObject[] dontDestroyObjects;


  [Header("开始游戏跳转的场景请拖到这里")]
  [Tooltip("开始游戏跳转的场景哒")]
  public AssetName sceneName;

  // Start is called before the first frame update
  void Start() {
    // 禁用按钮
    foreach (var button in disableButtons) {
      button.GetComponent<Button>().interactable = false;
    }
    // 设置跨场景
    // foreach (var dontDestroyObject in dontDestroyObjects) {
    //   DontDestroyOnLoad(dontDestroyObject);
    // }
  }

  public void _NaviToFirstScene() {
    SceneManager.LoadScene(sceneName.assetName, LoadSceneMode.Single);
  }

  public void _QuitGame() {
    // save any game data here
#if UNITY_EDITOR
    // Application.Quit() does not work in the editor so
    // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }
}