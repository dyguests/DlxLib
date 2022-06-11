using System;
using System.IO;
using PuzzleLib;
using PuzzleLib.Rules;
using PuzzleLib.UnityEngine;
using SudokuConverterLib.Tables;

namespace SudokuConverterLib
{
    public static class SudokuConverter
    {
        public static PuzzleTable ToPuzzleTable(Sudoku sudoku)
        {
            var stream = new MemoryStream();
            // index:0，存放puzzle.size。其中x是byte2进制左4位，y是二进制右4位。注：一个byte最多放8bit，因此 0<x,y<=16
            stream.WriteByte(SizeToByte(sudoku.size));
            // index:1，存放puzzle.boxSize。其中x是byte2进制左4位，y是二进制右4位。注：一个byte最多放8bit，因此 0<x,y<=16
            stream.WriteByte(SizeToByte(sudoku.boxSize));
            // TODO 先不用 bit stream 了，没有支持的类; 注意：目前每个tile用了1byte来存储，不过还可以优化成 5bit。
            // index:2~(1+sudoku.solutionNumbers.Count),共sudoku.solutionNumbers.Count个byte。存放initNumbers&solutionNumbers;
            var initNumbers = sudoku.initNumbers;
            var solutionNumbers = sudoku.solutionNumbers;
            for (var i = 0; i < solutionNumbers.Length; i++)
            {
                // 这里可以存放value=0~15。如果是1~16还需要-1
                var value = (initNumbers[i] << 4) | solutionNumbers[i];
                stream.WriteByte((byte) value);
            }

            var bytes = stream.ToArray();
            var content = Convert.ToBase64String(bytes);
            // Debug.Log("content:\n" + content);

            return new()
            {
                Id = 1,
                CategoryId = 1,
                Name = "样例",
                Content = content,
            };
        }

        public static Sudoku FromPuzzleTable(PuzzleTable puzzleTable)
        {
            var content = puzzleTable.Content;
            byte[] bytes = Convert.FromBase64String(content);
            var stream = new MemoryStream(bytes);
            var size = Byte2Size(stream.ReadByte());
            var boxSize = Byte2Size(stream.ReadByte());
            var sizeLength = size.x * size.y;
            var initNumbers = new int[sizeLength];
            var solutionNumbers = new int[sizeLength];
            for (var i = 0; i < sizeLength; i++)
            {
                var oneByte = stream.ReadByte();
                // 这里可以存放value=0~15。如果是1~16还需要-1
                initNumbers[i] = oneByte >> 4;
                solutionNumbers[i] = oneByte & 0b1111;
            }

            var sudoku = new Sudoku
            {
                size = size,
                boxSize = boxSize,
                initNumbers = initNumbers,
                solutionNumbers = solutionNumbers,
            };
            sudoku.rules.Add(NormalRule.Instance);
            return sudoku;
        }

        // public static NoteTable ToNoteTable(PuzzleTable puzzleTable, SudokuNote note)
        // {
        //     var stream = new MemoryStream();
        //     foreach (var number in note.Numbers)
        //     {
        //         stream.WriteByte((byte) number);
        //     }
        //
        //     foreach (var number in note.Hints)
        //     {
        //         stream.WriteByte((byte) number);
        //     }
        //
        //     var bytes = stream.ToArray();
        //     // var content = Encoding.UTF8.GetString(bytes);
        //     var content = Convert.ToBase64String(bytes);
        //     return new()
        //     {
        //         PuzzleId = puzzleTable.Id,
        //         Content = content,
        //         Time = note.Time,
        //         Completed = note.Completed,
        //     };
        // }
        //
        // public static SudokuNote FromNoteTable(Sudoku puzzle, NoteTable noteTable)
        // {
        //     var sizeLength = puzzle.size.x * puzzle.size.y;
        //     var sudokuNote = new SudokuNote
        //     {
        //         Numbers = new int[sizeLength],
        //         Hints = new int[sizeLength],
        //         Time = noteTable.Time,
        //         Completed = noteTable.Completed,
        //     };
        //     var content = noteTable.Content;
        //
        //     if (!string.IsNullOrEmpty(content))
        //     {
        //         // var bytes = Encoding.UTF8.GetBytes(content);
        //         var bytes = Convert.FromBase64String(content);
        //         var stream = new MemoryStream(bytes);
        //         for (int i = 0; i < sizeLength; i++)
        //         {
        //             sudokuNote.Numbers[i] = stream.ReadByte();
        //         }
        //
        //         for (int i = 0; i < sizeLength; i++)
        //         {
        //             sudokuNote.Hints[i] = stream.ReadByte();
        //         }
        //     }
        //
        //     return sudokuNote;
        // }

        private static Coord Byte2Size(int oneByte)
        {
            var size = new Coord(
                (oneByte >> 4) + 1,
                y: (oneByte & 0b1111) + 1
            );
            return size;
        }

        private static byte SizeToByte(Coord size) => (byte) (((size.x - 1) << 4) | (size.y - 1));
    }
}