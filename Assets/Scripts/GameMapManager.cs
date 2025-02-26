using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static int gameMap;

    public static void decideMap()
    {
        gameMap = Random.Range(0, 2);
    }

    public static int getMapIndex()
    {
        return gameMap;
    }
}
