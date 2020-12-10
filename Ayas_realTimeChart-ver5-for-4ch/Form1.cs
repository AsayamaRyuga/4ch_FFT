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

namespace coil_4ch_FFT_ver1
{
    public partial class Form1 : Form
    {

        string message1;//センサから送られてくるメッセージ格納用
        //string message2;
        //string message3;
        static Stopwatch sw = new Stopwatch();
        private double[] originalData = new double[5];// 時間＋センサから送られてくる値
        private double[] ZeroData = new double[5];// センサゼロ値
        private double[] data = new double[5];// 初期値からの差分

        // 計算用
        private int order = 5; //生データの小数を何桁目まで残すか．

        // フーリエ変換用
        int N = 1024;//2のべき乗であることを確認する。フーリエ変換の要素数 complexDataの要素数も同様に変更すること↓↓↓
        private double[,] DataBox1 = new double[1024, 5];
        private double[] timeAve = new double[1024];
        int cutindex = 4;// この値より下のインデックスは最大ノルム観測に利用しない
        int dataPointNum = 0;// データの個数カウント用
        int[] MaxIndex;// 最大ノルムのインデックスを格納する場所

        // ログ作成用
        string filepath = "FFT_result/";
        string makefilepath;
        static Logging logging = new Logging();
        static LoggingFFT loggingFFT = new LoggingFFT();
        static LoggingFFTresult loggingFFTresult = new LoggingFFTresult();
        private bool flag_log = false;
        int FFTcount = 0;

        // グラフ作成用
        string legendCH0 = "CH0";
        string legendCH1 = "CH1";
        string legendCH2 = "CH2";
        string legendCH3 = "CH3";
        string bubbles = "CHbubbles";
        string bubble_area1 = "Area1";
        int displayTime = 10;// グラフに何秒間分のデータを表示するか(秒)
        private bool flag_zeroset = true;

        public Form1()
        {
            InitializeComponent();
            comboBox_windowFunc.Items.AddRange(new string[] { "Rectangular", "Hamming", "Hanning", "Blackman"});// 窓関数をコンボボックスに格納
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // コンボボックスに使用可能なCOMポートを格納
                string[] PortList = SerialPort.GetPortNames();
                comboBox_COM.Items.Clear();
                foreach (string PortName in PortList)
                {
                    comboBox_COM.Items.Add(PortName);
                }
                if(comboBox_COM.Items.Count > 0)
                {
                    comboBox_COM.SelectedIndex = 0;// コンボボックスの初期値設定
                }

                // グラフの描画設定
                chart_realtime.ChartAreas[0].AxisX.Title = "time[s]";
                chart_realtime.ChartAreas[0].AxisY.Title = "Inductance[μH]";
                chart_realtime.ChartAreas[0].AxisY.Maximum = 0.05;// Y軸の最大値指定
                chart_realtime.ChartAreas[0].AxisY.Minimum = -0.0125;// Y軸の最小値指定
                chart_realtime.ChartAreas[0].AxisY.Interval = 0.0125;// y軸のデータ表示間隔

                serialPort1.Close();

                this.Text = "Ayas RealTimeChart ver.5 for 4ch";// UIのタイトル設定
                
                comboBox_windowFunc.SelectedIndex = 2;// 窓関数の初期設定

                button_logoff.BackColor = Color.FromArgb(232, 172, 81);

                sw.Stop(); 
            }
            catch{ }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                message1 = Convert.ToString(Math.Round((double)sw.ElapsedTicks / Stopwatch.Frequency, 6)) + "," + serialPort1.ReadLine();// 時間＋シリアルポートから読み込んだ値
                //message2 = Convert.ToString(Math.Round((double)sw.ElapsedTicks / Stopwatch.Frequency, 6)) + "," + serialPort1.ReadLine();
                //message3 = Convert.ToString(Math.Round((double)sw.ElapsedTicks / Stopwatch.Frequency, 6)) + "," + serialPort1.ReadLine();
                if (checkBox_serialport.Checked)
                {
                    this.Invoke(new EventHandler(DisplayText));
                }
                this.Invoke(new EventHandler(store_data));
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
                    chart_realtime.Series[CH].Points.Clear();
                }

