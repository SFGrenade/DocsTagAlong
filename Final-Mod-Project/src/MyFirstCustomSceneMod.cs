using System;
using Modding;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SFCore.Generics;
using SFCore.Utils;
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

    private bool OnGetPlayerBoolHook(string target, bool orig)
    {
        var tmpField = ReflectionHelper.GetFieldInfo(typeof(SettingsClass), target);
        if (tmpField != null)
        {
            return (bool)tmpField.GetValue(SaveSettings);
        }

        return orig;
    }

    private bool OnSetPlayerBoolHook(string target, bool orig)
    {
        var tmpField = ReflectionHelper.GetFieldInfo(typeof(SettingsClass), target);
        if (tmpField != null)
        {
            tmpField.SetValue(SaveSettings, orig);
        }

        return orig;
    }

    private string OnLanguageGetHook(string key, string sheet, string orig)
    {
        // this hook can be trivialized using `SFCore.Utils.LanguageStrings`
        if (sheet == "Titles" && key == "MyFirstCustomSceneMod_AreaTitle_SUPER")
        {
            return "The";
        }

        if (sheet == "Titles" && key == "MyFirstCustomSceneMod_AreaTitle_MAIN")
        {
            return "Lands";
        }

        if (sheet == "Titles" && key == "MyFirstCustomSceneMod_AreaTitle_SUB")
        {
            return "Between";
        }

        return orig;
    }

    private void OnSceneChanged(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
    {
        if (to.name == "Town")
        {
            // we arrived in Dirtmouth
            // get the transition point in the well
            var tp1 = to.Find("bot1").GetComponent<TransitionPoint>();
            // we want to get to our custom scene
            tp1.targetScene = "MyFirstCustomSceneMod";
            // we'll enter our custom scene from the left
            tp1.entryPoint = "left1";
        }
        else if (to.name == "Crossroads_01")
        {
            // we arrived in the Forgotten Crossroads
            // get the transition point in the well
            var tp1 = to.Find("top2").GetComponent<TransitionPoint>();
            // we want to get to our custom scene
            tp1.targetScene = "MyFirstCustomSceneMod";
            // we'll enter our custom scene from the right
            tp1.entryPoint = "right1";

            var tp2Go = to.Find("door1");
            // get the transition point at the bottom
            var tp2 = tp2Go.GetComponent<TransitionPoint>();
            // we want to get to our custom scene
            tp2.targetScene = "MyFirstCustomSceneMod";
            // we'll enter our custom scene from the right
            tp2.entryPoint = "right1";
            var tp2Fsm = tp2Go.LocateMyFSM("Door Control");
            tp2Fsm.GetStringVariable("New Scene").Value = tp2.targetScene;
            tp2Fsm.GetStringVariable("Entry Gate").Value = tp2.entryPoint;
        }
    }
}