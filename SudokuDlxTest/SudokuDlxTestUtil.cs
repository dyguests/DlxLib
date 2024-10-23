namespace SudokuDlxLibTest
{
    public static class SudokuDlxTestUtil
    {
        public static string ToDisplay(this int[,] matrix)
        {
            var row = matrix.GetLength(0);
            var col = matrix.GetLength(1);
            var result = "";
            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < col; j++)
                {
                    result += matrix[i, j];
                }

                result += "\n";
            }

            return result;
        }
    }
}