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

 using System.Collections.Generic;
using CSFundamentals.Algorithms.GraphTraversal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSFundamentalsTests.Algorithms.GraphTraversal
{
    [TestClass]
    public class DfsTests
    {
        private GraphNode A = new GraphNode(4);
        private GraphNode B = new GraphNode(1);
        private GraphNode C = new GraphNode(20);
        private GraphNode D = new GraphNode(6);
        private GraphNode E = new GraphNode(3);
        private GraphNode F = new GraphNode(11);
        private GraphNode G = new GraphNode(5);

        [TestInitialize]
        public void Init()
        {
            A.Adjacents.Add(B);
            A.Adjacents.Add(C);
            A.Adjacents.Add(D);

            B.Adjacents.Add(E);
            B.Adjacents.Add(F);
            B.Adjacents.Add(A);

            C.Adjacents.Add(G);
            C.Adjacents.Add(A);

            D.Adjacents.Add(F);
            D.Adjacents.Add(A);

            F.Adjacents.Add(D);
            F.Adjacents.Add(B);

            E.Adjacents.Add(B);
        }

        public void ResetGraph() // It seems that this step is unnecessary. Even though the same instance is used across all the test methods. 
        {
            A.IsInserted = false;
            B.IsInserted = false;
            C.IsInserted = false;
            D.IsInserted = false;
            E.IsInserted = false;
            F.IsInserted = false;
            G.IsInserted = false;
        }
        [TestMethod]
        public void DFS_Iterative_test_StartFromA()
        {
            List<GraphNode> dfsOrdering = DFS.DFS_Iterative(A);
            Assert.AreEqual(7, dfsOrdering.Count);

            Assert.AreEqual(4, dfsOrdering[0].Value);
            Assert.AreEqual(6, dfsOrdering[1].Value);
            Assert.AreEqual(11, dfsOrdering[2].Value);
            Assert.AreEqual(20, dfsOrdering[3].Value);
            Assert.AreEqual(5, dfsOrdering[4].Value);
            Assert.AreEqual(1, dfsOrdering[5].Value);
            Assert.AreEqual(3, dfsOrdering[6].Value);

            ResetGraph();
        }

        [TestMethod]
        public void DFS_Iterative_test_StartFromE()
        {
            List<GraphNode> dfsOrdering = DFS.DFS_Iterative(E);
            Assert.AreEqual(7, dfsOrdering.Count);

            Assert.AreEqual(3, dfsOrdering[0].Value);
            Assert.AreEqual(1, dfsOrdering[1].Value);
            Assert.AreEqual(4, dfsOrdering[2].Value);
            Assert.AreEqual(6, dfsOrdering[3].Value);
            Assert.AreEqual(20, dfsOrdering[4].Value);
            Assert.AreEqual(5, dfsOrdering[5].Value);
            Assert.AreEqual(11, dfsOrdering[6].Value);

            ResetGraph();
        }

        [TestMethod]
        public void DFS_Recursive_StartFromA()
        {
            ResetGraph();
            List<GraphNode> dfsOrdering = new List<GraphNode>();
            DFS.DFS_Recursive(A, dfsOrdering);

            Assert.AreEqual(7, dfsOrdering.Count);

            Assert.AreEqual(4, dfsOrdering[0].Value);
            Assert.AreEqual(1, dfsOrdering[1].Value);
            Assert.AreEqual(3, dfsOrdering[2].Value);
            Assert.AreEqual(11, dfsOrdering[3].Value);
            Assert.AreEqual(6, dfsOrdering[4].Value);
            Assert.AreEqual(20, dfsOrdering[5].Value);
            Assert.AreEqual(5, dfsOrdering[6].Value);

            ResetGraph();
        }

        [TestMethod]
        public void DFS_Recursive_StartFromE()
        {
            ResetGraph();
            List<GraphNode> dfsOrdering = new List<GraphNode>();
            DFS.DFS_Recursive(E, dfsOrdering);

            Assert.AreEqual(7, dfsOrdering.Count);

            Assert.AreEqual(3, dfsOrdering[0].Value);
            Assert.AreEqual(1, dfsOrdering[1].Value);
            Assert.AreEqual(11, dfsOrdering[2].Value);
            Assert.AreEqual(6, dfsOrdering[3].Value);
            Assert.AreEqual(4, dfsOrdering[4].Value);
            Assert.AreEqual(20, dfsOrdering[5].Value);
            Assert.AreEqual(5, dfsOrdering[6].Value);

            ResetGraph();
        }
    }
}
