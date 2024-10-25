using System;
using System.Collections.Generic;
using SudokuLib.Rules;

namespace SudokuDlxLib.Rules
{
    public static class RuleDlxMapper
    {
        private static readonly Dictionary<Type, IRuleDlx> RuleDlxDict = new()
        {
            // 示例: [typeof(StandardRuleSketcher)] = new StandardRuleDlx(),
            [typeof(StandardRule)] = new StandardRuleDlx(),
            [typeof(DiagonalRule)] = new DiagonalRuleDlx(),
            [typeof(KillerRule)] = new KillerRuleDlx(),
        };

        public static IRuleDlx GetDlx(IRule rule)
        {
            var ruleType = rule.GetType();
            if (!typeof(IRule).IsAssignableFrom(ruleType))
            {
                throw new ArgumentException($"Type {ruleType.Name} is not a subclass of IRuleSketcher");
            }

            if (RuleDlxDict.TryGetValue(ruleType, out var ruleDlx))
            {
                return ruleDlx;
            }

            throw new KeyNotFoundException($"No IRuleDlx implementation found for {ruleType.Name}");
        }
    }
}