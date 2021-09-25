using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib;
using SudokuDlxLib;
using SudokuGeneratorLib.Utils;
using SudokuLib;

namespace SudokuGeneratorLib
{
    public static class SudokuGenerator
    {
        private static readonly Random Random = new Random();

        public static Sudoku GenerateNormalSudoku(int holeCount, int advancedHoleCount = 0)
        {
            var solutionNumbers = GenerateSolution();
            var initNumbers = (int[]) solutionNumbers.Clone();
            HollowMatchNormalSudoku(initNumbers, holeCount, advancedHoleCount);
            return new Sudoku
            {
                initNumbers = initNumbers,
                solutionNumbers = solutionNumbers,
                rules = new Rule[]
                {
                    new NormalRule(),
                },
            };
        }

        /// <summary>
        /// 生成杀手数独
        /// </summary>
        /// <param name="holeCount"></param>
        /// <param name="cageMaxSize"></param>
        /// <param name="cageMaxCoverCount"></param>
        /// <returns></returns>
        public static Sudoku GenerateKillerSudoku(int holeCount, int cageMaxSize, int cageMaxCoverCount)
        {
            return GenerateKillerSudoku(holeCount, 2, cageMaxSize, cageMaxCoverCount);
        }

        /// <summary>
        /// 生成杀手数独
        /// </summary>
        /// <param name="holeCount">挖洞数量</param>
        /// <param name="cageMinSize">笼子最小size</param>
        /// <param name="cageMaxSize">笼子最大size</param>
        /// <param name="cageMaxCoverCount">笼子最大覆盖数量</param>
        /// <returns></returns>
        private static Sudoku GenerateKillerSudoku(int holeCount, int cageMinSize, int cageMaxSize, int cageMaxCoverCount)
        {
            var solutionNumbers = GenerateSolution();
            var initNumbers = (int[]) solutionNumbers.Clone();
            var cageRule = GenerateKillerRule(initNumbers, solutionNumbers, cageMinSize, cageMaxSize);
            HollowMatchKillerSudoku(initNumbers, solutionNumbers, cageRule, holeCount);
            CleanCages();
            ReducedCages();

            return new Sudoku
            {
                initNumbers = initNumbers,
                solutionNumbers = solutionNumbers,
                rules = new Rule[]
                {
                    new NormalRule(),
                    cageRule
                },
            };

            void CleanCages()
            {
                cageRule.cages = cageRule.cages.Where(cage => cage.indexes.Any(index => initNumbers[index] == 0)).ToArray();
            }

            void ReducedCages()
            {
                var removableCages = new HashSet<CageRule.Cage>(cageRule.cages);
                while (cageRule.cages.Select(cage => cage.indexes.Length).Sum() > cageMaxCoverCount)
                {
                    var hasCageRemoved = false;

                    var orderCages = removableCages.OrderBy(cage => Random.Next());
                    foreach (var cage in orderCages)
                    {
                        var tmpCages = cageRule.cages.ToList();
                        tmpCages.Remove(cage);

                        var sudoku = new Sudoku
                        {
                            initNumbers = initNumbers,
                            solutionNumbers = solutionNumbers,
                            rules = new Rule[]
                            {
                                new NormalRule(),
                                new CageRule
                                {
                                    cages = tmpCages.ToArray(),
                                },
                            },
                        };

                        var matrix = SudokuDlxUtil.SudokuToMatrix(sudoku);
                        var solutions = Dlx.Solve(
                                matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns,
                                new Dlx.UpToOneInstrumentation()
                            )
                            .ToArray();
                        if (solutions.Length != 1)
                        {
                            removableCages.Remove(cage);
                            continue;
                        }

                        removableCages.Remove(cage);
                        cageRule.cages = tmpCages.ToArray();
                        hasCageRemoved = true;
                        break;
                    }

                    if (hasCageRemoved)
                    {
                        continue;
                    }

                    break;
                }
            }
        }

