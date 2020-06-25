using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSummonFactory : MonoBehaviour
{
    public void Summon(string type) {
        switch (type) {
            case "chicken":
                gameObject.GetComponent<KingSummonerMob>().Summon();
                break;
            case "fist":
                gameObject.GetComponent<KingSummonFist>().Summon();
                break;
        }
    }
}
