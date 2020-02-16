using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ItemOnScene))]
[CanEditMultipleObjects]
public class ItemSetting : Editor {
    SerializedProperty typeItem;
    SerializedProperty lootDropItem;

    SerializedProperty damageEvent;
    SerializedProperty AIEvent;
    SerializedProperty objects;
    SerializedProperty objectsName;
    

    void OnEnable() {
        typeItem = serializedObject.FindProperty("typeObject");
        lootDropItem = serializedObject.FindProperty("lootDropItem");
        damageEvent = serializedObject.FindProperty("damageEvent");
        AIEvent = serializedObject.FindProperty("AIEvent");

        objects = serializedObject.FindProperty("objects");
        objectsName = serializedObject.FindProperty("objectsName");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(typeItem);
        if (typeItem.enumNames[typeItem.enumValueIndex] == "Asteroid") {
            EditorGUILayout.PropertyField(lootDropItem);
            EditorGUILayout.PropertyField(objectsName);
            EditorGUILayout.PropertyField(objects);
            EditorGUILayout.PropertyField(damageEvent);
        } else if (typeItem.enumNames[typeItem.enumValueIndex] == "Turret") {
            EditorGUILayout.PropertyField(lootDropItem);
            EditorGUILayout.PropertyField(objectsName);
            EditorGUILayout.PropertyField(objects);
            EditorGUILayout.PropertyField(damageEvent);
            EditorGUILayout.PropertyField(AIEvent);
        } else if (typeItem.enumNames[typeItem.enumValueIndex] == "Mine") {
        } else if (typeItem.enumNames[typeItem.enumValueIndex] == "Container") {
            EditorGUILayout.PropertyField(lootDropItem);
            EditorGUILayout.PropertyField(objectsName);
            EditorGUILayout.PropertyField(objects);
            EditorGUILayout.PropertyField(damageEvent);
        } else if (typeItem.enumNames[typeItem.enumValueIndex] == "AbadonnedShip") {
            EditorGUILayout.PropertyField(lootDropItem);
            EditorGUILayout.PropertyField(objectsName);
            EditorGUILayout.PropertyField(objects);
            EditorGUILayout.PropertyField(damageEvent);
        }



        serializedObject.ApplyModifiedProperties();
    }
}
