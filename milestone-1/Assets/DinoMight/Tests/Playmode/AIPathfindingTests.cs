using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using Pathfinding;

namespace Tests
{
    public class AIPathfindingTests
    {
        GameObject mob;
        GameObject pathfinder;
        Path path;

        [SetUp]
        public void SetUp()
        {
            mob = new GameObject();
            mob.AddComponent<Seeker>();
            pathfinder = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DinoMight/Prefabs/Mobs/A_ pathfinder.prefab"));
            pathfinder.transform.position = Vector3.zero;
        }

        [UnityTest]
        public IEnumerator FindStraightPath()
        {
            Transform mobTransform = mob.transform;
            mobTransform.position = Vector3.zero;
            Vector3 target = new Vector3(10f, 0, 0);
            Seeker seeker = mob.GetComponent<Seeker>();
            yield return null;

            seeker.StartPath(mobTransform.position, target, OnPathComplete);

            while (path == null)
            {
                yield return null;
            }

            float displacement = 0f;
            int currentWaypoint = 0;
            Debug.Log(path.vectorPath.Count);
            while (currentWaypoint < path.vectorPath.Count - 1)
            {
                displacement += Vector3.Distance(mobTransform.position, path.vectorPath[currentWaypoint]);
                Debug.Log(displacement);    
                mobTransform.position = path.vectorPath[currentWaypoint];
                currentWaypoint++;
            }

            Assert.AreEqual(10f, displacement);
        }

        void OnPathComplete(Path p)
            {
                if (!p.error)
                {
                    path = p;
                }
            }
    }
}
