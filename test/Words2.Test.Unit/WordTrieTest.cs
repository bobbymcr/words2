//-----------------------------------------------------------------------
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
    }
}
