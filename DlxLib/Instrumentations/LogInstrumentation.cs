using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.Beans;

namespace DlxLib.Instrumentations
{
    public class LogInstrumentation : Instrumentation
    {
        #region Instrumentation

        public override void OnSearchStart(Stack<Node> stack)
        {
            base.OnSearchStart(stack);
            Console.WriteLine(string.Join(",", stack.Select(node => node.Row).Reverse()));
        }

        #endregion
    }
}