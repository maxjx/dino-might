using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NSubstitute;

namespace Tests
{
    public class DamageTests
    {
        private GameObject mob;
        private GameObject player;
        private GameObject hb;
        private GameObject ob;

        [SetUp]
        public void Setup()
        {
            mob = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Mobs/Dumbass chicken.prefab"));
            hb = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Testing prefabs/HealthBar.prefab"));
            player = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Character (pf).prefab"));
            ob = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Standard level stuff/ObjectPooler.prefab"));

            player.GetComponent<PlayerHealth>().healthBar = hb.GetComponent<HealthBar>();
        }

        [UnityTest]
        public IEnumerator PlayerGetsHurtByProximityAttack()
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            int beforeHealth = playerHealth.currentHealth;
            player.transform.position = Vector3.zero;
            mob.transform.position = Vector3.zero;

            yield return new WaitForSeconds(0.01f);
            int afterHealth = playerHealth.currentHealth;

            Assert.AreEqual(beforeHealth, afterHealth + 1);
        }

        [UnityTest]
        public IEnumerator PlayerDoesNotGetHurtByProximityAttackFarAway()
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            int beforeHealth = playerHealth.currentHealth;
            player.transform.position = Vector3.zero;
            mob.transform.position = new Vector3(100f, 0, 0);

            yield return new WaitForSeconds(0.01f);
            int afterHealth = playerHealth.currentHealth;

            Assert.AreEqual(beforeHealth, afterHealth);
        }

        [UnityTest]
        public IEnumerator MobGetsHurtByKick()
        {
            player.transform.position = Vector3.zero;
            mob.transform.position = new Vector3(0.3f, 0, 0);

            yield return new WaitForSeconds(0.01f);

            player.GetComponent<Attack>().UseKickAttack();

            yield return new WaitForSeconds(0.01f);

            Assert.AreEqual(mob.GetComponent<MobHealth>().currentHealth, 0);
        }

        [UnityTest]
        public IEnumerator MobDoesNotGetHurtByKickFarAway()
        {
            player.transform.position = Vector3.zero;
            mob.transform.position = new Vector3(100f, 0, 0);

            yield return new WaitForSeconds(0.01f);

            player.GetComponent<Attack>().UseKickAttack();

            yield return new WaitForSeconds(0.01f);

            Assert.AreEqual(mob.GetComponent<MobHealth>().currentHealth, 2);
        }

        [UnityTest]
        public IEnumerator MobGetsHurtByFireball()
        {
            player.transform.position = Vector3.zero;
            mob.transform.position = new Vector3(1f, 0, 0);

            yield return new WaitForSeconds(0.01f);

            player.GetComponent<Attack>().UseFireball();

            yield return new WaitForSeconds(0.05f);

            Assert.AreEqual(mob.GetComponent<MobHealth>().currentHealth, 0);
        }

        [UnityTest]
        public IEnumerator MobDoesNotGetHurtByFireballFarAway()
        {
            player.transform.position = Vector3.zero;
            mob.transform.position = new Vector3(100f, 0, 0);

            yield return new WaitForSeconds(0.01f);

            player.GetComponent<Attack>().UseFireball();

            yield return new WaitForSeconds(0.05f);

            Assert.AreEqual(mob.GetComponent<MobHealth>().currentHealth, 2);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(player);
            GameObject.Destroy(mob);
            GameObject.Destroy(hb);
            GameObject.Destroy(ob);
        }
    }
}
