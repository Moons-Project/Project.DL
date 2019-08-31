// 一些编辑器Asset相关的类

using UnityEngine;

namespace Jsky.Common {

  // 基础类
  public class AssetOnInspectorBase {
    // 用作索引文件
    public Object streamingAsset;
  }

  // 文件名相关
  [System.Serializable]
  public class AssetName : AssetOnInspectorBase {
    // 储存文件名
    public string assetName;
  }

  // 文件路径相关
  [System.Serializable]
  public class AssetPath : AssetOnInspectorBase {
    // 储存文件路径
    public string assetPath;

    // 常量字符串
    private static string kResourcesKey = StaticValues.ResourcesPathKey;

    // 判断文件路径是否为空
    public bool isAssetPathEmpty => 
      this.assetPath == null || this.assetPath == string.Empty;

    // 相对于资源文件夹下路径
    public string ResourcesPath {
      get {
        if (isAssetPathEmpty) {
          return this.assetPath;
        }
        // 去掉前缀
        var prefabPath = this.assetPath.Substring(
          assetPath.IndexOf(kResourcesKey) + kResourcesKey.Length);
        // 返回去掉后缀名的部分
        return prefabPath.Substring(0,
          prefabPath.LastIndexOf('.') == -1 ?
          prefabPath.Length :
          prefabPath.LastIndexOf('.'));
      }
    }

  }

}