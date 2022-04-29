using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace DemoChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess.Chess chess = new Chess.Chess("rnbqkbnr/1p1111p1/8/8/8/8/1P1111P1/RNBQKBNR w KQkq - 0 1");  //тест доски и хода
            while (true)
            {
                Console.WriteLine(chess.fen);
                Console.WriteLine(ChessToAscii(chess));
                foreach(string moves in chess.GetAllMoves())
                    Console.WriteLine(moves + "\n");
                Console.WriteLine();
                Console.Write("> ");
                string move = Console.ReadLine();
                if (move == "") break;
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess.Chess chess)  //вывод шахмат на экран
        {
            string text = "  +-----------------+\n";
            for(int y = 7; y >= 0; y--)
            {
                text += y + 1;  //горизонталь(число)
                text += " | ";

                for (int x = 0; x < 8; x++)  // вертикаль
                    text += chess.GetFigureAt(x, y) + " ";
                text += "|\n";
            }  
            text += "  +-----------------+\n";
            text += "    a b c d e f g h\n";
            return text;
        }
    }
}
