//-----------------------------------------------------------------------
// <copyright file="WordTrie.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2
{
    using System;
    using System.Collections.Generic;

    public class WordTrie
    {
        private readonly Node root;

        public WordTrie(IEnumerable<Word> words)
            : this()
        {
            foreach (Word word in words)
            {
                this.Add(word);
            }
        }

        public WordTrie()
        {
            this.root = new Node();
        }

        public bool Add(Word item)
        {
            Node current = this.root;
            bool anyAdded = false;
            for (int i = 0; i < item.Length; ++i)
            {
                bool added = current.TryAddChild(item[i], out current);
                if (added)
                {
                    anyAdded = true;
                }
            }

            return anyAdded;
        }

        public bool Remove(Word item)
        {
            Node current = this.root;
            for (int i = 0; i < item.Length; ++i)
            {
                bool removed = current.TryRemoveChild(item[i], out current);
                if (!removed)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Contains(Word item)
        {
            Node current = this.root;
            for (int i = 0; i < item.Length; ++i)
            {
                bool found = current.TryGetChild(item[i], out current);
                if (!found)
                {
                    return false;
                }
            }

            return true;
        }

        private sealed class Node
        {
            private readonly Dictionary<char, Node> children;

            public Node()
            {
                this.children = new Dictionary<char, Node>();
            }

            public bool TryAddChild(char key, out Node child)
            {
                bool added = false;
                if (!this.children.TryGetValue(key, out child))
                {
                    added = true;
                    child = new Node();
                    this.children.Add(key, child);
                }

                return added;
            }

            public bool TryRemoveChild(char key, out Node child)
            {
                bool removed = false;
                if (this.children.TryGetValue(key, out child))
                {
                    removed = true;
                    this.children.Remove(key);
                }

                return removed;
            }

            public bool TryGetChild(char key, out Node child)
            {
                return this.children.TryGetValue(key, out child);
            }
        }
    }
}
