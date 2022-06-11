using System;

namespace PuzzleLib.Entities
{
    public abstract class Note : ICloneable, IUpdatable
    {
        /// <summary>
        /// 这里存放的是填入的数字
        /// </summary>
        public int[] Numbers { get; set; }

        /// <summary>
        /// 9*9*9
        /// 为1则表示有可能数字i的提示，
        /// 
        /// 否则(0)没提示。
        /// </summary>
        public int[] Hints { get; set; }

        public int[] Corners { get; set; }

        public int[] Colors { get; set; }

        public int Time { get; set; }
        public bool Completed { get; set; }
        public string Memo { get; set; }

        public abstract object Clone();

        public abstract void Update(object obj);
    }
}