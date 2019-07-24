using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AssetName))]
public class AssetsNameOnInspector : PropertyDrawer {
  public override void OnGUI(Rect position,
    SerializedProperty property, GUIContent label) {

    var streamingAsset = property.FindPropertyRelative("streamingAsset");
    var assetName = property.FindPropertyRelative("assetName");

    EditorGUI.BeginProperty(position, label, property);

    // Draw label
    position = EditorGUI.PrefixLabel(position,
      GUIUtility.GetControlID(FocusType.Passive), label);

    // // Don't make child fields be indented
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

    if (streamingAsset.objectReferenceValue != null) {
      var id = streamingAsset.objectReferenceValue.GetInstanceID();
      var assetPath = AssetDatabase.GetAssetPath(id);
      var fileName = Path.GetFileName(assetPath);
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