﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    enum Color  //перечисление для цветов фигур
    {
        none,
        white,
        black
    }

    static class ColorMethods  //метод для переключения цвета
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.black) return Color.white;
            if(color == Color.white) return Color.black;
            return Color.none;
        }
    }
}
