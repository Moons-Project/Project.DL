using UnityEditor;
using UnityEngine;

public class CommandBuild {
  private static string[] ms_scenes = {
    "Assets/Scenes/KillerStarter.unity"
  };

  private static bool ms_isDebugBuild = false;
  private static BuildTarget ms_buildTarget = BuildTarget.Android;

  private static string XCODE_PROJECT_NAME = "XCodeProject";
  private static string BUILD_OUTPUT_ANDROID = "Bin/Android/";

  private static void UpdateBuildFlag() {
    string[] args = System.Environment.GetCommandLineArgs();
    foreach (string oneArg in args) {
      if (oneArg != null && oneArg.Length > 0) {
        if (oneArg.ToLower().Contains("-debug")) {
          Debug.Log("\"-debug\" is detected, switch to debug build.");
          ms_isDebugBuild = true;
          return;
        } else if (oneArg.ToLower().Contains("-release")) {
          Debug.Log("\"-release\" is detected, switch to release build.");
          ms_isDebugBuild = false;
          return;
        }
      }
    }

    if (ms_isDebugBuild) {
      Debug.Log("neither \"-debug\" nor \"-release\" is detected, current is to debug build.");
    } else {
      Debug.Log("neither \"-debug\" nor \"-release\" is detected, current is to release build.");
    }
  }
  private static void UpdateBuildTarget() {
    string[] args = System.Environment.GetCommandLineArgs();
    foreach (string oneArg in args) {
      if (oneArg != null && oneArg.Length > 0) {
        if (oneArg.ToLower().Contains("-android")) {
          Debug.Log("\"-android\" is detected, switch build target to android.");
          ms_buildTarget = BuildTarget.Android;
          return;
        } else if (oneArg.ToLower().Contains("-iphone")) {
          Debug.Log("\"-iphone\" is detected, switch build target to iphone.");
          ms_buildTarget = BuildTarget.iOS;
          return;
        } else if (oneArg.ToLower().Contains("-ios")) {
          Debug.Log("\"-ios\" is detected, switch build target to iphone.");
          ms_buildTarget = BuildTarget.iOS;
          return;
        }
      }
    }

    Debug.Log("neither \"-android\", \"-ios\" nor \"-iphone\" is detected, current build target is: " + ms_buildTarget);
  }
  public static void PreBuild() {
    Debug.Log("PreBuild");
    UpdateBuildFlag();
    SetKgfDebugActive(ms_isDebugBuild);
  }
  public static void Build() {
    Debug.Log("Build");
    UpdateBuildTarget();

    BuildOptions buildOption = BuildOptions.None;
    if (ms_isDebugBuild) {
      buildOption |= BuildOptions.Development;
      buildOption |= BuildOptions.AllowDebugging;
      buildOption |= BuildOptions.ConnectWithProfiler;
    } else {
      buildOption |= BuildOptions.None;
    }

    string locationPathName;
    if (BuildTarget.iOS == ms_buildTarget) {
      locationPathName = XCODE_PROJECT_NAME;
    } else {
      locationPathName = BUILD_OUTPUT_ANDROID;
      System.DateTime time = System.DateTime.Now;
      locationPathName += "killer_" + time.Month.ToString("D2") + time.Day.ToString("D2") +
        "_" + time.Hour.ToString("D2") + time.Minute.ToString("D2") + ".apk";
    }
    BuildPipeline.BuildPlayer(ms_scenes, locationPathName, ms_buildTarget, buildOption);
  }
  public static void PostBuild() {
    Debug.Log("PostBuild");
  }

  private static void SetKgfDebugActive(bool activated) {
    ///非重点，略
  }
}