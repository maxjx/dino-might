using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    public class DamageTests
    {
        private GameObject player;

        [SetUp]
        public void Setup()
        {
            player = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Character (pf).prefab"));

        }

        [UnityTest]
        public IEnumerator Testmob()
        {
            GameObject mob = new GameObject();
            mob.AddComponent<ProximityAttack>();
            yield return null;
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(player);
            //GameObject.Destroy(mob);
        }
    }
}
