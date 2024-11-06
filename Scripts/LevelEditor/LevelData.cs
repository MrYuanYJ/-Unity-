using UnityEngine;

public class LevelData : ScriptableObject
{
    public int LevelID;
    public Vector2Int LevelSize;
    public SingleGridData[] GridData;
}
