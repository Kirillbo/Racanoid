using Tools;
using Random = UnityEngine.Random;

public class SystemGenerateMap : IAwake
{
        
    public void OnAwake()
    {
        
        InfoPoint [,] _generateMap = new InfoPoint[10,5];
        
        for (int posX = 0; posX < _generateMap.GetLength(0)/2; posX++)
        {
            for (int posY = 0; posY < _generateMap.GetLength(1); posY++)
            {
                
                
                if(!ToolsRandom.Choice(0.5f)) continue;

                var index = Random.Range(0, 3f);
                _generateMap[posX, posY] = new InfoPoint(posX,posY, (TypeEnemy)index);
                
                
            } 
        }

        var newMap = Reflection(_generateMap);
        PoolManager.Instance.AddComponent<ComponentMap>().Map = newMap;
    }

    
    

    public InfoPoint[,] Reflection(InfoPoint[,] map)
    {
        var myMap = map;
        int centerLine = (myMap.GetLength(0) / 2) - 1;
        int lastblock = myMap.GetLength(0) - 1;
        
        for (int posX = lastblock; posX > centerLine; posX--)
        {
            for (int posY = 0; posY < myMap.GetLength(1); posY++)
            {
                
                
                var mirrorBlock = myMap[lastblock - posX, posY];
                if(!mirrorBlock.IsActive) continue;

                myMap[posX, posY] = new InfoPoint(posX, posY, mirrorBlock.EnemyType);

                
            } 
        }
        return myMap;
    }
}



public enum TypeEnemy
{
    Simple,
    Medium,
    Hard
}
