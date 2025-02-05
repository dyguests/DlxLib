using System.Collections.Generic;
using DlxLib.Beans;

namespace DlxLib.Instrumentations
{
    public class StepLimitInstrumentation : Instrumentation
    {
        private readonly int _maxStep;
        private int _step;


        public StepLimitInstrumentation(int maxStep = 10000)
        {
            _maxStep = maxStep;
        }

        public override void OnSearchStart(Stack<Node> stack)
        {
            base.OnSearchStart(stack);
            _step++;
        }

        public override bool ShouldInterrupt()
        {
            return _step >= _maxStep;
        }
    }
}