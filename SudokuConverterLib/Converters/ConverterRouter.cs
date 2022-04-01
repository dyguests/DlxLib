using System.IO;
using SudokuLib;

namespace SudokuConverterLib.Converters
{
    public static class ConverterRouter
    {
        public static IRuleConverter GetConverter(Rule rule)
        {
            switch (rule)
            {
                case NormalRule _:
                    return NormalRuleConverter.GetInstance();
                case CageRule _:
                    return CageRuleConverter.GetInstance();
                case DiagonalRule _:
                    return DiagonalRuleConverter.GetInstance();
                default:
                    throw new InvalidDataException();
            }
        }
    }
}