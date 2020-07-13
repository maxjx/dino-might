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
    public static bool isLoggedIn = false;
    // Key: NPC's name, Value: corresponding dialogue index.
    public static Dictionary<string, int> NPCDialogueDict = new Dictionary<string, int>();
    // All monologues distinguished by their ids that are in this list have been played.
    public static List<string> playedMonologueList = new List<string>();
    // Indicates which quest the player is currently doing.
    public static int questNumber;

    // Summary
    public static HashSet<string> priorities;
    public static HashSet<string> challenges;
    public static HashSet<string> habits;

}
