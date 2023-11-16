using System.Text;

namespace CodeTest.Helpers
{
    public static class GameHelper
    {
        public static bool[,]? ParseGameBoard(this string input)
        {
            var lines = input
                .Replace(" ", string.Empty)
                .Replace("\t", string.Empty)
                .Split(Environment.NewLine)
                .ToArray();

            var width = lines.Max(x => x.Length);
            var height = lines.Length;
            if (width < 2 || height < 2 || lines.Any(x => x.Length != width) || lines.Any(x => x.Any(c => c != '0' && c != '1')))
            {
                return null;
            }

            var result = new bool[height, width];
            for (var i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = lines[i][j] == '1';
                }
            }

            return result;
        }

        public static string GameBoardToString(this bool[,] board)
        {
            var result = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                result.AppendJoin(' ', Enumerable.Range(0, board.GetLength(1)).Select(j => board[i, j] ? '1' : '0').ToArray());
                result.AppendLine();
            }

            return result.ToString();
        }

        public static bool GetCellState(this bool[,] board, int i, int j)
        {
            var currentState = board[i, j];
            var hasUpper = i != 0;
            var hasLower = i + 1 != board.GetLength(0);
            var hasLeft = j != 0;
            var hasRight = j + 1 != board.GetLength(1);

            var neighborSum =
                (hasUpper && hasLeft && board[i - 1, j - 1] ? 1 : 0)
                + (hasUpper && board[i - 1, j] ? 1 : 0)
                + (hasUpper && hasRight && board[i - 1, j + 1] ? 1 : 0)
                + (hasLeft && board[i, j - 1] ? 1 : 0)
                + (hasRight && board[i, j + 1] ? 1 : 0)
                + (hasLower && hasLeft && board[i + 1, j - 1] ? 1 : 0)
                + (hasLower && board[i + 1, j] ? 1 : 0)
                + (hasLower && hasRight && board[i + 1, j + 1] ? 1 : 0);

            return neighborSum == 3 || currentState && neighborSum == 2;
        }
    }
}
