using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Archigen.Tests;

[TestClass]
public class RandomSelectorTests
{
    [TestMethod]
    public void RandomSelector_NoItems_ThrowsException()
    {
        var sut = new RandomSelector<int>();
        Assert.ThrowsException<InvalidOperationException>(() => sut.Next());
    }

    [TestMethod]
    public void RandomSelector_AddOneItemThroughConstructor_ReturnsItem()
    {
        var sut = new RandomSelector<int>(1);
        Assert.AreEqual(1, sut.Next());
    }

    [TestMethod]
    public void RandomSelector_AddOneItemThroughAddMethod_ReturnsItem()
    {
        var sut = new RandomSelector<int>();
        sut.Add(1);

        Assert.AreEqual(1, sut.Next());
    }

    [TestMethod]
    public void RandomSelector_AddMultipleThroughConstructor_ReturnsItem()
    {
        var sut = new RandomSelector<int>(1, 2, 3);

        var detectedValues = new HashSet<int>();

        for (int i = 0; i < 1000; i++)
        {
            var result = sut.Next();
            detectedValues.Add(result);
        }

        Assert.IsTrue(detectedValues.Contains(1));
        Assert.IsTrue(detectedValues.Contains(2));
        Assert.IsTrue(detectedValues.Contains(3));
    }

    [TestMethod]
    public void RandomSelector_AddMultipleThroughAddMethod_ReturnsItem()
    {
        var sut = new RandomSelector<int>();
        sut.Add(1, 2);
        sut.Add(3);

        var detectedValues = new HashSet<int>();

        for (int i = 0; i < 1000; i++)
        {
            var result = sut.Next();
            detectedValues.Add(result);
        }

        Assert.IsTrue(detectedValues.Contains(1));
        Assert.IsTrue(detectedValues.Contains(2));
        Assert.IsTrue(detectedValues.Contains(3));
    }
}
