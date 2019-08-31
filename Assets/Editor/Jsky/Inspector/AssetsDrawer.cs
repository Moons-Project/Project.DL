// 一些资源文件在Inpector上的绘制方式类

using System.IO;

using UnityEditor;
using UnityEngine;

using Jsky.Common;


[CustomPropertyDrawer(typeof(AssetPath))]
public class AssetsPathOnInspector : PropertyDrawer {

  // 需要两倍高度
  public override float GetPropertyHeight(SerializedProperty property,
    GUIContent label) {
    return base.GetPropertyHeight(property, label) * 2;
  }

  // 绘制
  public override void OnGUI(Rect position,
    SerializedProperty property, GUIContent label) {

    // 储存property
    var streamingAsset = property.FindPropertyRelative("streamingAsset");
    var assetPath = property.FindPropertyRelative("assetPath");
    // 开始绘制
    EditorGUI.BeginProperty(position, label, property);
    // Calculate rects
    var assetRect = new Rect(position.x, position.y,
      position.width, position.height / 2);
    var pathRect = new Rect(position.x, position.y + position.height / 2,
      position.width, position.height / 2);
    // Draw label
    assetRect = EditorGUI.PrefixLabel(assetRect,
      GUIUtility.GetControlID(FocusType.Passive), label);
    EditorGUI.PropertyField(assetRect,
      property.FindPropertyRelative("streamingAsset"), GUIContent.none);
    // 得到property
    if (streamingAsset.objectReferenceValue != null) {
      var id = streamingAsset.objectReferenceValue.GetInstanceID();
      var assetPath2 = AssetDatabase.GetAssetPath(id);
      assetPath.stringValue = assetPath2;
    } else {
      assetPath.stringValue = string.Empty;
    }
    EditorGUI.LabelField(pathRect, "Asset Path :" + assetPath.stringValue);
    // Set indent back to what is was
    EditorGUI.EndProperty();
  }
}


[CustomPropertyDrawer(typeof(AssetName))]
public class AssetsNameOnInspector : PropertyDrawer {
  public override void OnGUI(Rect position,
    SerializedProperty property, GUIContent label) {
    // 储存property
    var streamingAsset = property.FindPropertyRelative("streamingAsset");
    var assetName = property.FindPropertyRelative("assetName");
    // 开始绘制
    EditorGUI.BeginProperty(position, label, property);
    // Draw label
    position = EditorGUI.PrefixLabel(position,
      GUIUtility.GetControlID(FocusType.Passive), label);
    // Don't make child fields be indented
    var indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;
    // Calculate rects
    var assetRect = new Rect(position.x, position.y,
      position.width / 2, position.height);
    var pathRect = new Rect(position.x + position.width / 2 + 10, position.y,
      position.width / 2 - 10, position.height);
    // Draw fields - pass GUIContent.none to each so they can draw with labels
    EditorGUI.PropertyField(assetRect,
      property.FindPropertyRelative("streamingAsset"), GUIContent.none);
    // 得到property
    if (streamingAsset.objectReferenceValue != null) {
      var id = streamingAsset.objectReferenceValue.GetInstanceID();
      var assetPath = AssetDatabase.GetAssetPath(id);
      var fileName = Path.GetFileName(assetPath);
      // 去掉扩展名
      assetName.stringValue = fileName.Substring(0, fileName.LastIndexOf('.'));
    } else {
      assetName.stringValue = "Empty";
    }
    EditorGUI.LabelField(pathRect, assetName.stringValue);
    // Set indent back to what is was
    EditorGUI.indentLevel = indent;
    EditorGUI.EndProperty();
  }
}