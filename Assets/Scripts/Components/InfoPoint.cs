
using UnityEngine;

public struct InfoPoint
{
    public bool IsActive;
    public Vector2Int Position;
    public TypeEnemy EnemyType;

    public InfoPoint(int x, int y, TypeEnemy type)
    {
        IsActive = true;
        Position = new Vector2Int(x, y);
        EnemyType = type;
    }
}