                chart_realtime.Titles.Clear();
                chart_FFTmagnitude.Titles.Clear();
                chart_windowFunc.Titles.Clear();
                chart_bubble.Titles.Clear();

                chart_realtime.Titles.Add("Row data");// グラフのタイトル
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
        private void button_chartClear_Click_1(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    string CH = "CH" + Convert.ToString(i);
                    chart_realtime.Series[CH].Points.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayText(object sender, EventArgs e)
        {
            textBox1.AppendText(message1 + Environment.NewLine);
        }

        private void store_data(object sender, EventArgs e)
        {
            try
            {
                string[] strArrayData = message1.Split(',');// カンマで分割

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

                    // 読み込んだデータの格納(1行のみ、時間以外）
                    for (int i = 1; i < strArrayData.Length; i++)
                    {
                        originalData[i] = Math.Round(Convert.ToDouble(strArrayData[i]), order);// 小数第５位までに四捨五入
                        data[i] = originalData[i] - ZeroData[i];// ゼロ点からの差分をデータとする
                        //data[i] = -(originalData[i] - ZeroData[i]);// アルミ使用時

                        // フィルタリング処理
                        /***
                        if (data[i] < 0.00005)
                        {
                            data[i] = 0;
                        }***/
                    }
                    double time = Math.Round(Convert.ToDouble(strArrayData[0]), order);// 時間

                    DataBox1[dataPointNum, 0] = time;// [s]表示
                    for (int i = 1; i < 5; i++)
                    {
                        DataBox1[dataPointNum, i] = data[i];
                    }
                    

                    if(dataPointNum == 0)
                    {
                        timeAve[dataPointNum] = 0;
                    }
                    else
                    {
                        timeAve[dataPointNum] = DataBox1[dataPointNum, 0] - DataBox1[dataPointNum - 1, 0];
                    }
                    
                    // グラフの表示・非表示設定
                    if (checkBox_CH0.Checked)
                    {
                        chart_realtime.Series[legendCH0].Points.AddXY(time, data[1]);
                    }
                    if (checkBox_CH1.Checked)
                    {
                        chart_realtime.Series[legendCH1].Points.AddXY(time, data[2]);
                    }
                    if (checkBox_CH2.Checked)
                    {
                        chart_realtime.Series[legendCH2].Points.AddXY(time, data[3]);
                    }
                    if (checkBox_CH3.Checked)
                    {
                        chart_realtime.Series[legendCH3].Points.AddXY(time, data[4]);
                    }
                    
                    if (checkBox_bubble.Checked)
                    {
                        bubblemap(data[1], data[2], data[3], data[4]);
                    }

                    // データの個数カウント
                    dataPointNum++;
                    label_Free.Text = "point num:" + dataPointNum;

                    // グラフの横軸の表示範囲設定
                    chart_realtime.ChartAreas[0].AxisX.Maximum = time;
                    chart_realtime.ChartAreas[0].AxisX.Minimum = time - displayTime;// 何秒前のデータまで表示するか

                    // logの作成
                    if (flag_log)
                    {
                        string logmsg = strArrayData[0] + "," + strArrayData[1] + "," + strArrayData[2] + "," + strArrayData[3] + "," + strArrayData[4];// CSVファイルに書き込み
                        logging.write(makefilepath + "/", logmsg);
                    }
                }

                if (dataPointNum == N)// データ数がNならFFT実行
                {
                    FFTcount += 1;
                    this.Invoke(new EventHandler(FFT));// FFT処理スレッドへ
                    bubblemap(data[1], data[2], data[3], data[4]);// バブルチャートの作成
                    dataPointNum = 0;// データの個数のカウントのリセット
                }
                
                for (int i = 0; i < 4; i++)
                {
                    string CH = "CH" + Convert.ToString(i);
                    chart_realtime.Series[CH].IsValueShownAsLabel = false;// データラベル表示設定
                    chart_realtime.Series[CH].ChartType = SeriesChartType.Line;// 折れ線グラフを指定
                    chart_realtime.Series[CH].BorderWidth = 2;// 折れ線グラフの幅を指定
                }
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
            this.button_logon.BackColor = Color.FromArgb(0, 135, 60);
            button_logoff.BackColor = Color.FromArgb(255, 255, 255);
            if (message1 != null)
            {
                flag_log = true;
                groupBox_log.Text = "Data Log (on)";
                makefilepath = filepath + "Logs-" + DateTime.Now.ToString("yyyy年MM月dd日-HH時mm分ss秒" + textBox_testname.Text);// ディレクトリの作成
                Directory.CreateDirectory(makefilepath);
            }
        }

        private void button_logoff_Click(object sender, EventArgs e)
        {
            this.button_logoff.BackColor = Color.FromArgb(232, 172, 81);
            button_logon.BackColor = Color.FromArgb(255, 255, 255);
            flag_log = false;
            groupBox_log.Text = "Data Log (off)";
            logging.end();
            loggingFFT.end();
            loggingFFTresult.end();
        }

        private void FFT(object sender, EventArgs e)
        {
            MaxIndex = new int[4];// センサから送られてくるデータの数
            List<double> maxMagnitude = new List<double>();
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
            double[] filteroutput = new double[N];
            

            // 各リストへの格納
            for (int i = 0; i < N; i++)
            {
                CH0_data[i] = DataBox1[i, 1];
                CH1_data[i] = DataBox1[i, 2];
                CH2_data[i] = DataBox1[i, 3];
                CH3_data[i] = DataBox1[i, 4];
            }

            // 平均データの格納
            double samplingRate = timeAve.Average();// サンプリング周期
            double CH0_Ave = CH0_data.Average();
            double CH1_Ave = CH1_data.Average();
            double CH2_Ave = CH2_data.Average();
            double CH3_Ave = CH3_data.Average();
            double test = CH1_data.Sum();

            chart_row.Series.Clear();
            chart_windowFunc.Series.Clear();
            chart_FFTmagnitude.Series.Clear();

            // 各データをグラフ描画する前処理
            for (int i = 0; i < 4; i++)
            {
                string CH = "CH" + Convert.ToString(i);

                chart_row.Series.Add(CH);
                chart_row.Series[CH].IsValueShownAsLabel = false;
                chart_row.Series[CH].ChartType = SeriesChartType.Line;
                chart_row.Series[CH].BorderWidth = 2;
                chart_row.Series[CH].IsVisibleInLegend = false;// 凡例表示設定

                chart_FFTmagnitude.Series.Add(CH);
                chart_FFTmagnitude.Series[CH].IsValueShownAsLabel = false;
                //chart_FFTmagnitude.Series[CH].ChartType = SeriesChartType.Column;// 棒グラフを指定
                chart_FFTmagnitude.Series[CH].ChartType = SeriesChartType.Line;

                chart_windowFunc.Series.Add(CH);
                chart_windowFunc.Series[CH].IsValueShownAsLabel = false;// データラベル表示設定
                chart_windowFunc.Series[CH].ChartType = SeriesChartType.Line;
                chart_windowFunc.Series[CH].BorderWidth = 2;// 折れ線グラフの幅を指定
                chart_row.Series[CH].IsVisibleInLegend = false;// 凡例表示設定
            }
            // 生データグラフの描画設定
            chart_row.ChartAreas[0].AxisX.Title = "Index";
            chart_row.ChartAreas[0].AxisY.Title = "Inductance[μH]";
            chart_row.ChartAreas[0].AxisX.Minimum = 0;
            chart_row.ChartAreas[0].AxisX.Interval = N / 8;

            // 窓関数グラフの描画設定
            chart_windowFunc.ChartAreas[0].AxisX.Title = "Index";
            chart_windowFunc.ChartAreas[0].AxisY.Title = "Inductance[μH]";
            chart_windowFunc.ChartAreas[0].AxisX.Minimum = 0;
            chart_windowFunc.ChartAreas[0].AxisX.Interval = N / 8;

            // FFTグラフの描画設定
            chart_FFTmagnitude.ChartAreas[0].AxisX.Title = "Index";
            chart_FFTmagnitude.ChartAreas[0].AxisY.Title = "Norm";
            chart_FFTmagnitude.ChartAreas[0].AxisX.Maximum = N / 4;
            chart_FFTmagnitude.ChartAreas[0].AxisX.Minimum = 0;
            chart_FFTmagnitude.ChartAreas[0].AxisX.Interval = N / 16;

            // ゼロ調整＆複素数データ変換
            for (int i = 0; i < N; i++)
            {
                // 平均値処理→この処理がないと周波数ゼロのところに大きなピークが残る
                CH0_data[i] = CH0_data[i] - CH0_Ave;
                CH1_data[i] = CH1_data[i] - CH1_Ave;
                CH2_data[i] = CH2_data[i] - CH2_Ave;
                CH3_data[i] = CH3_data[i] - CH3_Ave;


                //窓関数処理
                //一般的に、周期性を持った信号やランダム信号の分析で、
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
                
                if (checkBox_showrowChart.Checked)
                {
                    chart_row.Series[legendCH0].Points.AddXY(i, CH0_data[i]);
                    chart_row.Series[legendCH1].Points.AddXY(i, CH1_data[i]);
                    chart_row.Series[legendCH2].Points.AddXY(i, CH2_data[i]);
                    chart_row.Series[legendCH3].Points.AddXY(i, CH3_data[i]);
                }
                
                chart_windowFunc.Series[legendCH0].Points.AddXY(i, CH0_data[i] * winValue);
                chart_windowFunc.Series[legendCH1].Points.AddXY(i, CH1_data[i] * winValue);
                chart_windowFunc.Series[legendCH2].Points.AddXY(i, CH2_data[i] * winValue);
                chart_windowFunc.Series[legendCH3].Points.AddXY(i, CH3_data[i] * winValue);
            }

            chart_row.Titles.Clear();
            chart_row.Titles.Add("row data");

            chart_windowFunc.Titles.Clear();
            chart_windowFunc.Titles.Add(comboBox_windowFunc.SelectedItem.ToString());

            /*****フーリエ変換実行（FFT）*****/
            Fourier.Forward(complexData_CH0, FourierOptions.Default);
            Fourier.Forward(complexData_CH1, FourierOptions.Default);
            Fourier.Forward(complexData_CH2, FourierOptions.Default);
            Fourier.Forward(complexData_CH3, FourierOptions.Default);


            // FFTグラフ描画＆logの記録
            for (int i = cutindex; i <= N / 2; i++)// 標準化定理よりFFTの結果で有効なのはNの半分まで
            {
                if (flag_log)
                {
                    string logFFTmsg = FFTcount + "," + i + "," + (i/(samplingRate * N))
                        + "," + Convert.ToString(complexData_CH0[i].Magnitude)
                        + "," + Convert.ToString(complexData_CH1[i].Magnitude)
                        + "," + Convert.ToString(complexData_CH2[i].Magnitude)
                        + "," + Convert.ToString(complexData_CH3[i].Magnitude);
                    //string logFFTmsg = Convert.ToString(array_data[i, 1]) + "," + Convert.ToString(array_data[i, 2]);
                    loggingFFT.write(makefilepath + "/", logFFTmsg);
                }

                //label_Free3.Text = frequency + "[Hz] , " + Convert.ToString(complexData[i].Magnitude);
                complexDataAfter_CH0[i] = complexData_CH0[i].Magnitude;
                complexDataAfter_CH1[i] = complexData_CH1[i].Magnitude;
                complexDataAfter_CH2[i] = complexData_CH2[i].Magnitude;
                complexDataAfter_CH3[i] = complexData_CH3[i].Magnitude;

                chart_FFTmagnitude.Series[legendCH0].Points.AddXY(i, complexData_CH0[i].Magnitude);
                chart_FFTmagnitude.Series[legendCH1].Points.AddXY(i, complexData_CH1[i].Magnitude);
                chart_FFTmagnitude.Series[legendCH2].Points.AddXY(i, complexData_CH2[i].Magnitude);
                chart_FFTmagnitude.Series[legendCH3].Points.AddXY(i, complexData_CH3[i].Magnitude);
            }

            // 最大ノルムの確認
            for (int i = cutindex; i < N/2; i++)
            {
                
                if (complexData_CH0[i].Magnitude == complexDataAfter_CH0.Max())
                {
                    MaxIndex[0] = i;
                    maxMagnitude.Add(complexDataAfter_CH0.Max());
                }
            }
            for (int i = cutindex; i < N / 2; i++)
            {
                if (complexData_CH1[i].Magnitude == complexDataAfter_CH1.Max())
                {
                    MaxIndex[1] = i;
                    maxMagnitude.Add(complexDataAfter_CH1.Max());
                }
            }
            for (int i = cutindex; i < N / 2; i++)
            {
                if (complexData_CH2[i].Magnitude == complexDataAfter_CH2.Max())
                {
                    MaxIndex[2] = i;
                    maxMagnitude.Add(complexDataAfter_CH2.Max());
                }
            }
            for (int i = cutindex; i < N / 2; i++)
            {
                if (complexData_CH3[i].Magnitude == complexDataAfter_CH3.Max())
                {
                    MaxIndex[3] = i;
                    maxMagnitude.Add(complexDataAfter_CH3.Max());
                }
            }

            label_timeInterval.Text = "time interval：" + samplingRate + "[s]";
            label_samplingFrequency.Text = "sampling frequency：" + (1 / (samplingRate * N)) + "[Hz]";
            label_maxIndex.Text = "最大インデックス：" + Convert.ToString(MaxIndex[0]) + " / " + Convert.ToString(MaxIndex[1]) + " / " + Convert.ToString(MaxIndex[2]) + " / " + Convert.ToString(MaxIndex[3]);
            label_maxFrequency.Text = "Max frequency：" + Convert.ToString(MaxIndex[0] / (samplingRate * N)) + "[Hz] / " + Convert.ToString(MaxIndex[1] / (samplingRate * N)) + "[Hz] / " + Convert.ToString(MaxIndex[2] / (samplingRate * N)) + "[Hz] / " + Convert.ToString(MaxIndex[3] / (samplingRate * N)) + "[Hz] / ";

            if (flag_log)
            {
                //"FFTtimes,date,CH0 max Frequency[Hz],CH1 max Frequency[Hz],CH2 max Frequency[Hz],CH3 max Frequency[Hz],CH0 Norm,CH1 Norm,CH2 Norm,CH3 Norm,CH0 MaxIndex,CH1 MaxIndex,CH2 MaxIndex,CH3 MaxIndex,time interval(Ave) ,sampling frequency[Hz]"
                loggingFFTresult.write(makefilepath + "/", FFTcount + "," + Convert.ToString(MaxIndex[0] / (samplingRate * N)) + "," + Convert.ToString(MaxIndex[1] / (samplingRate * N)) + "," + Convert.ToString(MaxIndex[2] / (samplingRate * N)) + "," + Convert.ToString(MaxIndex[3] / (samplingRate * N))
                    + "," + complexData_CH0[MaxIndex[0]].Magnitude + "," + complexData_CH1[MaxIndex[1]].Magnitude + "," + complexData_CH2[MaxIndex[2]].Magnitude + "," + complexData_CH3[MaxIndex[3]].Magnitude
                    + "," + Convert.ToString(MaxIndex[0]) + "," + Convert.ToString(MaxIndex[1]) + "," + Convert.ToString(MaxIndex[2]) + "," + Convert.ToString(MaxIndex[3])
                    + "," + samplingRate + "," + (1 / (samplingRate * N)));

                string date = DateTime.Now.ToString("yyyy年MM月dd日-HH時mm分ss秒");
                label_date.Text = date + "--->" + FFTcount + "回目";

                //コントロールの外観を描画するBitmapの作成
                Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
                //キャプチャする
                panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
                //ファイルに保存する
                bmp.Save(makefilepath + "/" + FFTcount + "times-" + date + ".Jpeg");
                //後始末
                bmp.Dispose();

                /***
                //コントロールの外観を描画するBitmapの作成
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                //キャプチャする
                this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
                //ファイルに保存する
                bmp.Save(makefilepath + "/All-chart-" + date);
                //後始末
                bmp.Dispose();

                /***
                FFTimagefilename = makefilepath + "/FFTresult-chart-" + date;
                Windowimagefilename = makefilepath + "/WINandRow-chart-" + date;
                Rowimagefilename = makefilepath + "/rowdata-chart-" + date;

                chart_FFTmagnitude.SaveImage(FFTimagefilename, ChartImageFormat.Jpeg);// FFT後のグラフ保存
                chart_windowFunc.SaveImage(Windowimagefilename, ChartImageFormat.Jpeg);// 窓関数を掛けた後のグラフの保存
                chart_row.SaveImage(Rowimagefilename, ChartImageFormat.Jpeg);// 生データ画像の保存
                ***/
            }
        }

        public void bubblemap(double CH0, double CH1, double CH2, double CH3)
        {
            // バブルチャート用の設定とデータの格納
            chart_bubble.Series.Clear();
            chart_bubble.ChartAreas.Clear();

            chart_bubble.ChartAreas.Add(new ChartArea(bubble_area1));
            chart_bubble.ChartAreas[bubble_area1].AxisX.Interval = 1.0;
            chart_bubble.ChartAreas[bubble_area1].AxisY.Interval = 1.0;
            //chart_bubble.ChartAreas[bubble_area1].AxisY2.Maximum = 0.05;// バブルの大きさの最小値設定

            /***
            chart_bubble.Series.Add(bubbles);
            chart_bubble.Series[bubbles].ChartType = SeriesChartType.Bubble;
            chart_bubble.Series[bubbles].IsVisibleInLegend = false;// 凡例表示設定
            chart_bubble.Series[bubbles].MarkerStyle = MarkerStyle.Circle;

            // データのセット
            double[] x_values = new double[4] { 1.0, 1.0, 2.0, 2.0 };
            double[] y_values = new double[4] { 1.0, 2.0, 1.0, 2.0 };
            double[] bubble_values = new double[4] { CH2, CH1, CH3, CH0 };

            // データをシリーズにセット
            for (int i = 0; i < y_values.Length; i++)
            {
                //DataPoint dp = new DataPoint((double)x_values[i], y_values[i]);
                double[] y_vals = new double[2] { y_values[i], bubble_values[i] };
                DataPoint dp = new DataPoint((double)x_values[i], y_vals);
                chart_bubble.Series[bubbles].Points.Add(dp);
            }***/


            for (int i = 0; i < 4; i++)
            {
                string CH = "CH" + Convert.ToString(i);
                chart_bubble.Series.Add(CH);
                chart_bubble.Series[CH].IsValueShownAsLabel = false;
                chart_bubble.Series[CH].ChartType = SeriesChartType.Bubble;
                chart_bubble.Series[CH].IsVisibleInLegend = false;// 凡例表示設定
                chart_bubble.Series[CH].MarkerStyle = MarkerStyle.Circle;
            }

            double x_values_ch0 = 2.0;
            double[] y_values_ch0 = new double[2] { 2.0, CH0 };
            DataPoint dp0 = new DataPoint(x_values_ch0, y_values_ch0);
            chart_bubble.Series[legendCH0].Points.Add(dp0);

            double x_values_ch1 = 1.0;
            double[] y_values_ch1 = new double[2] { 2.0, CH1 };
            DataPoint dp1 = new DataPoint(x_values_ch1, y_values_ch1);
            chart_bubble.Series[legendCH1].Points.Add(dp1);

            double x_values_ch2 = 1.0;
            double[] y_values_ch2 = new double[2] { 1.0, CH2 };
            DataPoint dp2 = new DataPoint(x_values_ch2, y_values_ch2);
            chart_bubble.Series[legendCH2].Points.Add(dp2);

            double x_values_ch3 = 2.0;
            double[] y_values_ch3 = new double[2] { 1.0, CH3 };
            DataPoint dp3 = new DataPoint(x_values_ch3, y_values_ch3);
            chart_bubble.Series[legendCH3].Points.Add(dp3);
        }
    }
}
