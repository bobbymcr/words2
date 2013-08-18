//-----------------------------------------------------------------------
// <copyright file="WordTest.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class WordTest
    {
        public WordTest()
        {
        }

        [Fact]
        public void FromString_NormalizesToUppercase()
        {
            Word a1 = new Word("abc");
            Word a2 = new Word("abC");
            Word a3 = new Word("ABC");

            Assert.Equal("ABC", a1.ToString());
            Assert.Equal("ABC", a2.ToString());
            Assert.Equal("ABC", a3.ToString());
        }

        [Fact]
        public void FromString_NullWord_ThrowsArgumentNull()
        {
            string word = null;
            Exception e = Record.Exception(() => new Word(word));

            Assert.NotNull(e);
            ArgumentNullException ane = Assert.IsType<ArgumentNullException>(e);
            Assert.Equal("word", ane.ParamName);
        }

        [Fact]
        public void FromString_EmptyWord_ThrowsArgument()
        {
            string word = string.Empty;
            Exception e = Record.Exception(() => new Word(word));

            Assert.NotNull(e);
            ArgumentException ane = Assert.IsType<ArgumentException>(e);
            Assert.Equal("word", ane.ParamName);
        }

        [Fact]
        public void FromString_TooLong_ThrowsArgumentOutOfRange()
        {
            string word = "aaaabbbbccccdddd";
            Exception e = Record.Exception(() => new Word(word));

            Assert.NotNull(e);
            ArgumentOutOfRangeException aoore = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("word", aoore.ParamName);
            Assert.Contains(" 15 ", aoore.Message);
        }

        [Fact]
        public void ForEach_OneChar_RunsForEachChar()
        {
            ForEachInnerTest("a", new char[] { 'A' });
        }

        [Fact]
        public void ForEach_TwoChars_RunsForEachChar()
        {
            ForEachInnerTest("aB", new char[] { 'A', 'B' });
        }

        [Fact]
        public void ForEach_ThreeChars_RunsForEachChar()
        {
            ForEachInnerTest("aBc", new char[] { 'A', 'B', 'C' });
        }

        [Fact]
        public void Equals_ComparesCorrectly()
        {
            Word a1 = new Word("abc");
            Word a2 = new Word("abC");
            Word b1 = new Word("bCd");
            Word b2 = new Word("Bcd");

            Assert.True(a1.Equals(a2));
            Assert.True(a2.Equals(a1));
            Assert.False(a1.Equals(b2));
            Assert.True(b1.Equals(b2));
            Assert.True(b2.Equals(b1));
            Assert.False(a1.Equals(null));
            Assert.False(a1.Equals("ABC"));
        }

        [Fact]
        public void GetHashCode_ReturnsValidCode()
        {
            Word a1 = new Word("abc");
            Word a2 = new Word("abC");
            Word b = new Word("bcd");

            int ca1 = a1.GetHashCode();
            int ca2 = a2.GetHashCode();
            int cb = b.GetHashCode();

            Assert.Equal(ca1, ca2);
            Assert.NotEqual(ca1, cb);
        }

        private static void ForEachInnerTest(string word, char[] expected)
        {
            Word w = new Word(word);
            List<char> items = new List<char>();

            foreach (char c in w)
            {
                items.Add(c);
            }

            Assert.Equal(expected, items.ToArray());
        }
    }
}
