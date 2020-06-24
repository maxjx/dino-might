using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class HealthTests
    {
        [Test]
        public void DamageHealth()
        {
            int startHealth = 5;
            int newHealth = Health.MinusHealth(startHealth, 2);

            Assert.AreEqual(3, newHealth);
        }

        [Test]
        public void DoesNotOverKillHealth()
        {
            int startHealth = 5;
            int newHealth = Health.MinusHealth(startHealth, 6);

            Assert.AreEqual(0, newHealth);
        }

        [Test]
        public void HealHealth()
        {
            int startHealth = 5;
            int maxHealth = 10;
            int newHealth = Health.AddHealth(maxHealth, startHealth, 2);

            Assert.AreEqual(7, newHealth);
        }

        [Test]
        public void DoesNotOverHealHealth()
        {
            int startHealth = 5;
            int maxHealth = 10;
            int newHealth = Health.AddHealth(maxHealth, startHealth, 6);

            Assert.AreEqual(10, newHealth);
        }
    }
}
