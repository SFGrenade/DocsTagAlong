using System;
using Modding;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SFCore.Generics;
using UnityEngine;

namespace MyFirstCustomSceneMod;

/// <summary>
/// Base class.
/// </summary>
public class MyFirstCustomSceneMod : SaveSettingsMod<SettingsClass>
{
    private AssetBundle _abScenes = null;

    private void LoadAssetBundles()
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        if (_abScenes == null)
        {
            using Stream s = asm.GetManifestResourceStream("MyFirstCustomSceneMod.Resources.my_first_assetbundle");
            if (s != null)
            {
                _abScenes = AssetBundle.LoadFromStream(s);
            }
        }
    }

    public MyFirstCustomSceneMod() : base("My First Custom Scene Mod")
    {
        LoadAssetbundles();

        InitCallbacks();
    }

    public override List<ValueTuple<string, string>> GetPreloadNames()
    {
        return new List<ValueTuple<string, string>>
        {
            // we will populate this later
        };
    }

    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        // this will hold on to any preloads for other components to use
        PrefabHolder.Preloaded(preloadedObjects);
    }

    private void InitCallbacks()
    {
        ModHooks.GetPlayerBoolHook += OnGetPlayerBoolHook;
        ModHooks.SetPlayerBoolHook += OnSetPlayerBoolHook;
        ModHooks.LanguageGetHook += OnLanguageGetHook;
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
    }
}