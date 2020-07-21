using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {
    public static int playerId;

    // player stats
    public static int playerHealth;
    public static int playerMaxHealth = 15;
    public static int kickDmg = 2;
    public static int fireballDmg = 3;
    public static bool canDash = false;

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
    public static bool kingSpared = false;
    public static bool masterSpared = false;
    public static string imageAPath;            // in the form of "imageA", can be obtained by using Resources.Load(imageAPath)
    public static string imageBPath;            // in the form of "imageB"
    public static string imageCPath;            // in the form of "imageC"

    public static float musicVolume = 0.5f;
    public static float effectsVolume = 0.5f;

    // Summary
    public static HashSet<string> priorities = new HashSet<string>();
    public static HashSet<string> challenges = new HashSet<string>();
    public static HashSet<string> habits = new HashSet<string>();

}
