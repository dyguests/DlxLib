// using SimpleSQL;

namespace SudokuConverterLib.Tables
{
    public class PuzzleTable
    {
        /*[PrimaryKey]*/ public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }

        /// <summary>
        /// 压缩后的数据
        /// </summary>
        public string Content { get; set; }

        public override string ToString()
        {
            return $"{Id}, {CategoryId}, \"{Name}\", \"{Content}\"";
        }
    }
}