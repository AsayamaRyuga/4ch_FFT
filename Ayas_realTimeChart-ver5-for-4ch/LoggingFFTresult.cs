using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;//for stopwatch

namespace Ayas_realTimeChart_ver1
{
    class LoggingFFTresult
    {
        System.IO.StreamWriter writer;
        //stopwatch
        static Stopwatch stpw = new Stopwatch();
        double timestamp;
        string logfilename;
        //string currentDir = Environment.CurrentDirectory;
        //string file = currentDir + "\\" + initFileName; currentDir +"\\" + 
        string logfilepath = "FFT_result/";//logファイルまでのパス
        private bool nowlogging = false;
        //string digit = "f6";

        public LoggingFFTresult()
        {

        }

        public bool init()
        {
            logfilename = logfilepath + "FFTresult-log" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".csv";
            stpw.Restart();
            writer = new System.IO.StreamWriter(@logfilename, true, System.Text.Encoding.Default);
            string tmp = null;
            tmp += "date,max Frequency[Hz],Norm,time interval(Ave),sampling frequency[Hz],MaxIndex";//先頭行の表記
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

            
            timestamp = (double)stpw.ElapsedTicks / Stopwatch.Frequency*1000;
            string tmp = null;
            tmp += DateTime.Now.ToString("HH:mm:ss") + ",";
            //tmp += timestamp.ToString(digit) + ",";
            tmp += message;
            
            writer.WriteLine(tmp);
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
