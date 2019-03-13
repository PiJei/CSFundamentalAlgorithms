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
using CSFundamentals.DataStructures.Trees.API;
using CSFundamentals.Styling;

// TODO: Could just move all the other implementations of BinarySearchTree also here such as delete and build, and make a mock object in tests or turn this to non-abstract class with virtual implementations

namespace CSFundamentals.DataStructures.Trees
{
    /// <summary>
    /// Implements a binary search tree, and its operations. In a binary search tree, each node's key is larger than its left child's key, and smaller than its right child's key.
    /// A binary Search Tree can be used as a key-value store. 
    /// </summary>
    /// <typeparam name="T1">Specifies the type of the key in tree nodes.</typeparam>
    /// <typeparam name="T2">Specifies the type of the value in tree nodes. </typeparam>
    [DataStructure("BinarySearchTree (aka BST)")]
    public class BinarySearchTreeBase<T1, T2> : BinarySearchTreeBase<BinarySearchTreeNode<T1, T2>, T1, T2> where T1 : IComparable<T1>
    {
        //TODO Compute best and worst case for build operation. 
        [TimeComplexity(Case.Worst, "O(n^2)")] //  TODO: Use character map for power 2
        [TimeComplexity(Case.Average, "O(nLog(n))")]
        [SpaceComplexity("O(n)")]
        public override BinarySearchTreeNode<T1, T2> Build(List<BinarySearchTreeNode<T1, T2>> nodes)
        {
            return Build_BST(nodes);
        }

        // TODO: SOrt usings and headers in all the tree related code I have created, ... 
        // TODO: These bounds are no longer correct generally, depending on the Tree they change...
        // one way is to have these also as abstract and in the inherited class just make calls here and use those as wrappers, to have the space and insert complexity, etc, ... 
        // that is not a bad idea!
        // TODO: Build is very similar in all of these trees so also have a Build_BST somethings, ... 
        /// <summary>
        /// Implements insert in a binary search tree. 
        /// </summary>
        /// <param name="root">The node at which we would like to start the insert operation.</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>The new root node.</returns>
        [TimeComplexity(Case.Best, "O(1)", When = "The tree is empty, and the first node is added.")]
        [TimeComplexity(Case.Worst, "O(n)", When = "Tree is imbalanced such that it is like one sequential branch (linked list), every node except the leaf having exactly one child.")]
        [TimeComplexity(Case.Average, "O(Log(n))")]
        [SpaceComplexity("O(1)", InPlace = true)] /* Notice that a new node is allocated for a new key, thus can be considered as O(Size(TreeNode))*/
        public override BinarySearchTreeNode<T1, T2> Insert(BinarySearchTreeNode<T1, T2> root, BinarySearchTreeNode<T1, T2> newNode)
        {
            return Insert_BST(root, newNode);
        }

        [TimeComplexity(Case.Average, "")] // TODO
        [SpaceComplexity("O(1)")]
        public override BinarySearchTreeNode<T1, T2> Delete(BinarySearchTreeNode<T1, T2> root, T1 key)
        {
            if (root == null) return root;

            if (root.Key.CompareTo(key) < 0)
            {
                root.RightChild = Delete(root.RightChild, key);
            }
            else if (root.Key.CompareTo(key) > 0)
            {
                root.LeftChild = Delete(root.LeftChild, key);
            }
            else
            {
                if (root.RightChild == null && root.LeftChild == null)
                {
                    return null;
                }

                if (root.RightChild == null)
                {
                    return root.LeftChild;
                }

                if (root.LeftChild == null)
                {
                    return root.RightChild;
                }

                /* Else replacing the node that has 2 non-null children with its in-order successor, or could alternatively replace it with its in-order predecessor. */
                /* From these definitions it is obvious that the replacement node has less than 2 children. */
                BinarySearchTreeNode<T1, T2> rightChildMin = FindMin(root.RightChild);
                root.Key = rightChildMin.Key;
                root.Value = rightChildMin.Value;
                root.RightChild = Delete(root.RightChild, rightChildMin.Key); /* at this point both node, and rightChildMin have the same keys, but calling delete on the same key, will only result in the removal  of rightChildMin, because pf the root that is passed to Delete.*/
            }
            return root;
        }

        /// <summary>
        /// Implements Search/Lookup/Find operation for a BinarySearchTree. 
        /// </summary>
        /// <param name="root">Specifies the root of the tree.</param>
        /// <param name="key">Specifies the key, the method should look for. </param>
        /// <returns>The tree node that has the key. </returns>
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(n)", When = "Tree is imbalanced such that it is like one sequential branch (linked list), every node except the leaf having exactly one child.")]
        [TimeComplexity(Case.Average, "O(Log(n))")]
        [SpaceComplexity("O(1)", InPlace = true)]
        public override BinarySearchTreeNode<T1, T2> Search(BinarySearchTreeNode<T1, T2> root, T1 key)
        {
            return Search_BST(root, key);
        }

        /// <summary>
        /// Implements Update operation for a BinarySearchTree.
        /// </summary>
        /// <param name="root">Specifies the root of the tree.</param>
        /// <param name="key">Specifies the key of the node for which the value should be updated. </param>
        /// <param name="value">Specifies the new value for the given key. </param>
        /// <returns>True in case of success, and false otherwise. </returns>
        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "o(n)")]
        [TimeComplexity(Case.Average, "O(Log(n))")]
        [SpaceComplexity("O(1)", InPlace = true)]
        public override bool Update(BinarySearchTreeNode<T1,T2> root, T1 key, T2 value)
        {
            return Update_BST(root, key, value);
        }

        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(n)")]
        [TimeComplexity(Case.Average, "O(Log(n))")]
        [SpaceComplexity("O(1)")]
        public override BinarySearchTreeNode<T1, T2> FindMin(BinarySearchTreeNode<T1, T2> root)
        {
            return FindMin_BST(root);
        }

        [TimeComplexity(Case.Best, "O(1)")]
        [TimeComplexity(Case.Worst, "O(n)")]
        [TimeComplexity(Case.Average, "O(Log(n))")]
        [SpaceComplexity("O(1)")]
        public override BinarySearchTreeNode<T1, T2> FindMax(BinarySearchTreeNode<T1, T2> root)
        {
            return FindMax_BST(root);
        }
    }
}
