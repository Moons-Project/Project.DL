using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour {

  public GameObject[] disableButtons;


  [Header("开始游戏跳转的场景请拖到这里")]
  [Tooltip("开始游戏跳转的场景哒")]
  public AssetName sceneName;

  // Start is called before the first frame update
  void Start() {
    foreach (var button in disableButtons) {
      button.GetComponent<Button>().interactable = false;
    }
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