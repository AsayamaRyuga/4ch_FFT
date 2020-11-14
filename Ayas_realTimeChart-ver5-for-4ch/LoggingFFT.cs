/*******************
 LoggingFFT.cs
 FFT結果を格納するcsvファイルを作るclass
 使い方：
 init関数でログファイルファイルを用意．writeすれば自動的に実行される．
 write関数でそれにデータ書き込み．引数のmessageはカンマ区切りの文字列．
 end関数でファイル書き込み終了処理．

 *******************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;//for stopwatch

namespace Ayas_realTimeChart_ver1
{
    class LoggingFFT
    {
        System.IO.StreamWriter writer;
        //stopwatch
        static Stopwatch stpw = new Stopwatch();
        //double timestamp;
        string logfilename;
        //string currentDir = Environment.CurrentDirectory;
        //string file = currentDir + "\\" + initFileName; currentDir +"\\" + 
        string logfilepath = "log_tactilesensor/";//logファイルまでのパス
        private bool nowlogging = false;
        //string digit = "f6";

        public LoggingFFT()
        {

        }

        public bool init()
        {
            logfilename = logfilepath + "FFT-log" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".csv";
            stpw.Restart();
            writer = new System.IO.StreamWriter(@logfilename, true, System.Text.Encoding.Default);
            string tmp = null;
            tmp += "Index,Frequency[Hz],Norm(CH0),Norm(CH1),Norm(CH2),Norm(CH3)";//先頭行の表記
            writer.WriteLine(tmp);
            return true;
        }

        public void write(string message)//messageはカンマ区切りでデータを記載すること
        {
            if (!nowlogging)//もし前ステップまでログをとっていなかったら、ログファイルの名前を新しくつける
            {
                init();
                //Console.WriteLine("Now Logging to " + logfilename);
                nowlogging = true;
            }

            /***
            timestamp = (double)stpw.ElapsedTicks / Stopwatch.Frequency * 1000;
            string tmp = null;
            tmp += timestamp.ToString(digit) + ",";
            tmp += message;
            ***/
            writer.WriteLine(message);
        }

        public void end()
        {
            if (nowlogging)
            {
                writer.Close();
                nowlogging = false;
            }
        }
    }
}
