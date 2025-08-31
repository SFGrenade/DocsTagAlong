using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace MyFirstCustomSceneMod;

class PrefabHolder
{
    public static GameObject PopAreaTitleCtrlPrefab { get; private set; }
    public static GameObject PopPmU2dPrefab { get; private set; }

    public static void Preloaded(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        PopAreaTitleCtrlPrefab = UObject.Instantiate(preloadedObjects["White_Palace_18"]["Area Title Controller"]);
        SetInactive(PopAreaTitleCtrlPrefab);
        PopPmU2dPrefab = UObject.Instantiate(preloadedObjects["White_Palace_18"]["_Managers/PlayMaker Unity 2D"]);
        SetInactive(PopPmU2dPrefab);
    }

    private static void SetInactive(GameObject go)
    {
        if (go != null)
        {
            UObject.DontDestroyOnLoad(go);
            go.SetActive(false);
        }
    }

    private static void SetInactive(UObject go)
    {
        if (go != null)
        {
            UObject.DontDestroyOnLoad(go);
        }
    }
}