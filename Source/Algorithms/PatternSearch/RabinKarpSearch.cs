﻿#region copyright
/* 
 * Copyright (c) 2019 (PiJei) 
 * 
 * This file is part of AlgorithmsAndDataStructures project.
 *
 * AlgorithmsAndDataStructures is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * AlgorithmsAndDataStructures is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with AlgorithmsAndDataStructures.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion
using AlgorithmsAndDataStructures.Algorithms.Hashing;
using AlgorithmsAndDataStructures.Decoration;

namespace AlgorithmsAndDataStructures.Algorithms.PatternSearch
{
    /// <summary>
    /// Implements Rabin-Karp algorithm for searching a (pattern) string in another string. 
    /// </summary>
    public class RabinKarpSearch
    {
        private const int NumCharacters = 256;
        private const int PrimeNumber = 101;

        /// <summary>
        /// Implements RabinKarp search algorithm using <see cref="RollingHash"/> concept. 
        /// </summary>
        /// <param name= "text">The string in which <paramref name="pattern"/> is searched for.</param>
        /// <param name= "pattern">The string that is being searched in (<paramref name="text"/>).</param>
        /// <returns>All the indexes in <paramref name="text"/> starting from which a match for <paramref name="pattern"/> is found.</returns>
        [Algorithm(AlgorithmType.PatternSearch, "RabinKarp")]
        public static int Search(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            int hashConstant = RollingHash.ComputeHashConstantForRollingHash(m, PrimeNumber, NumCharacters);
            int patternHash = RollingHash.GetHash(pattern, PrimeNumber, NumCharacters); /* This hash is computed only once. Complexity : O(subString.Length)*/

            if (m > text.Length)
            {
                return -1;
            }

            string patternInText = text.Substring(0, m);
            int patternInTextHash = RollingHash.GetHash(patternInText, PrimeNumber, NumCharacters);

            for (int i = 0; i < n - 1; i++) /* O(text.Length) */
            {
                if (patternHash == patternInTextHash)
                {
                    if (pattern == patternInText) /* This check is necessary as the hash function may have collisions.*/
                    {
                        return i;
                    }
                }

                if (i < n - m)
                {
                    patternInText = text.Substring(i + 1, m); /* a substring in text, size of pattern, starting at index i;*/
                    patternInTextHash = RollingHash.GenerateRollingHash(patternInTextHash, text[i], text[i + m], m, hashConstant, PrimeNumber, NumCharacters); /* O(1) with a rolling hash, otherwise: O(pattern.Length) */
                }
                else
                {
                    break;
                }
            }

            return -1;
        }
    }
}
