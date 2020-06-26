using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovingPlatform
    {
        private GameObject movingPlatform;

        [SetUp]
        public void SetUp() {
            movingPlatform = MonoBehaviour.Instantiate(
                    Resources.Load<GameObject>("Assets/DinoMight/Prefabs/Tiles/Moving Tiles"));
        }

        [UnityTest]
        public IEnumerator PlatformExists() {
            Assert.NotNull(movingPlatform.GetComponent<Rigidbody2D>());
        }

        [UnityTest]
        public IEnumerator MovingPlatformNotStationary()
        {
            Vector3 initPos = movingPlatform.transform.position;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(initPos, movingPlatform.transform.position);
        }

        [TearDown]
        public void TearDown() {
            Object.Destroy(movingPlatform);
        }
    }
}
