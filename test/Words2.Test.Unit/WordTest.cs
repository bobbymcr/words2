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
            Word a2 = new Word("abCaBc");
            Word a3 = new Word("ABCabCABc");
            Word a4 = new Word("ABCabCABcabC");
            Word a5 = new Word("ABCabCABcabCaBC");

            Assert.Equal("ABC", a1.ToString());
            Assert.Equal("ABCABC", a2.ToString());
            Assert.Equal("ABCABCABC", a3.ToString());
            Assert.Equal("ABCABCABCABC", a4.ToString());
            Assert.Equal("ABCABCABCABCABC", a5.ToString());
        }

        [Fact]
        public void FromString_Digit_ThrowsArgument()
        {
            FromStringNonAlphaInnerTest("ab1cdefghijk", '1');
        }

        [Fact]
        public void FromString_Space_ThrowsArgument()
        {
            FromStringNonAlphaInnerTest(" lmnopqrstuv", ' ');
        }

        [Fact]
        public void FromString_Punctuation_ThrowsArgument()
        {
            FromStringNonAlphaInnerTest("w.xyz", '.');
        }

        [Fact]
        public void FromString_NotAToZ_ThrowsArgument()
        {
            FromStringNonAlphaInnerTest("abcdé", 'é');
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
            Assert.Contains(" 15 ", aoore.Message, StringComparison.Ordinal);
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
        public void CompareTo_ComparesCorrectly()
        {
            Word a = new Word("a");
            Word aa = new Word("aa");
            Word aaa = new Word("aaa");
            Word aab = new Word("aab");
            Word abb = new Word("abb");
            Word bbb = new Word("bbb");
            Word bcd = new Word("bcd");

            Assert.True(a.CompareTo(aa) < 0);
            Assert.True(aa.CompareTo(a) > 0);
            Assert.True(aa.CompareTo(aaa) < 0);
            Assert.True(aaa.CompareTo(aab) < 0);
            Assert.True(aab.CompareTo(aaa) > 0);
            Assert.True(aab.CompareTo(abb) < 0);
            Assert.True(abb.CompareTo(aab) > 0);
            Assert.True(bbb.CompareTo(aaa) > 0);
            Assert.True(bbb.CompareTo(abb) > 0);
            Assert.True(bbb.CompareTo(bcd) < 0);
            Assert.True(bcd.CompareTo(bbb) > 0);
            Assert.True(bcd.CompareTo(a) > 0);
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

        private static void FromStringNonAlphaInnerTest(string word, char expected)
        {
            Exception e = Record.Exception(() => new Word(word));

            Assert.NotNull(e);
            ArgumentException ane = Assert.IsType<ArgumentException>(e);
            Assert.Equal("word", ane.ParamName);
            Assert.Contains("'" + expected + "'", ane.Message, StringComparison.Ordinal);
        }
    }
}
