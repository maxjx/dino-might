using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

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

    // Indicates which quest the player is currently doing.
    public static int questNumber = 0;
    public static bool kingSpared = false;
    public static bool masterSpared = false;
    public static byte[] imageAPath = new byte[]{};
    public static byte[] imageBPath = new byte[]{};
    public static byte[] imageCPath = new byte[]{};

    public static float musicVolume = 0.5f;
    public static float effectsVolume = 0.5f;

    // Summary
    public static List<string> priorities = new List<string>();
    public static List<string> challenges = new List<string>();
    public static List<string> habits = new List<string>();

    // NEW NOT SAVED
    // Key: npc name, value: canvas index
    public static DictStringInt NPCCanvasDict = new DictStringInt();
    // Key: NPC's name, Value: corresponding dialogue index.
    public static DictStringInt NPCDialogueDict = new DictStringInt();
    // All monologues distinguished by their ids that are in this list have been played.
    public static List<string> playedMonologueList = new List<string>();

}
