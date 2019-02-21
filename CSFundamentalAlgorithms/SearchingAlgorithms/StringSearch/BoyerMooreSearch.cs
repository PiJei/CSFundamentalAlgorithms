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
 * along with CSFundamentalAlgorithms.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System;

namespace CSFundamentalAlgorithms.SearchingAlgorithms.StringSearch
{
    public class BoyerMooreSearch
    {
        List<int> Search_BasedOnBadCharacterShiftOnly(string text, string subString)
        {
            List<int> indexes = new List<int>();

            /* Preprocessing step for subString */
            Dictionary<char, int> subStringMap = MapCharToLastIndex(subString);

            int i = 0;  /* Is the index over text. */
            while (i < text.Length - subString.Length)
            {
                int j = subString.Length - 1; /* Starting index over subString - notice that we match the string backwards.*/
                while (text[i + j] == subString[j]) /* Continue moving backward on subString as long as it matches the text.*/
                {
                    j--;
                }

                if (j < 0) /* this means a match is found. */
                {
                    indexes.Add(i); /* Add the starting index of the text, from which a match for subString is found. */

                    if (i + subString.Length < text.Length) /* Get the next character in text*/
                    {
                        char nextChar = text[i + subString.Length];
                        int lastIndexOfNextCharInSubString = subStringMap.ContainsKey(nextChar) ? subStringMap[nextChar] : -1;
                        i = i + subString.Length - lastIndexOfNextCharInSubString;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                }
                else /* this means a mis match is observed. The mismatched character in text is called a BadCharacter */
                {
                    char nextChar = text[i + j];
                    int lastIndexOfNextCharInSubString = subStringMap.ContainsKey(nextChar) ? subStringMap[nextChar] : -1;
                    i = Math.Max(j - lastIndexOfNextCharInSubString, 1);
                }
            }

            return indexes;
        }

        /// <summary>
        /// Maps every character in the given string to its last index in the string. 
        /// An example use is Boyer-Moore search algorithm for re-alignment of the pattern being searched for when a bad character is found in in the string that is being searched in.
        /// </summary>
        /// <returns>A mapping of all the characters in the given string to their last index in the string. </returns>
        public static Dictionary<char, int> MapCharToLastIndex(string text)
        {
            Dictionary<char, int> indexes = new Dictionary<char, int>();

            for (int i = 0; i < text.Length; i++)
            {
                if (indexes.ContainsKey(text[i]))
                {
                    indexes[text[i]] = i;
                }
                else
                {
                    indexes.Add(text[i], i);
                }
            }
            return indexes;
        }
    }
}