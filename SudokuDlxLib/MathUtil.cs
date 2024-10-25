using System;
using System.Collections.Generic;

namespace SudokuDlxLib
{
    [Obsolete]
    public class MathUtil
    {
        // 获取所有排列的函数
        public static IEnumerable<int[]> GetPermutations(int[] combination)
        {
            return Permute(combination, 0);

            // 递归生成排列，使用 IEnumerable<int[]>
            static IEnumerable<int[]> Permute(int[] combination, int start)
            {
                if (start >= combination.Length)
                {
                    // 当排列完成时，yield return 当前排列的副本
                    yield return (int[])combination.Clone();
                }
                else
                {
                    for (var i = start; i < combination.Length; i++)
                    {
                        // 交换元素
                        Swap(ref combination[start], ref combination[i]);
                        // 递归生成排列
                        foreach (var perm in Permute(combination, start + 1))
                        {
                            yield return perm;
                        }

                        // 回溯交换回原位
                        Swap(ref combination[start], ref combination[i]);
                    }
                }
            }

            // 交换数组中的两个元素
            static void Swap(ref int a, ref int b) => (a, b) = (b, a);
        }
    }
}