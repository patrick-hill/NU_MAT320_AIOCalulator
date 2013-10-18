﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator.CalcObjects
{
    public class Matrix
    {
        private string name = null;
        private double[,] matrix;
        private int matrixRow, matrixColumn;

        public Matrix(int row, int column)
        {
            matrixRow = row;
            matrixColumn = column;
            matrix = new double[matrixRow, matrixColumn];
        }

        public void AddCell(int row, int column, double value)
        {
            matrix[row, column] = value;
        }

        public double GetCell(int row, int column)
        {
            return matrix[row, column];
        }

        public int GetRows()
        {
            return matrixRow;
        }

        public int GetColumns()
        {
            return matrixColumn;
        }

        public double[,] getMatrix()
        {
            return matrix;
        }

        public void SetName(String str)
        {
            name = str;
        }

        public String GetName()
        {
            return name;
        }

        public override String ToString()
        {
            if (name.Equals(null))
                name = "none";
            return name;
        }
    }
}