        public static Sudoku GenerateDiagonalSudoku()
        {
            // empty diagonal sudoku
            var sudoku = new Sudoku
            {
                initNumbers = new[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                },
                rules = new Rule[]
                {
                    new NormalRule(),
                    new DiagonalRule(),
                },
            };
            var matrix = SudokuDlxUtil.SudokuToMatrix(sudoku);
            matrix.matrix.ShuffleDimension0();
            var solutions = Dlx.Solve(matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns).ToArray();

            return sudoku;
        }

        /// <summary>
        /// 新建一个numbers迷底
        /// </summary>
        /// <returns></returns>
        public static int[] GenerateSolution()
        {
            var board = new int[9, 9];
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }

            Shuffle(board);

            return board.Flatten();
        }

        private static CageRule GenerateKillerRule(int[] initNumbers, int[] solutionNumbers, int cageMinSize, int cageMaxSize)
        {
            var order = Enumerable.Range(0, 9 * 9).OrderBy(index => Random.Next());
            var unusedIndexes = new HashSet<int>(Enumerable.Range(0, 9 * 9));

            var cages = new List<CageRule.Cage>();
            foreach (var index in order)
            {
                var cage = GenerateCage(index, cageMinSize, cageMaxSize);
                if (cage != null)
                {
                    cages.Add(cage.Value);
                }
            }

            return new CageRule
            {
                cages = cages.ToArray(),
            };

            CageRule.Cage? GenerateCage(int startIndex, int minSize, int maxSize)
            {
                if (!unusedIndexes.Contains(startIndex)) return null;

                var cageIndexes = new HashSet<int> {startIndex};

                var size = Random.Next(minSize, maxSize + 1);
                while (cageIndexes.Count < size)
                {
                    var nearIndexFound = false;
                    var nearIndexes = GetNearIndexes();
                    foreach (var nearIndex in nearIndexes)
                    {
                        if (cageIndexes.Select(cageIndex => solutionNumbers[cageIndex]).Any(number => number == solutionNumbers[nearIndex]))
                        {
                            continue;
                        }

                        cageIndexes.Add(nearIndex);
                        nearIndexFound = true;
                        break;
                    }

                    if (nearIndexFound)
                    {
                        continue;
                    }

                    break;
                }

                if (cageIndexes.Count < minSize)
                {
                    return null;
                }

                unusedIndexes.ExceptWith(cageIndexes);
                return new CageRule.Cage
                {
                    sum = solutionNumbers.Where((number, index) => cageIndexes.Contains(index)).Sum(),
                    indexes = cageIndexes.ToArray(),
                };

                IEnumerable<int> GetNearIndexes()
                {
                    return cageIndexes.Select(i => (i % 9, i / 9))
                        .SelectMany(tuple =>
                        {
                            var list = new HashSet<(int, int)>();

                            if (tuple.Item1 > 0)
                            {
                                list.Add((tuple.Item1 - 1, tuple.Item2));
                            }

                            if (tuple.Item1 < 9 - 1)
                            {
                                list.Add((tuple.Item1 + 1, tuple.Item2));
                            }

                            if (tuple.Item2 > 0)
                            {
                                list.Add((tuple.Item1, tuple.Item2 - 1));
                            }

                            if (tuple.Item2 < 9 - 1)
                            {
                                list.Add((tuple.Item1, tuple.Item2 + 1));
                            }

                            return list;
                        })
                        .Select(tuple => tuple.Item1 + tuple.Item2 * 9)
                        .Where(index => unusedIndexes.Contains(index))
                        .Where(index => !cageIndexes.Contains(index))
                        .OrderBy(index => Random.Next());
                }
            }
        }


        private static void Shuffle(int[,] board, int times = 15)
        {
            for (int i = 0; i < times; i++)
            {
                ShuffleOnce(board);
            }
        }

