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
            Assert.Equal(3, a1.Length);
            Assert.Equal("ABCABC", a2.ToString());
            Assert.Equal(6, a2.Length);
            Assert.Equal("ABCABCABC", a3.ToString());
            Assert.Equal(9, a3.Length);
            Assert.Equal("ABCABCABCABC", a4.ToString());
            Assert.Equal(12, a4.Length);
            Assert.Equal("ABCABCABCABCABC", a5.ToString());
            Assert.Equal(15, a5.Length);
        }

        [Fact]
        public void FromString_AtSignAllowed()
        {
            Word a1 = new Word("@bc");
            Word a2 = new Word("a@bc");
            Word a3 = new Word("ab@cd");

            Assert.Equal("@BC", a1.ToString());
            Assert.Equal(3, a1.Length);
            Assert.Equal("A@BC", a2.ToString());
            Assert.Equal(4, a2.Length);
            Assert.Equal("AB@CD", a3.ToString());
            Assert.Equal(5, a3.Length);
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
        public void ForEach_FifteenChars_RunsForEachChar()
        {
            ForEachInnerTest("aBcdefghijklmno", new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O' });
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
        public void CompareTo_LongWords_ComparesCorrectly()
        {
            Word a01 = new Word("aaaaaaaaaaaaaaa");
            Word a02 = new Word("aaaaaaaaaaaaaab");
            Word a03 = new Word("aaaaaaaaaaaaabc");
            Word a04 = new Word("aaaaaaaaaaaabcd");
            Word a05 = new Word("aaaaaaaaaaabcde");
            Word a06 = new Word("aaaaaaaaaabcdef");
            Word a07 = new Word("aaaaaaaaabcdefg");
            Word a08 = new Word("aaaaaaaabcdefgh");
            Word a09 = new Word("aaaaaaabcdefghi");
            Word a10 = new Word("aaaaaabcdefghij");
            Word a11 = new Word("aaaaabcdefghijk");
            Word a12 = new Word("aaaabcdefghijkl");
            Word a13 = new Word("aaabcdefghijklm");
            Word a14 = new Word("aabcdefghijklmn");
            Word a15 = new Word("abcdefghijklmno");

            Assert.True(a01.CompareTo(a01) == 0);
            Assert.True(a01.CompareTo(a02) < 0);
            Assert.True(a02.CompareTo(a01) > 0);
            Assert.True(a02.CompareTo(a03) < 0);
            Assert.True(a03.CompareTo(a02) > 0);
            Assert.True(a03.CompareTo(a04) < 0);
            Assert.True(a04.CompareTo(a03) > 0);
            Assert.True(a04.CompareTo(a05) < 0);
            Assert.True(a05.CompareTo(a04) > 0);
            Assert.True(a05.CompareTo(a06) < 0);
            Assert.True(a06.CompareTo(a05) > 0);
            Assert.True(a06.CompareTo(a07) < 0);
            Assert.True(a07.CompareTo(a06) > 0);
            Assert.True(a07.CompareTo(a08) < 0);
            Assert.True(a08.CompareTo(a07) > 0);
            Assert.True(a08.CompareTo(a09) < 0);
            Assert.True(a09.CompareTo(a08) > 0);
            Assert.True(a09.CompareTo(a10) < 0);
            Assert.True(a10.CompareTo(a09) > 0);
            Assert.True(a10.CompareTo(a11) < 0);
            Assert.True(a11.CompareTo(a10) > 0);
            Assert.True(a11.CompareTo(a12) < 0);
            Assert.True(a12.CompareTo(a11) > 0);
            Assert.True(a12.CompareTo(a13) < 0);
            Assert.True(a13.CompareTo(a12) > 0);
            Assert.True(a13.CompareTo(a14) < 0);
            Assert.True(a14.CompareTo(a13) > 0);
            Assert.True(a14.CompareTo(a15) < 0);
            Assert.True(a15.CompareTo(a14) > 0);
            Assert.True(a15.CompareTo(a15) == 0);
        }

        [Fact]
        public void CompareTo_LongWordsWithShortWords_ComparesCorrectly()
        {
            Word a01 = new Word("a");
            Word b01 = new Word("b");
            Word a15 = new Word("aaaaaaaaaaaaaaa");
            Word b15 = new Word("bbbbbbbbbbbbbbb");
            Word ba02 = new Word("ba");
            Word bc02 = new Word("bc");

            Assert.True(a01.CompareTo(b01) < 0);
            Assert.True(b01.CompareTo(a01) > 0);
            Assert.True(a01.CompareTo(a15) < 0);
            Assert.True(a15.CompareTo(a01) > 0);
            Assert.True(b01.CompareTo(a15) > 0);
            Assert.True(a15.CompareTo(b01) < 0);
            Assert.True(a15.CompareTo(ba02) < 0);
            Assert.True(b15.CompareTo(ba02) > 0);
            Assert.True(ba02.CompareTo(b15) < 0);
            Assert.True(b15.CompareTo(bc02) < 0);
            Assert.True(bc02.CompareTo(b15) > 0);
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

        [Fact]
        public void GetHashCode_LongWord_ReturnsValidCode()
        {
            Word a15 = new Word("aaaaaaaaaaaaaaa");
            Word ab15 = new Word("aaaaaaaaaaaaaab");
            
            int ca15 = a15.GetHashCode();
            int cab15 = ab15.GetHashCode();

            Assert.NotEqual(ca15, cab15);
        }

        [Fact]
        public void Indexer_OutOfRange_Throws()
        {
            Word w = new Word("a");
            
            Exception e = Record.Exception(() => w[1]);
            Assert.NotNull(e);
            Assert.IsType<IndexOutOfRangeException>(e);
        }

        [Fact]
        public void Indexer_ShortWord_ReturnsCharAtPosition()
        {
            Word w = new Word("abcdefgh");
            Assert.Equal('A', w[0]);
            Assert.Equal('B', w[1]);
            Assert.Equal('C', w[2]);
            Assert.Equal('D', w[3]);
            Assert.Equal('E', w[4]);
            Assert.Equal('F', w[5]);
            Assert.Equal('G', w[6]);
            Assert.Equal('H', w[7]);
        }

        [Fact]
        public void Indexer_LongWord_ReturnsCharAtPosition()
        {
            Word w = new Word("abcdefghijklmno");
            Assert.Equal('A', w[0]);
            Assert.Equal('B', w[1]);
            Assert.Equal('C', w[2]);
            Assert.Equal('D', w[3]);
            Assert.Equal('E', w[4]);
            Assert.Equal('F', w[5]);
            Assert.Equal('G', w[6]);
            Assert.Equal('H', w[7]);
            Assert.Equal('I', w[8]);
            Assert.Equal('J', w[9]);
            Assert.Equal('K', w[10]);
            Assert.Equal('L', w[11]);
            Assert.Equal('M', w[12]);
            Assert.Equal('N', w[13]);
            Assert.Equal('O', w[14]);
        }

        [Fact]
        public void Append_AddsCharAtEnd_ReturnsNewValue()
        {
            Word a = new Word("a");

            Word ab = a.Append('b');

            Assert.Equal("AB", ab.ToString());
        }

        [Fact]
        public void Replace_LongWord_ChangesCharAtIndexAndReturnsNewValue()
        {
            Word a = new Word("abcdefghijklmno");
            Word b;

            b = a.Replace(14, 'l');
            Assert.Equal("ABCDEFGHIJKLMNL", b.ToString());

            b = a.Replace(13, 'm');
            Assert.Equal("ABCDEFGHIJKLMMO", b.ToString());

            b = a.Replace(12, 'n');
            Assert.Equal("ABCDEFGHIJKLNNO", b.ToString());

            b = a.Replace(11, 'o');
            Assert.Equal("ABCDEFGHIJKOMNO", b.ToString());

            b = a.Replace(10, 'p');
            Assert.Equal("ABCDEFGHIJPLMNO", b.ToString());

            b = a.Replace(9, 'q');
            Assert.Equal("ABCDEFGHIQKLMNO", b.ToString());

            b = a.Replace(8, 'r');
            Assert.Equal("ABCDEFGHRJKLMNO", b.ToString());

            b = a.Replace(7, 's');
            Assert.Equal("ABCDEFGSIJKLMNO", b.ToString());

            b = a.Replace(6, 't');
            Assert.Equal("ABCDEFTHIJKLMNO", b.ToString());

            b = a.Replace(5, 'u');
            Assert.Equal("ABCDEUGHIJKLMNO", b.ToString());

            b = a.Replace(4, 'v');
            Assert.Equal("ABCDVFGHIJKLMNO", b.ToString());

            b = a.Replace(3, 'w');
            Assert.Equal("ABCWEFGHIJKLMNO", b.ToString());

            b = a.Replace(2, 'x');
            Assert.Equal("ABXDEFGHIJKLMNO", b.ToString());

            b = a.Replace(1, 'y');
            Assert.Equal("AYCDEFGHIJKLMNO", b.ToString());

            b = a.Replace(0, 'z');
            Assert.Equal("ZBCDEFGHIJKLMNO", b.ToString());
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
