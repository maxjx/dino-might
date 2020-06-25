using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KingSummoner : MonoBehaviour
{
    [SerializeField] protected Transform prefab;

    public abstract void Summon();
}
