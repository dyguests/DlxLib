using System.Collections.Generic;
using DlxLib.Beans;

namespace DlxLib.Instrumentations
{
    public interface IInstrumentation
    {
        void OnSolutionFound();

        void OnSearchStart(Stack<Node> stack);
    }

    /// <summary>
    /// 插桩逻辑
    ///
    /// 如手动取消、超过两个结果不再检查
    /// </summary>
    public abstract class Instrumentation : IInstrumentation
    {
        #region IInstrumentation

        public virtual void OnSolutionFound() { }
        public virtual void OnSearchStart(Stack<Node> stack) { }

        #endregion

        private bool isCancelled;

        protected void Cancel()
        {
            isCancelled = true;
        }

        public bool ShouldInterrupt()
        {
            return isCancelled;
        }
    }
}