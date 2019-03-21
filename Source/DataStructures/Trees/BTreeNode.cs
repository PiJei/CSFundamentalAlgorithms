﻿/* 
 * Copyright (c) 2019 (PiJei) 
 * 
 * This file is part of CSFundamentalAlgorithms project.
 *
 * CSFundamentalAlgorithms is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * CSFundamentalAlgorithms is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with CSFundamentals.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

namespace CSFundamentals.DataStructures.Trees
{
    /// <summary>
    /// Implements a B-Tree node. A B-tree node is an ordered sequence of K keys, and K+1 children.
    /// </summary>
    /// <typeparam name="T1">Is the type of the keys in the tree. </typeparam>
    /// <typeparam name="T2">Is the type of the values in the tree. </typeparam>
    public class BTreeNode<T1, T2> where T1 : IComparable<T1>
    {
        /// <summary>
        /// Is the minimum number of keys in a B-tree internal/leaf node. (Notice that a root has no lower bound on the number of keys. Intuitively when the tree is just being built it might start with 1, and grow afterwards.)
        /// </summary>
        public int MinKeys { get; private set; }

        /// <summary>
        /// Is the maximum number of keys in a B-tree internal/leaf/root node. This is often 2 times the MinKeys.
        /// </summary>
        public int MaxKeys { get; private set; }

        /// <summary>
        /// Is the minimum number of branches/children a B-tree internal node can have. 
        /// </summary>
        public int MinBranchingDegree { get; private set; }

        /// <summary>
        /// Is the maximum number of branches/children a B-tree internal or root node can have. Leaf nodes contain 0 children. 
        /// </summary>
        public int MaxBranchingDegree { get; private set; }

        public List<KeyValuePair<T1, T2>> KeyValues { get; private set; }

        public List<BTreeNode<T1, T2>> Children { get; private set; }

        /// <summary>
        /// Is the parent of the current node.
        /// </summary>
        public BTreeNode<T1, T2> Parent = null;

        public BTreeNode(int minKeys)
        {
            MinKeys = minKeys;
            MaxKeys = 2 * minKeys;
            MinBranchingDegree = minKeys + 1;
            MaxBranchingDegree = MaxKeys + 1;
            KeyValues = new List<KeyValuePair<T1, T2>>(MaxKeys);
            Children = new List<BTreeNode<T1, T2>>(MaxBranchingDegree);
        }


        /// <summary>
        /// Checks whether the current node is leaf. A node is leaf if it has no children. 
        /// </summary>
        /// <returns>True if the current node is leaf, and false otherwise. </returns>
        public bool IsLeaf()
        {
            if (Children.Count == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether the current node is root. A node is root if it has no parent.
        /// </summary>
        /// <returns>True if the current node is root, and false otherwise.</returns>
        public bool IsRoot()
        {
            if (Parent == null)
            {
                return true;
            }
            return false;
        }
        //todo: do we need parent?, a link to the sibling? etc, left and right siblings? or just not?

        //TODO: How can  make tis to use the binary search I have implemented in this project?
        // Expects inclusive indexes, ...
        public int Search(T1 key, int startIndex, int endIndex)
        {
            if (startIndex <= endIndex &&
                endIndex <= KeyValues.Count - 1 &&
                KeyValues[startIndex].Key.CompareTo(key) >= 0
                && KeyValues[endIndex].Key.CompareTo(key) <= 0)
            {
                int middleIndex = (startIndex + endIndex) / 2;

                if (KeyValues[middleIndex].Key.CompareTo(key) == 0)
                {
                    return middleIndex;
                }
                else if (KeyValues[middleIndex].Key.CompareTo(key) < 0)
                {
                    return Search(key, startIndex, middleIndex - 1);
                }
                else if (KeyValues[middleIndex].Key.CompareTo(key) > 0)
                {
                    return Search(key, middleIndex + 1, endIndex);
                }
            }

            return -1;
        }

    }
}