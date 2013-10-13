﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;
using System.Collections;
using System.Diagnostics;
using NumericalCalculator.Methods;

namespace NumericalCalculator
{
    class Calculator
    {
        public String log = "";
        private MAT320_AIO_Calulator gui;

        public Calculator(MAT320_AIO_Calulator gui)
        {
            this.gui = gui;
        }
        
        public void Calculate(String method, String function, String functionDer, String range, String _tolerance)
        {
            /// Parse out e and exponenets for NCalc
            function = parseSpecialCases(function);

            /// Add Power & Exp checks here ///
            Expression exp = new Expression(function);
            Expression expDer = null;
            String[] ranges = range.Split(',');
            double a = double.Parse(ranges[0]);
            double b = double.Parse(ranges[1]);
            double tolerance = double.Parse(_tolerance);

            // Find & run method
            switch (method)
            {
                case "Bisection":
                    Bisection bis = new Bisection();
                    bis.Evaluate(exp, a, b, tolerance);
                    setLog(bis.log);
                    break;
                case "Regula Falsi":
                    RegulaFalsi rf = new RegulaFalsi();
                    rf.Evaluate(exp, a, b, tolerance);
                    setLog(rf.log);
                    break;
                case "Newtons Method":
                    if (functionDer.Equals(""))
                    {
                        log += "ERROR: DERIVATION NEEDED!" + "\r\n";
                        setLog(log);
                        break;
                    }
                    expDer = new Expression(parseSpecialCases(functionDer));
                    Newton nt = new Newton();
                    nt.Evaluate(exp, expDer, a, b, tolerance);
                    setLog(nt.log);
                    break;
            }
            //setLog(log);
        }

        public String parseSpecialCases(String func)
        {
            char[] types = new char[2] { 'e', '^' };

            for (int i = 0; i < types.Length; i++)
            {
                func = FunctionReplacement(types[i], func);
            }
            return func;
        }

        private String FunctionReplacement(char type, String func)
        {
            // Examples:
            // x - e^-x + 0 && x - e^5 + 0
            // 4^-x && x^-2x + 5
            if (func.Contains(type))
            {
                /// Replaces each type occurance with needed format
                while (func.Contains(type))
                {
                    // substring needed sub-function
                    String subFunc = getSubstringFunction(type, func);

                    // get exponent
                    String exponent = getSubFunctionExponent(subFunc);
                    
                    String expBase = "";
                    if(type.Equals('^'))
                        expBase = getSubFunctionExpBase(subFunc);

                    // Build & Substitue new substring
                    String newSubFunc = replaceSubFunction(type, subFunc, exponent, expBase);
                    func = func.Replace(subFunc, newSubFunc);

                    // log
                    log += "Replaced: " + subFunc + "\t" + " with: " + newSubFunc + "\r\n";
                    //setLog(log);
                }
            }
            return func;
        }

        private String getSubstringFunction(char type, String func)
        {
            int startIndex = func.IndexOf(type);
            int endIndex = startIndex;

            // Find start/end indices
            while (startIndex >= 0 && !func[startIndex].Equals(' '))
                startIndex--;
            startIndex++; // moves from space
            while (endIndex < func.Length && !func[endIndex].Equals(' '))
                endIndex++;
            endIndex--; // moves from space
            return func.Substring(startIndex, (endIndex - startIndex) + 1);
        }

        private String getSubFunctionExponent(string func)
        {
            int expIndex = func.IndexOf('^') + 1;
            return func.Substring(expIndex);
        }

        private String getSubFunctionExpBase(string func)
        {
            int expIndex = func.IndexOf('^');
            return func.Substring(0, expIndex);
        }

        private String replaceSubFunction(char type, String function, String exponent, String expBase)
        {
            if (type.Equals('e'))
                function = "Exp(" + exponent + ")";
            else
            {
                function = "Pow(" + expBase + "," + exponent + ")";
            }
            return function;
        }

        void setLog(String log)
        {
            gui.outputTextBox.Text = log;
        }
    }
}
