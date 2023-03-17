﻿using ChatEpt.Services;

namespace ChatEpt.UnitTests;

public class BadWordCheckerTests
{
    [Test]
    [TestCase("Hello World", false)]
    [TestCase("Hello fucking World", true)]
    [TestCase("Hello asshole World", true)]
    [TestCase("Hello World of bitch", true)]
    public void HasBadWordInText_Test(string text, bool expected)
    {
        var service = new BadWordChecker();

        Assert.Equals(expected, service.HasBadWordInText(text));
    }
}