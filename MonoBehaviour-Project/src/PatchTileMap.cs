using UnityEngine;

namespace MyFirstCustomSceneMod;

public class PatchTileMap : MonoBehaviour
{
    public int width;
    public int height;
    public int columns;
    public int rows;
    public int partSizeX;
    public int partSizeY;
    public PhysicsMaterial2D physicsMaterial2D;
    public GameObject renderData;
}