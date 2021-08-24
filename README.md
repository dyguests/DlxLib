# DlxLib

用DLX算法（跳舞链算法）求解精确匹配(Exact Cover)问题。

## Show


| params | description |
| ---- | ---- |
| input | matrix |
| output | solutions('rows for exact cover' for each solution) |

Exact Cover:

```
-------- begin --------
matrix:
{0}
Solutions:
-------- end --------
```

```
-------- begin --------
matrix:
{1}
Solutions:
Solution:0
-------- end --------
```

```
-------- begin --------
matrix:
{1,0}
{0,1}
{1,1}
{0,0}
Solutions:
Solution:0,1
Solution:2
-------- end --------
```

```
-------- begin --------
matrix:

Solutions:
Solution:
-------- end --------
```

```
-------- begin --------
matrix:
{1,0,0,1,0,0,1}
{1,0,0,1,0,0,0}
{0,0,0,1,1,0,1}
{0,0,1,0,1,1,0}
{0,1,1,0,0,1,1}
{0,1,0,0,0,0,1}
Solutions:
Solution:1,3,5
-------- end --------
```

Secondary columns:

```
-------- begin --------
matrix:
{1,0,1,0}
{0,1,1,0}
{0,1,0,1}
{0,1,0,0}
numPrimaryColumns:2
Solutions:
Solution:0,2
Solution:0,3
-------- end --------
```

Unique check:

```
-------- begin --------
{1,0}
{0,1}
{0,0}
Solution:0,1
Unique:True
-------- end --------
```

```
-------- begin --------
{1,0}
{0,1}
{1,1}
{1,1}
Solution:0,1
Solution:2
Unique:False
-------- end --------
```

Sudoku:

```
-------- begin --------
Puzzle:
..1......
.9.3.84.6
.649.5.8.
2.9731.4.
1.35.492.
45..9..1.
.25.8....
9.8.7.5..
3..25..91
Solution:6,28,31,34,35,38,39,26,2,3,29,32,37,15,19,25,40,41,42,44,45,50,52,46,54,55,56,57,58,59,60,62,63,65,66,68,69,70,10,8,14,71,72,73,75,76,77,78,81,82,83,89,91,85,93,95,96,100,106,107,103,111,97,113,116,118,114,120,123,125,121,126,129,130,133,134,135,136,139,140,141
Answer:
831647259
592318476
764925183
289731645
173564928
456892317
625189734
918473562
347256891
-------- end --------
```

## Notices

- 注意稀疏矩阵是一个十字环形链表。（“十字”、“环形”）

## Todo List

-[x] 可选约束
-[ ] o dict -> stack
-[ ] GetRuleDlxProcessor Singleton

## Reference

- [用DLX解sudoku(数独)](http://blog.gssxgss.me/use-dlx-to-solve-sudoku-1/)
- [taylorjg/DlxLib](https://github.com/taylorjg/DlxLib)
- [Knuth's Algorithm X](https://en.wikipedia.org/wiki/Knuth%27s_Algorithm_X)
- [Wiki Dancing Links](https://en.wikipedia.org/wiki/Dancing_Links)