using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class FigureOnSquare  //"контейнер" для хранения фигур и квадратов
    {
        public Figure figure { get; private set; }
        public Square square { get; private set; }
        public FigureOnSquare(Figure figure, Square square)
        {
            this.figure = figure;
            this.square = square;
        }
    }
}
