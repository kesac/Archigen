using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archigen.Tests;

[TestClass]
public class WeightedSelectorTests
{
    public class WeightedFruit : IWeighted
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public WeightedFruit(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }

    [TestMethod]
    public void WeightedSelector_NoItems_ThrowsException()
    {
        var sut = new WeightedSelector<string>();
        Assert.ThrowsException<InvalidOperationException>(() => sut.Next());
    }

    [TestMethod]
    [DataRow("Apple")]
    public void WeightedSelector_AddOneItemThroughConstructor_ReturnsItem(string item)
    {
        var sut = new WeightedSelector<string>(item);
        Assert.AreEqual(item, sut.Next());
    }

    [TestMethod]
    [DataRow("Apple")]
    public void WeightedSelector_AddOneItemThroughAddMethod_ReturnsItem(string item)
    {
        var sut = new WeightedSelector<string>();
        sut.Add(item, 1);

        Assert.AreEqual(item, sut.Next());
    }

    [TestMethod]
    public void WeightedSelector_AddIWeightedItemsThroughConstructor_ReturnsItems()
    {
        var values = new List<WeightedFruit>
        {
            new WeightedFruit("Apple", 1),
            new WeightedFruit("Orange", 2),
            new WeightedFruit("Grapes", 3)
        };

        var sut = new WeightedSelector<WeightedFruit>(values);
        var occurrences = new Dictionary<string, int>();

        for (int i = 0; i < 1000; i++)
        {
            var result = sut.Next();
            if (occurrences.ContainsKey(result.Name))
            {
                occurrences[result.Name]++;
            }
            else
            {
                occurrences[result.Name] = 1;
            }
        }

        var appleCount = occurrences["Apple"];
        var orangeCount = occurrences["Orange"];
        var grapesCount = occurrences["Grapes"];

        Assert.IsTrue(appleCount < orangeCount);
        Assert.IsTrue(orangeCount < grapesCount);

    }

    [TestMethod]
    public void WeightedSelector_AddIWeightedItemsThroughAddMethod_ReturnsItems()
    {
        var sut = new WeightedSelector<WeightedFruit>();
        sut.Add(new WeightedFruit("Apple", 1));
        sut.Add(new WeightedFruit("Orange", 2));
        sut.Add(new WeightedFruit("Grapes", 3));

        var occurrences = new Dictionary<string, int>();

        for (int i = 0; i < 1000; i++)
        {
            var result = sut.Next();
            if (occurrences.ContainsKey(result.Name))
            {
                occurrences[result.Name]++;
            }
            else
            {
                occurrences[result.Name] = 1;
            }
        }

        var appleCount = occurrences["Apple"];
        var orangeCount = occurrences["Orange"];
        var grapesCount = occurrences["Grapes"];

        Assert.IsTrue(appleCount < orangeCount);
        Assert.IsTrue(orangeCount < grapesCount);

    }

    [TestMethod]
    public void WeightedSelector_AddMultipleItemsThroughAddMethod_ReturnsItems()
    {
        var sut = new WeightedSelector<string>();
        sut.Add("Apple", 1);
        sut.Add("Orange", 2);
        sut.Add("Grapes", 3);

        var occurrences = new Dictionary<string, int>();

        for (int i = 0; i < 1000; i++)
        {
            var result = sut.Next();
            if (occurrences.ContainsKey(result))
            {
                occurrences[result]++;
            }
            else
            {
                occurrences[result] = 1;
            }
        }

        var appleCount = occurrences["Apple"];
        var orangeCount = occurrences["Orange"];
        var grapesCount = occurrences["Grapes"];

        Assert.IsTrue(appleCount < orangeCount);
        Assert.IsTrue(orangeCount < grapesCount);

    }

}
