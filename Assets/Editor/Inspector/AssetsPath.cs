using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AssetPath))]
public class AssetsPathOnInspector : PropertyDrawer {

  public override float GetPropertyHeight(SerializedProperty property,
    GUIContent label) {
    return base.GetPropertyHeight(property, label) * 2;
  }

  public override void OnGUI(Rect position,
    SerializedProperty property, GUIContent label) {

    var streamingAsset = property.FindPropertyRelative("streamingAsset");
    var assetPath = property.FindPropertyRelative("assetPath");

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