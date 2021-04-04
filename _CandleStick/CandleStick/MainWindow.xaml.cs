using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CandleStick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartFileTranslation_Click(object sender, RoutedEventArgs e)
        {
            List<string> CSVLines = System.IO.File.ReadAllLines(@"C:\Dev\CandleStick\NewNQ.CSV").ToList();
            List<string> FinalResults = new List<string>();
            List<CandleStick> StackList = new List<CandleStick>();

            // checks to make sure it has 4 column
            if (CSVLines[0].Split(',').ToList().Count >= 5 && CSVLines.Count() > 2) //at least have header, stick1, stick2
            {
                string outputFolder = @"C:\Dev\CandleStick";
                int NumOutputFiles = Directory.EnumerateFiles(outputFolder).Where(f => Regex.IsMatch(f, "output")).Count();
                string FileName = "output" + (NumOutputFiles + 1) + ".CSV";
                string outputFilePath = System.IO.Path.Combine(@"C:\Dev\CandleStick\", FileName);
                //File.AppendAllText(outputFilePath, (CSVLines[0].ToString()) + Environment.NewLine); //write header to csv

                int LineCount = CSVLines.Count() - 1;
                int index = 1;
                CandleStick FirstCS;
                CandleStick SecondCS;
                FirstCS = new CandleStick(CSVLines[LineCount]);
                SecondCS = new CandleStick(CSVLines[LineCount - 1]);
                CandleStick ThirdCS;
                //bool FirstSecondCombined = false;

                //first and second stick is special
                while (Trend(FirstCS, SecondCS) == 0 && LineCount - index > 2) // NEED TO COMBINE very FIRST and second stick
                {
                    //FirstSecondCombined = true;
                    index++;

                    FirstCS = Combine(TrendIsUpOnlyCheckHigh(FirstCS, SecondCS)? true : false, FirstCS, SecondCS);
                    SecondCS = new CandleStick(CSVLines[LineCount - index]);
                }

                //Do not combine 1 and 2, add 1 to results
                FinalResults.Add(FirstCS.RowDataString);
                //FirstCS = SecondCS;
                //if (FirstSecondCombined)
                //{
                //    index++;
                //    SecondCS = new CandleStick(CSVLines[LineCount - index]);
                //}

                ThirdCS = new CandleStick(CSVLines[LineCount - index - 1]);

                while (LineCount - index >= 2)
                {
                    while (Trend(SecondCS, ThirdCS) == 0) // second and third can be combined
                    {
                        index++;
                        //when growing trend, open = newLow, close = newHigh, green stick increasing 
                        if (LineCount - index < 2) break;
                        SecondCS = Trend(FirstCS, SecondCS) == 1 ? Combine(true, SecondCS, ThirdCS) : Combine(false, SecondCS, ThirdCS);
                        ThirdCS = new CandleStick(CSVLines[LineCount - index - 1]);

                        //StackList.Add(new CandleStick(CombinedCS));
                    }

                    //evaluate last 2 items
                    if (LineCount - index <= 2)
                    {
                        if(Trend(SecondCS, ThirdCS) == 0)
                        {
                            SecondCS = Trend(FirstCS, SecondCS) == 1 ? Combine(true, SecondCS, ThirdCS) : Combine(false, SecondCS, ThirdCS);
                            FinalResults.Add(SecondCS.RowDataString);
                        }
                        else
                        {
                            FinalResults.Add(SecondCS.RowDataString);
                            FinalResults.Add(ThirdCS.RowDataString);
                        }
                        break;
                    }
                    FinalResults.Add(SecondCS.RowDataString);

                    index++;
                    FirstCS = SecondCS;
                    SecondCS = ThirdCS;
                    ThirdCS = new CandleStick(CSVLines[LineCount - index - 1]);
                }

                FinalResults.Add(CSVLines[0]);
                FinalResults.Reverse();
                File.AppendAllText(outputFilePath, string.Join(Environment.NewLine, FinalResults));
            }
        }

        private int Trend(CandleStick First, CandleStick Second)
        {
            //1 => upward
            // -1 => downward
            // 0
            if (Second.High > First.High && Second.Low > First.Low) return 1; //going upward
            if (Second.High < First.High && Second.Low < First.Low) return -1;// going downward
            return 0;
        }

        private bool TrendIsUpOnlyCheckHigh(CandleStick First, CandleStick Second)
        {
            //1 => upward
            // -1 => downward
            if (Second.High >= First.High) return true; //going upward
            return false;// going downward
        }

        private CandleStick Combine(bool IsUpward, CandleStick _SecondCS, CandleStick _ThirdCS)
        {
            if (IsUpward)
            {
                var NewHigh = Math.Max(_SecondCS.High, _ThirdCS.High);
                var NewLow = Math.Max(_SecondCS.Low, _ThirdCS.Low);
                //when growing trend, open = newLow, close = newHigh, green stick increasing 
                return new CandleStick(_SecondCS.Date + "," + NewLow + "," + NewHigh + "," + NewLow + "," + NewHigh);
            }
            else
            {
                var NewHigh = Math.Min(_SecondCS.High, _ThirdCS.High);
                var NewLow = Math.Min(_SecondCS.Low, _ThirdCS.Low);
                //when growing trend, open = newLow, close = newHigh, green stick increasing 
                return new CandleStick(_SecondCS.Date + "," + NewHigh + "," + NewHigh + "," + NewLow + "," + NewLow);
            }

        }

        class CandleStick
        {
            [DebuggerDisplay("High={High}; Low={Low}")]
            public string Date { get; }
            public double Open { get; }
            public double High { get; }
            public double Low { get; }
            public double Close { get; }
            public string[] RowData { get; }
            public string RowDataString { get; }

            public CandleStick(string _RowData)
            {
                RowDataString = _RowData;
                RowData = _RowData.Split(',');
                Date = RowData.ElementAt(0);
                Open = double.Parse(RowData.ElementAt(1));
                High = double.Parse(RowData.ElementAt(2));
                Low = double.Parse(RowData.ElementAt(3));
                Close = double.Parse(RowData.ElementAt(4));
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            var win = Window.GetWindow(button);
            if (win == null)
                return;

            win.Close();
        }
    }
}
