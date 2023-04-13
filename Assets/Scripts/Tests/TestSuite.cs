using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestSuite
{

    public void TestSuiteSimplePasses()
    {
        Assert.AreEqual(1,1);

    }

    [UnityTest]
    public IEnumerator BeginMoveRight()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Snake>();
        yield return new WaitForSeconds(.1f);
        Assert.Greater(player.transform.position.x,0);

    }

    [UnityTest]
    public IEnumerator FoodRandomizePosition()
    {
        var gameObject = new GameObject();
        var food = gameObject.AddComponent<Food>();
        yield return new WaitForSeconds(.1f);
        Assert.AreNotSame(0, food.transform.position);

    }
}
