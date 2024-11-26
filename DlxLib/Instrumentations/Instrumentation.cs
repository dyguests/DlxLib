namespace DlxLib.Instrumentations
{
    /// <summary>
    /// 插桩逻辑
    ///
    /// 如手动取消、超过两个结果不再检查
    /// </summary>
    public abstract class Instrumentation
    {
        private bool isCancelled;

        protected void Cancel()
        {
            isCancelled = true;
        }

        public bool IsCancelled()
        {
            return isCancelled;
        }

        public virtual void NotifySolutionIncrease()
        {
        }
    }
}