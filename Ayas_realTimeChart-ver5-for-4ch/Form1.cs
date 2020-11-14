/*******************
 * ver1:グラフの描画
 * ver2:CSV出力
 * ver3:ゼロ点調整
 * ver4:リアルタイムフーリエ処理
 * ver4.3:FFT処理の調整、FFT画像の保存、FFTデータの保存、最大周波数の保存などが可能
 * ver5:窓関数の追加
***************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.IO.Ports;

namespace Ayas_realTimeChart_ver1
{
    public partial class Form1 : Form
    {
        string imagefilepath = "FFT_result/";
        string FFTimagefilename;
        string Windowimagefilename;

        string message;//センサから送られてくるメッセージ格納用
        static Stopwatch sw = new Stopwatch();
        private double[] originalData = new double[5];// 時間＋センサから送られてくる値
        private double[] ZeroData = new double[5];// センサゼロ値
        private double[] data = new double[5];// 初期値からの差分

        

        // 計算用
        private int order = 5; //生データの小数を何桁目まで残すか．

        // フーリエ変換用
        int N = 256;//2のべき乗であることを確認する。フーリエ変換の要素数 complexDataの要素数も同様に変更すること↓↓↓
        int dataPointNum = 0;// データの個数カウント用
        
        //private double[] complexDataBefore = new double[256];
        //private double[] timeBefore = new double[256];
        private double[,] DataBox = new double[256, 5];
        private double[] timeAve = new double[256];
        int[] MaxIndex;// 最大ノルムのインデックスを格納する場所

        // 窓関数
        public enum WindowFunc
        {
            Hamming,
            Hanning,
            Blackman,
            Rectangular
        }

        // ログ作成用
        static Logging logging = new Logging();
        static LoggingFFT loggingFFT = new LoggingFFT();
        static LoggingFFTresult loggingFFTresult = new LoggingFFTresult();
        private bool flag_log = false;

        // グラフ作成用
        string legendCH0 = "CH0";
        string legendCH1 = "CH1";
        string legendCH2 = "CH2";
        string legendCH3 = "CH3";

        string legend2 = "complex data";
        string legend3 = "row data";
        string legend4 = "after windowFunc";
        int displayTime = 10;// グラフに何秒間分のデータを表示するか(秒)
        private bool flag_zeroset = true;

        public Form1()
        {
            InitializeComponent();
            comboBox_windowFunc.Items.AddRange(new string[] { "Rectangular", "Hamming", "Hanning", "Blackman"});
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {   // COM通信用
                string[] PortList = SerialPort.GetPortNames();
                comboBox_COM.Items.Clear();
                foreach (string PortName in PortList)
                {
                    comboBox_COM.Items.Add(PortName);
                }
                if(comboBox_COM.Items.Count > 0)
                {
                    comboBox_COM.SelectedIndex = 0;
                }
                // グラフの描画設定
                chart1.ChartAreas[0].AxisX.Title = "time[s]";
                chart1.ChartAreas[0].AxisY.Title = "Inductance[μH]";
                chart1.ChartAreas[0].AxisY.Maximum = 0.05;// Y軸の最大値指定
                chart1.ChartAreas[0].AxisY.Minimum = -0.0125;// Y軸の最小値指定
                chart1.ChartAreas[0].AxisY.Interval = 0.0125;// y軸のデータ表示間隔

                serialPort1.Close();
                this.Text = "Ayas RealTimeChart ver.5 for 4ch";// UIのタイトル設定
                comboBox_windowFunc.SelectedIndex = 0;
                sw.Stop(); 
            }
            catch{ }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                message = Convert.ToString(Math.Round((double)sw.ElapsedTicks / Stopwatch.Frequency, 1)) + "," + serialPort1.ReadLine();// 時間＋シリアルポートから読み込んだ値
                this.Invoke(new EventHandler(DisplayText));
                this.Invoke(new EventHandler(showChart));
            }
            catch { }                                
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (comboBox_COM.SelectedIndex != -1)
            {
                serialPort1.PortName = comboBox_COM.SelectedItem.ToString();
                groupBox_COM.Text = "Sensor(" + comboBox_COM.SelectedItem.ToString() + ")-OK";
            }
            
            try
            {
                //serialPort1.PortName =;
                serialPort1.Open();
                textBox1.ResetText();
                sw.Restart();// stopwatchスタート
                
                for (int i = 0; i <4; i++)
                {
                    string CH = "CH" + Convert.ToString(i);
                    chart1.Series[CH].Points.Clear();
                }
                //chart1.Series[legendCH0].Points.Clear();
                
                chart_FFTmagnitude.Series[legend2].Points.Clear();
                chart1.Titles.Add("Row data");// グラフのタイトル
                chart_FFTmagnitude.Titles.Add("FFT");// グラフのタイトル
                chart_windowFunc.Titles.Add("row & window Func");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                groupBox_COM.Text = "Sensor(" + comboBox_COM.SelectedItem.ToString() + ")-Failed";
            }
        }

        private void button_Disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                groupBox_COM.Text = "Sensor(" + comboBox_COM.SelectedItem.ToString() + ")-removed";
                sw.Stop();
                logging.end();
                loggingFFT.end();
                loggingFFTresult.end();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayText(object sender, EventArgs e)
        {
            textBox1.AppendText(message + Environment.NewLine);
            
        }

        private void showChart(object sender, EventArgs e)
        {
            try
            {
                string[] strArrayData = message.Split(',');// カンマで分割

                if (strArrayData.Length == 5)
                {

                    // ゼロ点調整
                    if (checkBox_zeroset.Checked && flag_zeroset == true)
                    {
                        for (int i = 1; i < strArrayData.Length; i++)
                        {
                            ZeroData[i] = Math.Round(Convert.ToDouble(strArrayData[i]), order);
                        }
                        //label_Free.Text = "Zero pos.: " + ZeroData[1] + ", " + ZeroData[2] + ", " + ZeroData[3] + ", " + ZeroData[4];

                        if (ZeroData[4] < 20)// ゼロ点調整を終わらせる条件
                        {
                            flag_zeroset = false;
                        }
                    }

                    // 読み込んだデータの格納(1行のみ）
                    for (int i = 1; i < strArrayData.Length; i++)
                    {
                        originalData[i] = Math.Round(Convert.ToDouble(strArrayData[i]), order);// 小数第５位までに四捨五入
                        data[i] = originalData[i] - ZeroData[i];// ゼロ点からの差分をデータとする
                    }
                    double time = Convert.ToDouble(strArrayData[0]);// 時間

                    //complexDataBefore[dataPointNum] = data[1];//for debug
                    //timeBefore[dataPointNum] = time;

                    DataBox[dataPointNum, 0] = time;
                    for (int i = 1; i < 5; i++)
                    {
                        DataBox[dataPointNum, i] = data[i];
                        //label_rawData.Text = "CH" + i + " raw data(zero setted)：" + Convert.ToString(Math.Round(DataBox[dataPointNum, i], 3));
                    }
                    

                    if(dataPointNum == 0)
                    {
                        timeAve[dataPointNum] = 0;
                    }
                    else
                    {
                        //timeAve[dataPointNum] = timeBefore[dataPointNum] - timeBefore[dataPointNum - 1];
                        timeAve[dataPointNum] = DataBox[dataPointNum, 0] - DataBox[dataPointNum - 1, 0];
                    }

                    //label_rawData.Text = "raw data(zero setted)：" + Convert.ToString(Math.Round(complexDataBefore[dataPointNum], 3));
                    

                    for (int i = 0; i < 4; i++)
                    {
                        string CH = "CH" + Convert.ToString(i);
                        chart1.Series[CH].Points.AddXY(time, data[i+1]);
                    }

                    // データの個数カウント
                    dataPointNum++;
                    label_Free.Text = "point num:" + dataPointNum;

                    // グラフの横軸の表示範囲設定
                    chart1.ChartAreas[0].AxisX.Maximum = time;
                    chart1.ChartAreas[0].AxisX.Minimum = time - displayTime;// 何秒前のデータまで表示するか

                    // logの作成
                    if (flag_log)
                    {
                        string logmsg = strArrayData[1] + "," + strArrayData[2] + "," + strArrayData[3] + "," + strArrayData[4];// CSVファイルに書き込み
                        logging.write(logmsg);
                    }
                }

                if (dataPointNum == N)// データ数がNならFFT実行
                {
                    this.Invoke(new EventHandler(FFT));// FFT処理スレッドへ
                    dataPointNum = 0;// データの個数のカウントのリセット
                }

                
                for (int i = 0; i < 4; i++)
                {
                    string CH = "CH" + Convert.ToString(i);
                    chart1.Series[CH].IsValueShownAsLabel = false;// データラベル表示設定
                    chart1.Series[CH].ChartType = SeriesChartType.Line;// 折れ線グラフを指定
                    chart1.Series[CH].BorderWidth = 2;// 折れ線グラフの幅を指定
                }
                chart1.Series[legendCH0].Color = Color.Orange;
                chart1.Series[legendCH1].Color = Color.BlueViolet;
                chart1.Series[legendCH2].Color = Color.DeepSkyBlue;
                chart1.Series[legendCH3].Color = Color.Green;

            }

            catch { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                serialPort1.Close();
                sw.Stop();
                logging.end();
                loggingFFT.end();
                loggingFFTresult.end();
            }
            catch { }
        }

        private void button_logon_Click(object sender, EventArgs e)
        {
            flag_log = true;
            groupBox_log.Text = "Data Log (on)";

        }

        private void button_logoff_Click(object sender, EventArgs e)
        {
            flag_log = false;
            groupBox_log.Text = "Data Log (off)";
            logging.end();
            loggingFFT.end();
            loggingFFTresult.end();
        }

        private void FFT(object sender, EventArgs e)
        {
            MaxIndex = new int[4];
            Complex[] complexData_CH0 = new Complex[N];
            Complex[] complexData_CH1 = new Complex[N];
            Complex[] complexData_CH2 = new Complex[N];
            Complex[] complexData_CH3 = new Complex[N];
            double[] CH0_data = new double[N];
            double[] CH1_data = new double[N];
            double[] CH2_data = new double[N];
            double[] CH3_data = new double[N];
            double[] complexDataAfter_CH0 = new double[N];
            double[] complexDataAfter_CH1 = new double[N];
            double[] complexDataAfter_CH2 = new double[N];
            double[] complexDataAfter_CH3 = new double[N];

            for (int i = 0; i < N; i++)
            {
                CH0_data[i] = DataBox[i, 1];
                CH1_data[i] = DataBox[i, 2];
                CH2_data[i] = DataBox[i, 3];
                CH3_data[i] = DataBox[i, 4];
            }

            // 平均データの格納
            double samplingRate = timeAve.Average();// サンプリング周期
            double CH0_Ave = CH0_data.Average();
            double CH1_Ave = CH1_data.Average();
            double CH2_Ave = CH2_data.Average();
            double CH3_Ave = CH3_data.Average();

            // FFTグラフの描画設定
            chart_FFTmagnitude.Series.Clear();
            chart_FFTmagnitude.Series.Add(legend2);
            chart_FFTmagnitude.ChartAreas[0].AxisX.Title = "Index";
            chart_FFTmagnitude.ChartAreas[0].AxisY.Title = "Norm";
            chart_FFTmagnitude.Series[legend2].IsVisibleInLegend = false;// 凡例表示設定
            chart_FFTmagnitude.Series[legend2].IsValueShownAsLabel = false;// データラベル表示設定
            chart_FFTmagnitude.Series[legend2].ChartType = SeriesChartType.Column;// 棒グラフを指定
            //chart2.Series[legend2].ChartType = SeriesChartType.Line;// 折れ線グラフを指定
            chart_FFTmagnitude.ChartAreas[0].AxisX.Maximum = N / 2;
            chart_FFTmagnitude.ChartAreas[0].AxisX.Minimum = 0;
            chart_FFTmagnitude.ChartAreas[0].AxisX.Interval = N / 8;

            // FFT前のグラフの描画設定
            chart_windowFunc.Series.Clear();
            chart_windowFunc.Series.Add(legend3);
            chart_windowFunc.Series.Add(legend4);
            chart_windowFunc.ChartAreas[0].AxisX.Title = "Index";
            chart_windowFunc.ChartAreas[0].AxisY.Title = "Inductance[μH]";
            chart_windowFunc.Series[legend3].IsValueShownAsLabel = false;// データラベル表示設定
            chart_windowFunc.Series[legend4].IsValueShownAsLabel = false;// データラベル表示設定
            chart_windowFunc.ChartAreas[0].AxisX.Minimum = 0;
            chart_windowFunc.ChartAreas[0].AxisX.Interval = N / 8;
            chart_windowFunc.Series[legend3].ChartType = SeriesChartType.Line;
            chart_windowFunc.Series[legend4].ChartType = SeriesChartType.Line;
            chart_windowFunc.Series[legend3].BorderWidth = 2;// 折れ線グラフの幅を指定
            chart_windowFunc.Series[legend4].BorderWidth = 2;// 折れ線グラフの幅を指定

            label_timeInterval.Text = "time interval：" + samplingRate + "[ms]";

            // ゼロ調整＆複素数データ変換
            for (int i = 0; i < N; i++)
            {
                //complexDataBefore[i] = complexDataBefore[i] - averageData;// 平均値処理→この処理がないと周波数ゼロのところに大きなピークが残る
                CH0_data[i] = CH0_data[i] - CH0_Ave;
                CH1_data[i] = CH1_data[i] - CH1_Ave;
                CH2_data[i] = CH2_data[i] - CH2_Ave;
                CH3_data[i] = CH3_data[i] - CH3_Ave;


                //窓関数処理一般的に、周期性を持った信号やランダム信号の分析で、
                //周波数を重視するときにはハニングウインドウを、
                //振幅を重視するときにはフラットトップウインドウを使用し
                //打撃試験で伝達関数を測定するときはレクタンギュラウインドウを使う
                double winValue = 0;
                if (comboBox_windowFunc.SelectedItem.ToString() == "Hamming")
                {
                    winValue = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / (N - 1));
                }
                else if (comboBox_windowFunc.SelectedItem.ToString() == "Hanning")
                {
                    winValue = 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / (N - 1));
                }
                else if (comboBox_windowFunc.SelectedItem.ToString() == "Blackman")
                {
                    winValue = 0.42 - 0.5 * Math.Cos(2 * Math.PI * i / (N - 1))
                                    + 0.08 * Math.Cos(4 * Math.PI * i / (N - 1));
                }
                else if (comboBox_windowFunc.SelectedItem.ToString() == "Rectangular")
                {
                    winValue = 1.0;
                }
                else
                {
                    winValue = 1.0;
                }

                complexData_CH0[i] = new Complex(CH0_data[i] * winValue, 0);
                complexData_CH1[i] = new Complex(CH1_data[i] * winValue, 0);
                complexData_CH2[i] = new Complex(CH2_data[i] * winValue, 0);
                complexData_CH3[i] = new Complex(CH3_data[i] * winValue, 0);

                chart_windowFunc.Series[legend3].Points.AddXY(i, CH0_data[i]);
                chart_windowFunc.Series[legend4].Points.AddXY(i, CH0_data[i] * winValue);
            }

            chart_windowFunc.Titles.Clear();
            chart_windowFunc.Titles.Add("row & " + comboBox_windowFunc.SelectedItem.ToString());

            /*****フーリエ変換実行（FFT）*****/
            Fourier.Forward(complexData_CH0, FourierOptions.Default);
            Fourier.Forward(complexData_CH1, FourierOptions.Default);
            Fourier.Forward(complexData_CH2, FourierOptions.Default);
            Fourier.Forward(complexData_CH3, FourierOptions.Default);

            // グラフ描画＆logの記録
            for (int i = 0; i <= N / 2; i++)// 標準化定理よりFFTの結果で有効なのはNの半分まで
            {

                if (flag_log)
                {
                    string logFFTmsg = i + "," + (i/(samplingRate * N))
                        + "," + Convert.ToString(complexData_CH0[i].Magnitude)
                        + "," + Convert.ToString(complexData_CH1[i].Magnitude)
                        + "," + Convert.ToString(complexData_CH2[i].Magnitude)
                        + "," + Convert.ToString(complexData_CH3[i].Magnitude);
                    //string logFFTmsg = Convert.ToString(array_data[i, 1]) + "," + Convert.ToString(array_data[i, 2]);
                    loggingFFT.write(logFFTmsg);
                }

                //label_Free3.Text = frequency + "[Hz] , " + Convert.ToString(complexData[i].Magnitude);
                complexDataAfter_CH0[i] = complexData_CH0[i].Magnitude;
                complexDataAfter_CH1[i] = complexData_CH1[i].Magnitude;
                complexDataAfter_CH2[i] = complexData_CH2[i].Magnitude;
                complexDataAfter_CH3[i] = complexData_CH3[i].Magnitude;

                chart_FFTmagnitude.Series[legend2].Points.AddXY(i, complexData_CH1[i].Magnitude);
                //chart2.Series[]

                
            }

            // 最大ノルムの確認
            for (int i = 0; i < N/2; i++)
            {
                
                if (complexData_CH0[i].Magnitude == complexDataAfter_CH0.Max())
                {
                    MaxIndex[0] = i;
                }
            }
            for (int i = 0; i < N / 2; i++)
            {
                if (complexData_CH1[i].Magnitude == complexDataAfter_CH1.Max())
                {
                    MaxIndex[1] = i;
                }
            }
            for (int i = 0; i < N / 2; i++)
            {
                if (complexData_CH2[i].Magnitude == complexDataAfter_CH2.Max())
                {
                    MaxIndex[2] = i;
                }
            }
            for (int i = 0; i < N / 2; i++)
            {
                if (complexData_CH3[i].Magnitude == complexDataAfter_CH3.Max())
                {
                    MaxIndex[3] = i;
                }
            }


            label_samplingFrequency.Text = "sampling frequency：" + (1 / (samplingRate * N)) + "[Hz]";
            label_maxIndex.Text = "最大インデックス：" + Convert.ToString(MaxIndex[0]) + " / " + Convert.ToString(MaxIndex[1]) + " / " + Convert.ToString(MaxIndex[2]) + " / " + Convert.ToString(MaxIndex[3]);
            label_maxFrequency.Text = "Max frequency：" + Convert.ToString(MaxIndex[0] / (samplingRate * N)) + "[Hz] / " + Convert.ToString(MaxIndex[1] / (samplingRate * N)) + "[Hz] / " + Convert.ToString(MaxIndex[2] / (samplingRate * N)) + "[Hz] / " + Convert.ToString(MaxIndex[3] / (samplingRate * N)) + "[Hz] / ";

            if (flag_log)
            {
                //"date,CH0 max Frequency[Hz],CH1 max Frequency[Hz],CH2 max Frequency[Hz],CH3 max Frequency[Hz],CH0 Norm,CH1 Norm,CH2 Norm,CH3 Norm,CH0 MaxIndex,CH1 MaxIndex,CH2 MaxIndex,CH3 MaxIndex,time interval(Ave) ,sampling frequency[Hz]"
                loggingFFTresult.write(Convert.ToString(MaxIndex[0] / (samplingRate * N)) + "," + Convert.ToString(MaxIndex[1] / (samplingRate * N)) + "," + Convert.ToString(MaxIndex[2] / (samplingRate * N)) + "," + Convert.ToString(MaxIndex[3] / (samplingRate * N))
                    + "," + complexData_CH0[MaxIndex[0]].Magnitude + "," + complexData_CH0[MaxIndex[1]].Magnitude + "," + complexData_CH0[MaxIndex[2]].Magnitude + "," + complexData_CH0[MaxIndex[3]].Magnitude
                    + "," + Convert.ToString(MaxIndex[0]) + "," + Convert.ToString(MaxIndex[1]) + "," + Convert.ToString(MaxIndex[2]) + "," + Convert.ToString(MaxIndex[3])
                    + "," + samplingRate + "," + (1 / (samplingRate * N)));
                
                FFTimagefilename = imagefilepath + "FFTresult-chart-" + DateTime.Now.ToString("yyyyMMdd-HHmm-ss") + ".Jpeg";
                Windowimagefilename = imagefilepath + "WINandRow-chart-" + DateTime.Now.ToString("yyyyMMdd-HHmm-ss") + ".Jpeg";

                chart_FFTmagnitude.SaveImage(FFTimagefilename, ChartImageFormat.Jpeg);// FFT後のグラフ保存
                chart_windowFunc.SaveImage(Windowimagefilename, ChartImageFormat.Jpeg);// 生データと窓関数を掛けた後のグラフの保存

            }

            if (checkBox_FFT_Yaxis_Fixed.Checked)// グラフの最大値設定
            {
                chart_FFTmagnitude.ChartAreas[0].AxisY.Maximum = 0.1;
            }
            else
            {
                chart_FFTmagnitude.ChartAreas[0].AxisY.Maximum = complexDataAfter_CH0.Max();
            }

            
        }
    }
}
