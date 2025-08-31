using UnityEngine;

namespace MyFirstCustomSceneMod;

class PatchPlayMakerManager : MonoBehaviour
{
    public Transform ManagerTransform;

    public void Awake()
    {
        GameObject tmpPmu2D = Instantiate(PrefabHolder.PopPmU2dPrefab, ManagerTransform);
        tmpPmu2D.SetActive(true);
        tmpPmu2D.name = "PlayMaker Unity 2D";
    }
}