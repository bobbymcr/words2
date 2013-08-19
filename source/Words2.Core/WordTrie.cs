﻿//-----------------------------------------------------------------------
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

            if (!current.IsLeaf)
            {
                anyAdded = true;
                current.IsLeaf = true;
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
            bool found = false;
            Node node;
            if (this.ContainsInner(item, out node))
            {
                found = node.IsLeaf;
            }

            return found;
        }

        public void Match(Word prefix, Action<Word> onMatch)
        {
            this.root.Match(prefix, onMatch);
        }

        private bool ContainsInner(Word item, out Node node)
        {
            node = this.root;
            for (int i = 0; i < item.Length; ++i)
            {
                bool found = node.TryGetChild(item[i], out node);
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
            private bool isLeaf;

            public Node()
            {
                this.children = new Dictionary<char, Node>();
            }

            public bool IsLeaf
            {
                get { return this.isLeaf; }
                set { this.isLeaf = value; }
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

            public void Match(Word prefix, Action<Word> onMatch)
            {
                MatchInner(this, prefix, 0, onMatch);
            }

            private static void MatchInner(Node current, Word prefix, int start, Action<Word> onMatch)
            {
                for (int i = start; i < prefix.Length; ++i)
                {
                    char c = prefix[i];
                    if (c != Word.WildChar)
                    {
                        if (!current.TryGetChild(c, out current))
                        {
                            return;
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<char, Node> child in current.children)
                        {
                            MatchInner(child.Value, prefix.Replace(i, child.Key), i + 1, onMatch);
                        }

                        return;
                    }
                }

                current.MatchSelfAndChildren(prefix, onMatch);
            }

            private void MatchSelfAndChildren(Word prefix, Action<Word> onMatch)
            {
                if (this.IsLeaf)
                {
                    onMatch(prefix);
                }

                foreach (KeyValuePair<char, Node> child in this.children)
                {
                    Word word = prefix.Append(child.Key);
                    child.Value.MatchSelfAndChildren(word, onMatch);
                }
            }
        }
    }
}