using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabBatch {

    [MenuItem("Tools/Delete Script")]
    private static void DeleteScript() {
        string path = string.Format("{0}/{1}", Application.dataPath, "Prefab/");
        string[] paths = Directory.GetFiles(path, "*.prefab", SearchOption.AllDirectories);
        for (int i = 0; i < paths.Length; i++) {
            string abPath = paths[i].Substring(paths[i].IndexOf("Assets"));
            GameObject prefab = AssetDatabase.LoadAssetAtPath(abPath, typeof(GameObject)) as GameObject;
            Debug.Log("root: " + prefab.name);
            GameObject gameObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            TestComponent[] tc = gameObj.GetComponentsInChildren<TestComponent>();
            for (int j = 0; j < tc.Length; j++) {
                Debug.Log("child :" + tc[j].name);
                GameObject.DestroyImmediate(tc[j]);
            }
            PrefabUtility.ReplacePrefab(gameObj, prefab, ReplacePrefabOptions.Default);
            EditorUtility.SetDirty(prefab);
            GameObject.DestroyImmediate(gameObj);
        }
        AssetDatabase.SaveAssets();


    }
}
