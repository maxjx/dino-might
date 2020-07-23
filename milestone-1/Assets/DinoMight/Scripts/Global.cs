﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {
    public static int playerId;

    // player stats
    public static int playerHealth;
    public static int playerMaxHealth = 15;     // to save
    public static int kickDmg = 2;              // to save
    public static int fireballDmg = 3;          // to save
    public static bool canDash = false;         // to save

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
    public static int questNumber = 0;                  // to save
    public static bool kingSpared = false;              // to save
    public static bool masterSpared = false;            // to save
    public static byte[] imageAPath = new byte[]{};
    public static byte[] imageBPath = new byte[]{};
    public static byte[] imageCPath = new byte[]{};

    public static float musicVolume = 0.5f;
    public static float effectsVolume = 0.5f;

    // Summary
    public static List<string> priorities = new List<string>();         // to save
    public static List<string> challenges = new List<string>();         // to save
    public static List<string> habits = new List<string>();         // to save

    // NEW
    // Key: npc name, value: canvas index
    public static Dictionary<string, int> NPCCanvasDict = new Dictionary<string, int>();

}
