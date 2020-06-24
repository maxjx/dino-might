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
        private GameObject mob;
        private PlayerHealth playerHealth;

        [SetUp]
        public void Setup()
        {
            player = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Character (pf).prefab"));
            playerHealth = player.GetComponent<PlayerHealth>();

            mob = new GameObject();
            ProximityAttack pa = mob.AddComponent<ProximityAttack>();
            pa.player = player.GetComponent<Transform>();
        }

        [Test]
        public void PlayerGetsHurtByProximityAttackManually()
        {
            int beforeHealth = playerHealth.currentHealth;
            mob.GetComponent<ProximityAttack>().Hurt();
            int afterHealth = playerHealth.currentHealth;

            Assert.AreEqual(beforeHealth, afterHealth - 1);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(player);
            GameObject.Destroy(mob);
        }
    }
}