        private static void ShuffleOnce(int[,] board)
        {
            // 任选两个不同的数字，交换两种数字的位置
            var chooseNumber = -1;
            var replacingNumber = -1;
            while (replacingNumber == chooseNumber)
            {
                chooseNumber = Random.Next(1, 9 + 1);
                replacingNumber = Random.Next(1, 9 + 1);
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == chooseNumber)
                    {
                        board[i, j] = replacingNumber;
                    }
                    else if (board[i, j] == replacingNumber)
                    {
                        board[i, j] = chooseNumber;
                    }
                }
            }

            // 交换任意两行（同宫内）
            // 即1~3(4~6,7~9)行内部交换，但是不会出现第1行与第4行交换
            var sizeOfInnerMatrix = 3;
            var chooseRowIndex = -1;
            var replacingRowIndex = -1;
            while (chooseRowIndex == replacingRowIndex)
            {
                chooseRowIndex = Random.Next(0, sizeOfInnerMatrix);
                replacingRowIndex = Random.Next(0, sizeOfInnerMatrix);
            }

            var multiplier = Random.Next(0, sizeOfInnerMatrix);
            chooseRowIndex += (multiplier * sizeOfInnerMatrix);
            replacingRowIndex += (multiplier * sizeOfInnerMatrix);

            for (var i = 0; i < board.GetLength(1); i++)
            {
                var tmp = board[chooseRowIndex, i];
                board[chooseRowIndex, i] = board[replacingRowIndex, i];
                board[replacingRowIndex, i] = tmp;
            }

            //矩阵转置
            board.Transpose();
        }

        private static void HollowMatchKillerSudoku(int[] initNumbers, int[] solutionNumbers, CageRule cageRule, int holeCount)
        {
            HollowMatchNormalSudokuWithCheck(initNumbers, holeCount, index =>
            {
                var sudoku = new Sudoku
                {
                    initNumbers = initNumbers,
                    rules = new Rule[]
                    {
                        new NormalRule(),
                        cageRule,
                    }
                };

                var matrix = SudokuDlxUtil.SudokuToMatrix(sudoku);
                var solutions = Dlx.Solve(matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns).ToArray();
                return solutions.Length == 1;
            });
        }

        /// <summary>
        /// 挖洞
        /// 
        /// 要保证挖完洞后，基于普通数独规则有唯一解
        /// </summary>
        /// <param name="initNumbers"></param>
        /// <param name="holeCount"></param>
        /// <param name="advancedHoleCount"></param>
        private static void HollowMatchNormalSudoku(int[] initNumbers, int holeCount, int advancedHoleCount)
        {
            var hollowedCount = HollowMatchNormalSudokuWithCheck(initNumbers, holeCount, index =>
            {
                return initNumbers.Where(i => i % 9 == index % 9
                                              || i / 9 == index / 9
                                              || (i % 9 / 3 == index % 9 / 3 && i / 9 / 3 == index / 9 / 3))
                           .Distinct()
                           .ToArray().Length == 8;
            });
            int holeCount1 = advancedHoleCount + holeCount - hollowedCount;
            HollowMatchNormalSudokuWithCheck(initNumbers, holeCount1, index =>
            {
                var sudoku = new Sudoku
                {
                    initNumbers = initNumbers,
                    rules = new Rule[]
                    {
                        new NormalRule(),
                    }
                };

                var matrix = SudokuDlxUtil.SudokuToMatrix(sudoku);
                var solutions = Dlx.Solve(matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns).ToArray();
                return solutions.Length == 1;
            });
        }

        private static int HollowMatchNormalSudokuWithCheck(int[] initNumbers, int holeCount, Func<int, bool> validator)
        {
            var removedItems = 0;
            foreach (var index in Enumerable.Range(0, 9 * 9).OrderBy(i => Random.Next()))
            {
                if (removedItems >= holeCount)
                {
                    break;
                }

                var temp = initNumbers[index];
                if (temp == 0)
                {
                    continue;
                }

                initNumbers[index] = 0;
                if (validator(index))
                {
                    removedItems++;
                }
                else
                {
                    initNumbers[index] = temp;
                }
            }

            return removedItems;
        }
    }
}