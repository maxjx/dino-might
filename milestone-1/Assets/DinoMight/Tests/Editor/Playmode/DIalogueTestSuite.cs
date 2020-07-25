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
        DialogueManager dm;

        [SetUp]
        public void SetUp()
        {
            // player = new GameObject();
            // player.transform.position = Vector3.zero;
            // player.AddComponent<BoxCollider2D>();
            // player.GetComponent<BoxCollider2D>().size = new Vector2(1, 2);
            // player.tag = "Player";
            player = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Character (pf).prefab"));
            npc = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/NPC pack/Kiddo NPC.prefab"));
            dm = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/NPC pack/DialogueManager.prefab")).GetComponent<DialogueManager>();
            dm.player = player;
            dm.dialogueCanvasesTaskValue = new List<int>() { 2, 4 };
            dm.recordConvo = false;
        }

        [Test]
        public void PlayerInsideNPC()
        {
            player.transform.position = Vector3.zero;
            npc.transform.position = Vector3.zero;

            Assert.AreEqual(player.transform.position, npc.transform.position);
        }

        [Test]
        public void CorrectCanvasWhenQuestIsLessThanValues()
        {
            Global.questNumber = 1;
            Assert.AreEqual(0, dm.GetCanvasIndex());
        }

        [Test]
        public void CorrectCanvasWhenQuestIsEqualValue()
        {
            Global.questNumber = 2;
            Assert.AreEqual(0, dm.GetCanvasIndex());
        }
        
        [Test]
        public void CorrectCanvasWhenQuestIsInBetweenValues()
        {
            Global.questNumber = 3;
            Assert.AreEqual(1, dm.GetCanvasIndex());
        }

        [Test]
        public void CorrectCanvasWhenQuestIsMoreThanValues()
        {
            Global.questNumber = 5;
            Assert.AreEqual(1, dm.GetCanvasIndex());
        }

        [UnityTest]
        public IEnumerator PromptWhenPlayerIsInNPCCollider()
        {
            player.transform.position = Vector3.zero;
            npc.transform.position = Vector3.zero;
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(npc.transform.GetChild(0).gameObject.activeInHierarchy);
        }
        
        // [UnityTest]
        // public IEnumerator NoPromptWhenPlayerIsInNPCColliderAndCPressed()
        // {
        //     player.transform.position = Vector3.zero;
        //     npc.transform.position = Vector3.zero;
            
        //     yield return new WaitForSeconds(0.1f);
        //     npc.GetComponent<DialogueTrigger>().TriggerKeysPressed().Returns(true);
        //     yield return new WaitForSeconds(0.1f);

        //     Assert.IsFalse(npc.transform.GetChild(0).gameObject.activeInHierarchy);
        // }

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
            GameObject.Destroy(dm);
        }
    }
}
