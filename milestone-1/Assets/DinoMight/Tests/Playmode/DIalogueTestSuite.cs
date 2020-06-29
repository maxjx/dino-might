using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    public class DIalogueTestSuite
    {
        GameObject player;
        GameObject npc;

        [SetUp]
        public void SetUp()
        {
            player = new GameObject();
            player.AddComponent<BoxCollider2D>();
            player.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            player.tag = "Player";
            npc = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/NPC pack/Mask NPC.prefab"));
        }

        [UnityTest]
        public IEnumerator PromptWhenPlayerIsInNPCCollider()
        {
            player.transform.position = Vector3.zero;
            npc.transform.position = Vector3.zero;

            yield return new WaitForSeconds(1f);
            player.transform.position = Vector3.zero;
            npc.transform.position = Vector3.zero;
            Debug.Log(npc.GetComponent<Collider2D>().bounds.Contains(player.transform.position));
            
            Debug.Log(npc.transform.GetChild(0).gameObject.name);

            // player.transform.position = new Vector3(0.865f, 0.427f, 0);
            // yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(npc.transform.GetChild(0).gameObject.activeInHierarchy);
        }

        [UnityTest]
        public IEnumerator NoPromptWhenPlayerIsNotInNPCCollider()
        {
            player.transform.position = Vector3.zero;
            npc.transform.position = new Vector3(100, 100, 100);

            yield return new WaitForSeconds(0.1f);

            Assert.IsFalse(npc.transform.GetChild(0).gameObject.activeInHierarchy);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(player);
            GameObject.Destroy(npc);
        }
    }
}
