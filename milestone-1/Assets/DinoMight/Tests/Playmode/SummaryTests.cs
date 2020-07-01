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
            Transform summaryCanvasTransform = summaryCanvas.transform;
            bar1 = summaryCanvasTransform.GetChild(3).GetComponent<Slider>();
            bar2 = summaryCanvasTransform.GetChild(4).GetComponent<Slider>();
            bar3 = summaryCanvasTransform.GetChild(5).GetComponent<Slider>();
        }

        [Test]
        public void AssignValueToGlobal()
        {
            Global.priorities = 1;
            Global.challenges = 2;
            Global.habits = 0;

            Assert.AreEqual(1, Global.priorities);
            Assert.AreEqual(2, Global.challenges);
            Assert.AreEqual(0, Global.habits);
        }

        [UnityTest]
        public IEnumerator GetSummaryCategoriesValueFromGlobal()
        {
            Global.priorities = 1;
            Global.challenges = 2;
            Global.habits = 0;
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(1, bar1.value);
            Assert.AreEqual(2, bar2.value);
            Assert.AreEqual(0, bar3.value);
        }

        [UnityTest]
        public IEnumerator GetSummaryCategoriesNormalizedValueFromGlobal()
        {
            Global.priorities = 1;
            Global.challenges = 2;
            Global.habits = 0;
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(0.5f, bar1.normalizedValue);
            Assert.AreEqual(1, bar2.normalizedValue);
            Assert.AreEqual(0, bar3.normalizedValue);
        }

        [UnityTest]
        public IEnumerator CheckTipsGeneratedCorrectly()
        {
            Global.priorities = 1;
            Global.challenges = 2;
            Global.habits = 0;
            yield return new WaitForSeconds(4f);

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