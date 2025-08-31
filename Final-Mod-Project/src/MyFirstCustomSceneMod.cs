using Modding;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstCustomSceneMod;

/// <summary>
/// Base class.
/// </summary>
public class MyFirstCustomSceneMod : Mod
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public MyFirstCustomSceneMod() : base("MyFirstCustomSceneMod")
    {
        // some code maybe here
    }

    /// <summary>
    /// Displays the version.
    /// </summary>
    public override string GetVersion() => "0.0.0.0";

    /// <summary>
    /// Main menu is loaded.
    /// </summary>
    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        // some code maybe here
    }
}