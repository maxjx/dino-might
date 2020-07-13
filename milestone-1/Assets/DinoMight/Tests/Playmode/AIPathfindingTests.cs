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
            mob.transform.position = Vector3.zero;
            Vector3 target = new Vector3(10f, 0, 0);
            Seeker seeker = mob.GetComponent<Seeker>();
            yield return null;

            seeker.StartPath(mob.transform.position, target, OnPathComplete);

            while (path == null)
            {
                yield return null;
            }

            float displacement = 0f;
            int currentWaypoint = 0;
            float nextWaypointDistance = 0.00001f;

            while (mob.transform.position.x != target.x)
            {
                if (Vector3.Distance(mob.transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
                {
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    displacement += Vector3.Distance(path.vectorPath[currentWaypoint], mob.transform.position);
                    mob.transform.position = path.vectorPath[currentWaypoint];
                }
            }
            Assert.Greater(displacement, 10f);
        }

        [UnityTest]
        public IEnumerator FindNonStraightPath()
        {
            GameObject newGO = new GameObject();
            newGO.AddComponent<BoxCollider2D>();
            newGO.transform.position = new Vector3(5f, 0, 0);

            mob.transform.position = Vector3.zero;
            Vector3 target = new Vector3(10f, 0, 0);
            Seeker seeker = mob.GetComponent<Seeker>();
            yield return null;

            seeker.StartPath(mob.transform.position, target, OnPathComplete);

            while (path == null)
            {
                yield return null;
            }

            float displacement = 0f;
            int currentWaypoint = 0;
            float nextWaypointDistance = 0.00001f;

            while (mob.transform.position.x != target.x)
            {
                if (Vector3.Distance(mob.transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
                {
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    displacement += Vector3.Distance(path.vectorPath[currentWaypoint], mob.transform.position);
                    mob.transform.position = path.vectorPath[currentWaypoint];
                }
            }
            Assert.Greater(displacement, 10.05f);
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
