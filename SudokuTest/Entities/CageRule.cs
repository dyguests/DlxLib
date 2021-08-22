using System.Collections.Generic;
using System.Linq;

namespace Rules
{
    public class CageRule : Rule
    {
        public List<Cage> cages;

        public override string GetCategory()
        {
            return "cage";
        }

        public override int[] GetIndexes()
        {
            var result = new List<int>();
            if (cages != null)
            {
                foreach (var cage in cages)
                {
                    result.AddRange(cage.indexes);
                }
            }

            return result.Distinct().ToArray();
        }

        public override string Zip()
        {
            return string.Join(";", cages.Select(ZipCage));
        }

        private string ZipCage(Cage cage)
        {
            return cage.sum + "," + string.Join(",", cage.indexes);
        }

        /// <summary>
        /// 笼子的信息
        /// </summary>
        public struct Cage
        {
            /// <summary>
            /// 笼子的和
            /// </summary>
            public int sum;

            /// <summary>
            /// 笼子的格子
            /// 每个格子为 [m,n]=>m+n*9
            /// </summary>
            public int[] indexes;

            public bool Contains(int index)
            {
                return indexes.Contains(index);
            }
        }
    }
}