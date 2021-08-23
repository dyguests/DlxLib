namespace Rules
{
    public abstract class Rule
    {
        public abstract class Data
        {
        }


        /// <summary>
        /// 种类
        ///
        /// 如 笼子，大小，等
        /// </summary>
        /// <returns></returns>
        public abstract string GetCategory();

        /// <summary>
        /// 当前规则相关的所有index.
        /// </summary>
        /// <returns></returns>
        public abstract int[] GetIndexes();

        /// <summary>
        /// 将信息压缩，用于存到db中
        /// </summary>
        /// <returns></returns>
        public abstract string Zip();
    }
}