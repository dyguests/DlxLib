using System;
using System.Collections.Generic;
using SudokuLib.Rules;
using SudokuLib.Sketchers;

namespace SudokuDlxLib.Rules
{
    public static class RuleDlxUtil
    {
        private static readonly Dictionary<Type, IRuleDlx> _ruleDlxDict = new()
        {
            // 示例: [typeof(StandardRuleSketcher)] = new StandardRuleDlx(),
            [typeof(StandardRuleSketcher)] = new StandardRuleDlx(),
            [typeof(DiagonalRuleSketcher)] = new DiagonalRuleDlx(),
        };

        public static IRuleDlx GetDlx(IRuleSketcher rule)
        {
            var ruleType = rule.GetType();
            if (!typeof(IRuleSketcher).IsAssignableFrom(ruleType))
            {
                throw new ArgumentException($"Type {ruleType.Name} is not a subclass of IRuleSketcher");
            }

            if (_ruleDlxDict.TryGetValue(ruleType, out var ruleDlx))
            {
                return ruleDlx;
            }

            throw new KeyNotFoundException($"No IRuleDlx implementation found for {ruleType.Name}");
        }
    }
}