namespace DlxLib.Instrumentations
{
    /// <summary>
    /// 只求其中一个解就行
    /// </summary>
    public class UpToOneInstrumentation : Instrumentation
    {
        private int numberOfSolutions;

        public override void OnSolutionFound()
        {
            base.OnSolutionFound();
            numberOfSolutions++;
            if (numberOfSolutions >= 1)
            {
                Cancel();
            }
        }
    }
}