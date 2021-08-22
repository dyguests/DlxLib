using System.Collections.Generic;
using Rules;
using SudokuTest.Base;

namespace Entities
{
    public class Puzzle
    {
        /// <summary>
        /// number.x 初始数字
        /// number.y 当前数字 这个字段不用了，放到note.numbers.EACH 中去了。
        /// number.z 期望数字
        /// </summary>
        public Vector3Int[] numbers;

        /// <summary>
        /// 额外规则
        /// </summary>
        public List<Rule> rules;
    }
}