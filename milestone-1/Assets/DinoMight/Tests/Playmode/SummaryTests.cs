using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.UI;

namespace Tests
{
    public class SummaryTests
    {
        private Slider bar1;
        private Slider bar2;
        private Slider bar3;

        [SetUp]
        public void SetUp()
        {
            GameObject summaryCanvas = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Testing prefabs/Summary Canvas.prefab"));
            summaryCanvas.GetComponent<SummaryCanvas>().enabled = false;
            foreach (Transform child in summaryCanvas.transform)
            {
                child.gameObject.SetActive(true);
            }
            Transform summaryCanvasTransform = summaryCanvas.transform;
            bar1 = summaryCanvasTransform.GetChild(3).GetComponent<Slider>();
            bar2 = summaryCanvasTransform.GetChild(4).GetComponent<Slider>();
            bar3 = summaryCanvasTransform.GetChild(5).GetComponent<Slider>();
        }

        [Test]
        public void AssignValueToGlobal()
        {
            Global.priorities = new HashSet<string>(){ "" };
            Global.challenges = new HashSet<string>(){ "", " " };
            Global.habits = new HashSet<string>();

            Assert.AreEqual(1, Global.priorities.Count);
            Assert.AreEqual(2, Global.challenges.Count);
            Assert.AreEqual(0, Global.habits.Count);
        }

        [UnityTest]
        public IEnumerator GetSummaryCategoriesValueFromGlobal()
        {
            Global.priorities = new HashSet<string>(){ "" };
            Global.challenges = new HashSet<string>(){ "", " " };
            Global.habits = new HashSet<string>();
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(1, bar1.value);
            Assert.AreEqual(2, bar2.value);
            Assert.AreEqual(0, bar3.value);
        }

        [UnityTest]
        public IEnumerator GetSummaryCategoriesNormalizedValueFromGlobal()
        {
            Global.priorities = new HashSet<string>(){ "" };
            Global.challenges = new HashSet<string>(){ "", " " };
            Global.habits = new HashSet<string>();
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(0.5f, bar1.normalizedValue);
            Assert.AreEqual(1, bar2.normalizedValue);
            Assert.AreEqual(0, bar3.normalizedValue);
        }

        [UnityTest]
        public IEnumerator CheckTipsGeneratedCorrectly()
        {
            Global.priorities = new HashSet<string>(){ "" };
            Global.challenges = new HashSet<string>(){ "", " " };
            Global.habits = new HashSet<string>();
            yield return new WaitForSeconds(2f);

            // Manual visual check i guess
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(bar1);
            GameObject.Destroy(bar2);
            GameObject.Destroy(bar3);
        }
    }
}