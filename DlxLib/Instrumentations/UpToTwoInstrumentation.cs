namespace DlxLib.Instrumentations
{
    /// <summary>
    /// 对于求解是否具有唯一解，只取得两个解的情况下，后续就不会再求解也可以确定不具有唯一解了
    /// </summary>
    public class UpToTwoInstrumentation : Instrumentation
    {
        private int numberOfSolutions;

        public override void NotifySolutionIncrease()
        {
            base.NotifySolutionIncrease();
            numberOfSolutions++;
            if (numberOfSolutions >= 2)
            {
                Cancel();
            }
        }
    }
}