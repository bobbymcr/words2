//-----------------------------------------------------------------------
// <copyright file="WordLoaderTest.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class WordLoaderTest
    {
        public WordLoaderTest()
        {
        }

        [Fact]
        public void Load_EmptyString_DoesNothing()
        {
            int count = 0;
            WordLoader.Load(string.Empty, w => ++count);

            Assert.Equal(0, count);
        }

        [Fact]
        public void Load_TooLong_DoesNothing()
        {
            int count = 0;
            WordLoader.Load("abcdefghijklmnoqrstuvwxyz", w => ++count);

            Assert.Equal(0, count);
        }

        [Fact]
        public void Load_MixOfWords_FindsOnlyValidWordItemsSeparateByWhitespace()
        {
            List<string> words = new List<string>();
            WordLoader.Load("  a bc    def ghij abcdefghijklmnopqr  klmno\tpqrstu\rvwxy\nz  ", w => words.Add(w.ToString()));

            Assert.Equal(new string[] { "A", "BC", "DEF", "GHIJ", "KLMNO", "PQRSTU", "VWXY", "Z" }, words.ToArray());
        }
    }
}
