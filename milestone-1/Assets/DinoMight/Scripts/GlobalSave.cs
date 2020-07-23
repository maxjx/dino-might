using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GlobalSave {
    public List<string> priorities = Global.priorities;
    public List<string> challenges = Global.challenges;
    public List<string> habits = Global.habits;
    public List<string> playedMonologueList = Global.playedMonologueList;
}
