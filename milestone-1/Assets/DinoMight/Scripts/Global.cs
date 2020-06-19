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
    // Key: NPC's name, Value: corresponding dialogue id
    public static Dictionary<string, int> NPCDialogueDict = new Dictionary<string, int>();
    // All monologues distinguished by their ids that are in this list have been played
    public static List<string> playedMonologueList = new List<string>();
}
