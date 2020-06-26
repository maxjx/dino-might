using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    public class MovingPlatform
    {
        private GameObject movingPlatform;

        [SetUp]
        public void SetUp() {
            movingPlatform = GameObject.Instantiate(AssetDatabase.
                LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Tiles/Moving Tile.prefab"));
        }

        [UnityTest]
        public IEnumerator PlatformExists() {
            Assert.NotNull(movingPlatform.GetComponent<Transform>());
            yield return null;
        }

        [UnityTest]
        public IEnumerator MovingPlatformNotStationary()
        {
            Vector3 initPos = movingPlatform.transform.position;
            yield return new WaitForSeconds(0.1f);

            Assert.AreNotEqual(initPos, movingPlatform.transform.position);
            yield return null;
        }

        [UnityTest]
        public IEnumerator MovingPlatformIsMoving() {
            Vector3 initPos = movingPlatform.transform.position;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(initPos, movingPlatform.transform.position);
            yield return null;
        }

        [TearDown]
        public void TearDown() {
            Object.Destroy(movingPlatform);
        }
    }
}
