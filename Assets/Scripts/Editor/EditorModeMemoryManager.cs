#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad][ExecuteInEditMode]
public class EditorModeMemoryManager
{
    static EditorModeMemoryManager()
    {
        EditorApplication.update -= Update;
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        if (EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            ClearMemory();
        }
    }

    private static void ClearMemory()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Debug.Log("Memory cleared after exiting play mode.");
    }
}

[CustomPropertyDrawer(typeof(TypeRequireAttribute))]
public class RequireTypeDrawer : PropertyDrawer
{
    private const float ButtonHeight = 18f;

    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        var attribute = (TypeRequireAttribute)this.attribute;

        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.LabelField(
                position,
                label,
                "RequireType는 ObjectReference에만 사용할 수 있습니다."
            );

            return;
        }

        MonoScript currentScript =
            property.objectReferenceValue as MonoScript;

        Rect labelRect = new Rect(
            position.x,
            position.y,
            EditorGUIUtility.labelWidth,
            position.height
        );

        Rect buttonRect = new Rect(
            position.x + EditorGUIUtility.labelWidth,
            position.y,
            position.width - EditorGUIUtility.labelWidth,
            ButtonHeight
        );

        EditorGUI.LabelField(labelRect, label);

        string currentName = currentScript != null
            ? currentScript.name
            : "None";

        if (GUI.Button(buttonRect, currentName, EditorStyles.popup))
        {
            ShowMenu(property, attribute.Type);
        }
    }

    private void ShowMenu(
        SerializedProperty property,
        Type requiredType)
    {
        GenericMenu menu = new GenericMenu();

        // None
        menu.AddItem(
            new GUIContent("None"),
            property.objectReferenceValue == null,
            () =>
            {
                property.serializedObject.Update();

                property.objectReferenceValue = null;

                property.serializedObject.ApplyModifiedProperties();
            }
        );

        List<MonoScript> scripts = FindScripts(requiredType);

        if (scripts.Count == 0)
        {
            menu.AddDisabledItem(
                new GUIContent("(사용 가능한 스크립트 없음)")
            );
        }
        else
        {
            foreach (MonoScript script in scripts)
            {
                MonoScript capturedScript = script;

                menu.AddItem(
                    new GUIContent(GetMenuPath(capturedScript)),
                    property.objectReferenceValue == capturedScript,
                    () =>
                    {
                        property.serializedObject.Update();

                        property.objectReferenceValue =
                            capturedScript;

                        property.serializedObject.ApplyModifiedProperties();
                    }
                );
            }
        }

        menu.ShowAsContext();
    }

    private List<MonoScript> FindScripts(Type requiredType)
    {
        List<MonoScript> result = new List<MonoScript>();

        string[] guids = AssetDatabase.FindAssets("t:MonoScript");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            MonoScript script =
                AssetDatabase.LoadAssetAtPath<MonoScript>(path);

            if (script == null)
                continue;

            Type type = script.GetClass();

            if (type == null)
                continue;

            if (!requiredType.IsAssignableFrom(type))
                continue;

            if (type.IsAbstract)
                continue;

            result.Add(script);
        }

        result.Sort((a, b) =>
            string.Compare(
                GetMenuPath(a),
                GetMenuPath(b),
                StringComparison.Ordinal
            )
        );

        return result;
    }

    private string GetMenuPath(MonoScript script)
    {
        Type type = script.GetClass();

        return type.Namespace != null
            ? $"{type.Namespace}/{type.Name}"
            : type.Name;
    }

    public override float GetPropertyHeight(
        SerializedProperty property,
        GUIContent label)
    {
        return ButtonHeight;
    }
}
#endif