using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class ExtensionMethods {

    public static void SpawnObject(this Transform t, Transform s) {
        t.position = s.position;
        t.rotation = s.rotation;
        t.gameObject.SetActive(true);
    }

    public static void ResetText(this Text t) {
        t.text = string.Empty;
    }
}
