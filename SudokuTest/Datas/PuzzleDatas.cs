using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;
using Rules;
using SudokuTest.Base;
using Utils;

namespace SudokuTest.Datas
{
    public static class PuzzleDatas
    {
        public static readonly Puzzle[] BaseDatas =
        {
            CreatePuzzle("BCHFjEdGifJeDiGBHcdiGBCHFEjIGBCHfJDeJeDIGBCfHchFjEDIbgHFJedigcBEDiGBCHjFGbcHFjeID"),
            CreatePuzzle("idBgehcfjfJcDbIEhGhGEJcFbIdCiJHDBgEfBhDFgEJCiEFgiJcdBhgCFbIjhdeJbIeHdFgcDehCFgiJB"),
        };

        public static readonly Puzzle[] KillerDatas =
        {
            CreatePuzzle("BCHFjEdGifJeDiGBHcdiGBCHFEjIGBCHfJDeJeDIGBCfHchFjEDIbgHFJedigcBEDiGBCHjFGbcHFjeID", "killer:10,0,1;17,79,80"),
        };

        private static Puzzle CreatePuzzle(string numberStrings, params string[] ruleStrings)
        {
            return new Puzzle
            {
                numbers = NumberStrings2Numbers(numberStrings),
                rules = RuleStrings2Rules(ruleStrings),
            };
        }

        private const byte s0 = (byte) 'A';

        private const byte h0 = (byte) 'a';

        private static Vector3Int[] NumberStrings2Numbers(string numberStrings)
        {
            var numbers = new Vector3Int[9 * 9];
            for (var index = 0; index < numberStrings.Length; index++)
            {
                var number = new Vector3Int();
                char c = numberStrings[index];
                if (c < h0)
                {
                    byte v = (byte) (c - s0);
                    number.x = v;
                    number.y = v;
                    number.z = v;
                }
                else
                {
                    byte v = (byte) (c - h0);
                    number.z = v;
                }

                numbers[index] = number;
            }

            return numbers;
        }

        private static List<Rule> RuleStrings2Rules(string[] ruleStrings)
        {
            if (ruleStrings == null || ruleStrings.Length == 0)
            {
                return null;
            }

            var rules = new List<Rule>();
            foreach (var ruleString in ruleStrings)
            {
                rules.Add(RuleString2Rule(ruleString));
            }

            return rules;
        }

        private static Rule RuleString2Rule(string ruleString)
        {
            var parts = ruleString.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidDataException();
            }

            switch (parts[0])
            {
                case "killer":
                    return ConvertToCageRule(parts[1]);
                default:
                    throw new InvalidDataException();
            }
        }

        private static Rule ConvertToCageRule(string ruleString)
        {
            return new CageRule
            {
                cages = ruleString.Split(';').Select(cageString => ConvertToCage(cageString)).ToList(),
            };
        }

        private static CageRule.Cage ConvertToCage(string cageString)
        {
            var parts = cageString.Split(',').Select(part => Convert.ToInt32(part)).ToArray();
            return new CageRule.Cage
            {
                sum = parts[0],
                indexes = parts.SubArray(1, parts.Length - 1),
            };
        }
    }
}