﻿//-----------------------------------------------------------------------
// <copyright file="WordTrieTest.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class WordTrieTest
    {
        public WordTrieTest()
        {
        }

        [Fact]
        public void Add_FirstTimeReturnsTrueNextTimeReturnsFalse()
        {
            WordTrie trie = new WordTrie();

            Assert.True(trie.Add(new Word("z")));
            Assert.False(trie.Add(new Word("z")));
            Assert.False(trie.Add(new Word("z")));

            Assert.True(trie.Add(new Word("zz")));
            Assert.False(trie.Add(new Word("zz")));
            Assert.False(trie.Add(new Word("zz")));

            Assert.True(trie.Add(new Word("zzx")));
            Assert.False(trie.Add(new Word("zzx")));
            Assert.False(trie.Add(new Word("zzx")));

            Assert.True(trie.Add(new Word("azxy")));
            Assert.False(trie.Add(new Word("azxy")));
            Assert.False(trie.Add(new Word("azxy")));

            Assert.True(trie.Add(new Word("azx")));
            Assert.False(trie.Add(new Word("azx")));
            Assert.False(trie.Add(new Word("azx")));

            Assert.True(trie.Add(new Word("zxzxzxzxzxzxzxz")));
            Assert.False(trie.Add(new Word("zxzxzxzxzxzxzxz")));
            Assert.False(trie.Add(new Word("zxzxzxzxzxzxzxz")));
        }

        [Fact]
        public void Construct_WordSequence_AddsToTrie()
        {
            Word w1 = new Word("aaaa");
            Word w2 = new Word("zzzz");

            WordTrie trie = new WordTrie(new Word[] { w1, w2 });

            Assert.True(trie.Contains(w1));
            Assert.True(trie.Contains(w2));
        }

        [Fact]
        public void Contains_ReturnsTrueIfFoundFalseOtherwise()
        {
            WordTrie trie = new WordTrie();

            Assert.False(trie.Contains(new Word("a")));

            trie.Add(new Word("a"));

            Assert.True(trie.Contains(new Word("a")));
            Assert.False(trie.Contains(new Word("ab")));

            trie.Add(new Word("abc"));

            Assert.False(trie.Contains(new Word("ab")));
            Assert.True(trie.Contains(new Word("abc")));

            trie.Remove(new Word("abc"));

            Assert.False(trie.Contains(new Word("ab")));
            Assert.False(trie.Contains(new Word("abc")));

            trie.Add(new Word("abcdefghijklmno"));

            Assert.False(trie.Contains(new Word("ab")));
            Assert.False(trie.Contains(new Word("abc")));
            Assert.False(trie.Contains(new Word("abcd")));
            Assert.False(trie.Contains(new Word("abcde")));
            Assert.False(trie.Contains(new Word("abcdef")));
            Assert.False(trie.Contains(new Word("abcdefg")));
            Assert.False(trie.Contains(new Word("abcdefgh")));
            Assert.False(trie.Contains(new Word("abcdefghi")));
            Assert.False(trie.Contains(new Word("abcdefghij")));
            Assert.False(trie.Contains(new Word("abcdefghijk")));
            Assert.False(trie.Contains(new Word("abcdefghijkl")));
            Assert.False(trie.Contains(new Word("abcdefghijklm")));
            Assert.False(trie.Contains(new Word("abcdefghijklmn")));
            Assert.True(trie.Contains(new Word("abcdefghijklmno")));
        }

        [Fact]
        public void Remove_TrieEmpty_AlwaysReturnsFalse()
        {
            WordTrie trie = new WordTrie();

            Assert.False(trie.Remove(new Word("z")));
            Assert.False(trie.Remove(new Word("zy")));
            Assert.False(trie.Remove(new Word("zyx")));
            Assert.False(trie.Remove(new Word("zyxw")));
            Assert.False(trie.Remove(new Word("abcdefghijklmno")));
        }

        [Fact]
        public void Remove_FirstTimeReturnsTrueNextTimeReturnsFalse()
        {
            WordTrie trie = new WordTrie();

            trie.Add(new Word("z"));

            Assert.True(trie.Remove(new Word("z")));
            Assert.False(trie.Remove(new Word("z")));
            Assert.False(trie.Remove(new Word("z")));

            trie.Add(new Word("zz"));

            Assert.True(trie.Remove(new Word("zz")));
            Assert.False(trie.Remove(new Word("zz")));
            Assert.False(trie.Remove(new Word("zz")));

            trie.Add(new Word("zzx"));

            Assert.True(trie.Remove(new Word("zzx")));
            Assert.False(trie.Remove(new Word("zzx")));
            Assert.False(trie.Remove(new Word("zzx")));

            trie.Add(new Word("zwxy"));

            Assert.True(trie.Remove(new Word("zwxy")));
            Assert.False(trie.Remove(new Word("zwxy")));
            Assert.False(trie.Remove(new Word("zwxy")));

            trie.Add(new Word("vwxy"));

            Assert.True(trie.Remove(new Word("vwxy")));
            Assert.False(trie.Remove(new Word("vwxy")));
            Assert.False(trie.Remove(new Word("vwxy")));
        }

        [Fact]
        public void Match_EmptyTrie_DoesNothing()
        {
            WordTrie trie = new WordTrie();

            int count = 0;
            trie.Match(new Word("aaaa"), w => ++count);

            Assert.Equal(0, count);
        }

        [Fact]
        public void Match_FirstCharNoMatch_DoesNothing()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("baaa"));
            
            int count = 0;
            trie.Match(new Word("aaaa"), w => ++count);

            Assert.Equal(0, count);
        }

        [Fact]
        public void Match_FirstCharMatchButNoOtherMatch_DoesNothing()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abbb"));

            int count = 0;
            trie.Match(new Word("ac"), w => ++count);

            Assert.Equal(0, count);
        }

        [Fact]
        public void Match_FirstLevelOneMatch_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("ab"));

            List<string> words = new List<string>();
            trie.Match(new Word("a"), w => words.Add(w.ToString()));

            Assert.Equal(new string[] { "AB" }, words.ToArray());
        }

        [Fact]
        public void Match_FirstLevelTwoMatches_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("ab"));
            trie.Add(new Word("ac"));

            List<string> words = new List<string>();
            trie.Match(new Word("a"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "AB", "AC" }, words.ToArray());
        }

        [Fact]
        public void Match_FirstAndSecondLevelTwoMatches_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("abd"));

            List<string> words = new List<string>();
            trie.Match(new Word("ab"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "ABC", "ABD" }, words.ToArray());
        }

        [Fact]
        public void Match_FirstLevelThreeDeepMatches_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("adefghijkl"));
            trie.Add(new Word("acdefghi"));
            trie.Add(new Word("abcdefg"));

            List<string> words = new List<string>();
            trie.Match(new Word("a"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "ABCDEFG", "ACDEFGHI", "ADEFGHIJKL" }, words.ToArray());
        }

        [Fact]
        public void Match_OneCharFirstLevelWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("xbc"));
            trie.Add(new Word("zbc"));

            List<string> words = new List<string>();
            trie.Match(new Word("?"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "ABC", "XBC", "ZBC" }, words.ToArray());
        }

        [Fact]
        public void Match_TwoCharsFirstLevelWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("bbb"));
            trie.Add(new Word("bbxx"));
            trie.Add(new Word("ccccc"));

            List<string> words = new List<string>();
            trie.Match(new Word("?b"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "ABC", "BBB", "BBXX" }, words.ToArray());
        }

        [Fact]
        public void Match_TwoCharsSecondLevelWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("acd"));
            trie.Add(new Word("axy"));
            trie.Add(new Word("bcd"));

            List<string> words = new List<string>();
            trie.Match(new Word("a?"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "ABC", "ACD", "AXY" }, words.ToArray());
        }

        [Fact]
        public void Match_ThreeCharsSecondLevelWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("acc"));
            trie.Add(new Word("axa"));
            trie.Add(new Word("axc"));

            List<string> words = new List<string>();
            trie.Match(new Word("a?c"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "ABC", "ACC", "AXC" }, words.ToArray());
        }

        [Fact]
        public void Match_ThreeCharsAllWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("bb"));
            trie.Add(new Word("ccc"));
            trie.Add(new Word("dddd"));
            trie.Add(new Word("eeeee"));

            List<string> words = new List<string>();
            trie.Match(new Word("???"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "CCC", "DDDD", "EEEEE" }, words.ToArray());
        }

        [Fact]
        public void MatchMaxLength_TwoCharsMaxLength3_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("bb"));
            trie.Add(new Word("cc"));
            trie.Add(new Word("ccc"));
            trie.Add(new Word("ccd"));
            trie.Add(new Word("cccc"));
            trie.Add(new Word("dddd"));
            trie.Add(new Word("eeeee"));

            List<string> words = new List<string>();
            trie.Match(new Word("cc"), 3, w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "CC", "CCC", "CCD", }, words.ToArray());
        }

        [Fact]
        public void MatchMaxLength_ThreeCharsAllWildMaxLength4_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("bb"));
            trie.Add(new Word("ccc"));
            trie.Add(new Word("dddd"));
            trie.Add(new Word("eeeee"));

            List<string> words = new List<string>();
            trie.Match(new Word("???"), 4, w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "CCC", "DDDD" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length1_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("b"));
            trie.Add(new Word("bc"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("a"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "A" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length1Wild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("b"));
            trie.Add(new Word("bc"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("?"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "A", "B" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length2_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("aa"));
            trie.Add(new Word("aab"));
            trie.Add(new Word("bbb"));
            trie.Add(new Word("bc"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("aa"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "AA" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length2FirstWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("aa"));
            trie.Add(new Word("aab"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("bbb"));
            trie.Add(new Word("bc"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("?a"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "AA", "AA", "AB" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length2LastWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("aa"));
            trie.Add(new Word("aab"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("bbb"));
            trie.Add(new Word("bc"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("a?"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "AA", "AA", "AB" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length2BothWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("aa"));
            trie.Add(new Word("aab"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("bbb"));
            trie.Add(new Word("bc"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("??"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "AA", "AB", "BC" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length3_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("dfg"));
            trie.Add(new Word("f"));
            trie.Add(new Word("fed"));
            trie.Add(new Word("feds"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("def"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "FED" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length3OneWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("dfg"));
            trie.Add(new Word("f"));
            trie.Add(new Word("fed"));
            trie.Add(new Word("feds"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("d?f"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "DFG", "FED" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length3TwoWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("dfg"));
            trie.Add(new Word("dog"));
            trie.Add(new Word("f"));
            trie.Add(new Word("fed"));
            trie.Add(new Word("feds"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("d??"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "DFG", "DOG", "FED" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length6_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("bcd"));
            trie.Add(new Word("east"));
            trie.Add(new Word("easter"));
            trie.Add(new Word("eaters"));
            trie.Add(new Word("era"));
            trie.Add(new Word("erase"));
            trie.Add(new Word("eraser"));
            trie.Add(new Word("extra"));
            trie.Add(new Word("seat"));
            trie.Add(new Word("seater"));
            trie.Add(new Word("tease"));
            trie.Add(new Word("teaser"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("eersta"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "EASTER", "EATERS", "SEATER", "TEASER" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_Length6ThreeWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("abc"));
            trie.Add(new Word("bcd"));
            trie.Add(new Word("east"));
            trie.Add(new Word("easter"));
            trie.Add(new Word("eaters"));
            trie.Add(new Word("era"));
            trie.Add(new Word("erase"));
            trie.Add(new Word("eraser"));
            trie.Add(new Word("extra"));
            trie.Add(new Word("seat"));
            trie.Add(new Word("seater"));
            trie.Add(new Word("tease"));
            trie.Add(new Word("teaser"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("ee???a"), w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "EASTER", "EATERS", "ERASER", "SEATER", "TEASER" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_MinLength1_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("b"));
            trie.Add(new Word("bc"));
            trie.Add(new Word("bcd"));
            trie.Add(new Word("c"));
            trie.Add(new Word("cab"));
            trie.Add(new Word("d"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("abc"), 1, w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "A", "AB", "B", "BC", "C", "CAB" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_MinLength1OneWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("a"));
            trie.Add(new Word("ab"));
            trie.Add(new Word("b"));
            trie.Add(new Word("bc"));
            trie.Add(new Word("bcd"));
            trie.Add(new Word("c"));
            trie.Add(new Word("cab"));
            trie.Add(new Word("d"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("ab?"), 1, w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "A", "A", "AB", "AB", "AB", "B", "B", "BC", "C", "CAB", "D" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_MinLength3_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("area"));
            trie.Add(new Word("best"));
            trie.Add(new Word("ear"));
            trie.Add(new Word("ears"));
            trie.Add(new Word("east"));
            trie.Add(new Word("eat"));
            trie.Add(new Word("raster"));
            trie.Add(new Word("rates"));
            trie.Add(new Word("steer"));
            trie.Add(new Word("tear"));
            trie.Add(new Word("treat"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("eersta"), 3, w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "EAR", "EARS", "EAST", "EAT", "RATES", "STEER", "TEAR" }, words.ToArray());
        }

        [Fact]
        public void MatchAnagram_MinLength3ThreeWild_InvokesOnAllMatches()
        {
            WordTrie trie = new WordTrie();
            trie.Add(new Word("area"));
            trie.Add(new Word("best"));
            trie.Add(new Word("ear"));
            trie.Add(new Word("ears"));
            trie.Add(new Word("east"));
            trie.Add(new Word("eat"));
            trie.Add(new Word("raster"));
            trie.Add(new Word("rates"));
            trie.Add(new Word("steer"));
            trie.Add(new Word("tear"));
            trie.Add(new Word("treat"));

            List<string> words = new List<string>();
            trie.MatchAnagram(new Word("??ae?s"), 3, w => words.Add(w.ToString()));

            words.Sort();
            Assert.Equal(new string[] { "AREA", "AREA", "AREA", "AREA", "AREA", "BEST", "BEST", "BEST", "EAR", "EAR", "EAR", "EAR", "EARS", "EARS", "EARS", "EARS", "EARS", "EARS", "EARS", "EAST", "EAST", "EAST", "EAST", "EAST", "EAST", "EAST", "EAT", "EAT", "EAT", "EAT", "RASTER", "RATES", "RATES", "RATES", "RATES", "STEER", "STEER", "TEAR", "TEAR", "TEAR", "TREAT" }, words.ToArray());
        }
    }
}
