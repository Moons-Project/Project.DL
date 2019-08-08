// 自動打包，腳本

using UnityEditor;

class Builder {
  static void build() {

    // Place all your scenes here
    string[] scenes = { "Assets/_Scenes/MainMenu.unity" };
    // Deploy path
    string pathToDeploy = "D:\\Desktop\\output\\Project.DL.exe";

    BuildPipeline.BuildPlayer(scenes, pathToDeploy,
      BuildTarget.StandaloneWindows, BuildOptions.None);

    EditorApplication.Exit(0);
  }
}