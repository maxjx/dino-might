using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {
    public static int playerId;
    public static int playerHealth;
    public static int playerLevel;
    public static float Xcoordinate;
    public static float Ycoordinate;
    public static bool isLoad;
    public static Dictionary<string, int> NPCDialogueDict;      // Key: NPC's name, Value: corresponding dialogue id
}
