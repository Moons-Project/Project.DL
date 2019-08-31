using UnityEngine;

[System.Serializable]
public class AssetPath {

  public Object streamingAsset;
  public string assetPath;

  private static string kResourcesKey = "Resources/";

  public string ResourcesPath {
    get {
      if (this.assetPath == null || this.assetPath == string.Empty) {
        return this.assetPath;
      }
      var prefabPath = this.assetPath.Substring(
        assetPath.IndexOf(kResourcesKey) + kResourcesKey.Length);
      return prefabPath.Substring(0, 
        prefabPath.LastIndexOf('.') == -1 ? 
        prefabPath.Length :
        prefabPath.LastIndexOf('.'));
    }
  }
}