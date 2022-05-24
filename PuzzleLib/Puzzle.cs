using System.Collections.Generic;
using System.Linq;
using PuzzleLib.Rules;

namespace PuzzleLib
{
    public abstract class Puzzle
    {
        /// <summary>
        /// 有多少个tile
        /// </summary>
        public Vector2Int size;

        /// <summary>
        /// 有多少个box
        /// </summary>
        public Vector2Int boxSize = Vector2Int.one;

        public int[] initNumbers;
        public int[] solutionNumbers;

        public List<Rule> rules = new();

        public T GetRule<T>() where T : Rule => rules.FirstOrDefault(rule => rule is T) as T;

        /// <summary>
        /// 返回 tile(x,y) 所在的 box(x2,y2)
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <returns></returns>
        public Vector2Int GetBoxCoord(int tileX, int tileY) => new(
            tileX / (size.x / boxSize.x),
            tileY / (size.y / boxSize.y)
        );

        public override string ToString()
        {
            return $"{nameof(size)}: {size}, {nameof(boxSize)}: {boxSize}, {nameof(initNumbers)}: {initNumbers}, {nameof(solutionNumbers)}: {solutionNumbers}, {nameof(rules)}: {rules}";
        }
    }
}