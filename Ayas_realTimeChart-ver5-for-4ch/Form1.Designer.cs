namespace Ayas_realTimeChart_ver1
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chart_realtime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_Connect = new System.Windows.Forms.Button();
            this.button_Disconnect = new System.Windows.Forms.Button();
            this.label_Free = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.button_logon = new System.Windows.Forms.Button();
            this.button_logoff = new System.Windows.Forms.Button();
            this.groupBox_log = new System.Windows.Forms.GroupBox();
            this.checkBox_zeroset = new System.Windows.Forms.CheckBox();
            this.chart_FFTmagnitude = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label_timeInterval = new System.Windows.Forms.Label();
            this.label_maxIndex = new System.Windows.Forms.Label();
            this.checkBox_showrowChart = new System.Windows.Forms.CheckBox();
            this.label_testname = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_windowFunc = new System.Windows.Forms.ComboBox();
            this.label_maxFrequency = new System.Windows.Forms.Label();
            this.label_samplingFrequency = new System.Windows.Forms.Label();
            this.chart_windowFunc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox_COM = new System.Windows.Forms.ComboBox();
            this.label_date = new System.Windows.Forms.Label();
            this.groupBox_COM = new System.Windows.Forms.GroupBox();
            this.textBox_testname = new System.Windows.Forms.TextBox();
            this.checkBox_serialport = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_chartClear = new System.Windows.Forms.Button();
            this.checkBox_CH3 = new System.Windows.Forms.CheckBox();
            this.checkBox_CH2 = new System.Windows.Forms.CheckBox();
            this.checkBox_CH0 = new System.Windows.Forms.CheckBox();
            this.checkBox_CH1 = new System.Windows.Forms.CheckBox();
            this.chart_row = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_realtime)).BeginInit();
            this.groupBox_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_FFTmagnitude)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_windowFunc)).BeginInit();
            this.groupBox_COM.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_row)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_realtime
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_realtime.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_realtime.Legends.Add(legend1);
            this.chart_realtime.Location = new System.Drawing.Point(11, 337);
            this.chart_realtime.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart_realtime.Name = "chart_realtime";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "CH0";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "CH1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "CH2";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "CH3";
            this.chart_realtime.Series.Add(series1);
            this.chart_realtime.Series.Add(series2);
            this.chart_realtime.Series.Add(series3);
            this.chart_realtime.Series.Add(series4);
            this.chart_realtime.Size = new System.Drawing.Size(298, 207);
            this.chart_realtime.TabIndex = 0;
            this.chart_realtime.Text = "chart1";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(135, 44);
            this.button_Connect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(77, 25);
            this.button_Connect.TabIndex = 1;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // button_Disconnect
            // 
            this.button_Disconnect.Location = new System.Drawing.Point(216, 44);
            this.button_Disconnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_Disconnect.Name = "button_Disconnect";
            this.button_Disconnect.Size = new System.Drawing.Size(77, 25);
            this.button_Disconnect.TabIndex = 2;
            this.button_Disconnect.Text = "Disconnect";
            this.button_Disconnect.UseVisualStyleBackColor = true;
            this.button_Disconnect.Click += new System.EventHandler(this.button_Disconnect_Click);
            // 
            // label_Free
            // 
            this.label_Free.AutoSize = true;
            this.label_Free.Location = new System.Drawing.Point(11, 68);
            this.label_Free.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Free.Name = "label_Free";
            this.label_Free.Size = new System.Drawing.Size(55, 12);
            this.label_Free.TabIndex = 3;
            this.label_Free.Text = "point num";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 6F);
            this.textBox1.Location = new System.Drawing.Point(6, 195);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 44);
            this.textBox1.TabIndex = 4;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM5";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // button_logon
            // 
            this.button_logon.Location = new System.Drawing.Point(16, 16);
            this.button_logon.Margin = new System.Windows.Forms.Padding(2);
            this.button_logon.Name = "button_logon";
            this.button_logon.Size = new System.Drawing.Size(41, 25);
            this.button_logon.TabIndex = 5;
            this.button_logon.Text = "on";
            this.button_logon.UseVisualStyleBackColor = true;
            this.button_logon.Click += new System.EventHandler(this.button_logon_Click);
            // 
            // button_logoff
            // 
            this.button_logoff.Location = new System.Drawing.Point(60, 16);
            this.button_logoff.Margin = new System.Windows.Forms.Padding(2);
            this.button_logoff.Name = "button_logoff";
            this.button_logoff.Size = new System.Drawing.Size(41, 25);
            this.button_logoff.TabIndex = 6;
            this.button_logoff.Text = "off";
            this.button_logoff.UseVisualStyleBackColor = true;
            this.button_logoff.Click += new System.EventHandler(this.button_logoff_Click);
            // 
            // groupBox_log
            // 
            this.groupBox_log.Controls.Add(this.button_logoff);
            this.groupBox_log.Controls.Add(this.button_logon);
            this.groupBox_log.Location = new System.Drawing.Point(6, 87);
            this.groupBox_log.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_log.Name = "groupBox_log";
            this.groupBox_log.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_log.Size = new System.Drawing.Size(113, 47);
            this.groupBox_log.TabIndex = 7;
            this.groupBox_log.TabStop = false;
            this.groupBox_log.Text = "Data Log (off)";
            // 
            // checkBox_zeroset
            // 
            this.checkBox_zeroset.AutoSize = true;
            this.checkBox_zeroset.Checked = true;
            this.checkBox_zeroset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_zeroset.Location = new System.Drawing.Point(123, 108);
            this.checkBox_zeroset.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_zeroset.Name = "checkBox_zeroset";
            this.checkBox_zeroset.Size = new System.Drawing.Size(64, 16);
            this.checkBox_zeroset.TabIndex = 8;
            this.checkBox_zeroset.Text = "ZeroSet";
            this.checkBox_zeroset.UseVisualStyleBackColor = true;
            // 
            // chart_FFTmagnitude
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_FFTmagnitude.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_FFTmagnitude.Legends.Add(legend2);
            this.chart_FFTmagnitude.Location = new System.Drawing.Point(231, 331);
            this.chart_FFTmagnitude.Name = "chart_FFTmagnitude";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "complex data";
            this.chart_FFTmagnitude.Series.Add(series5);
            this.chart_FFTmagnitude.Size = new System.Drawing.Size(549, 292);
            this.chart_FFTmagnitude.TabIndex = 9;
            this.chart_FFTmagnitude.Text = "chart2";
            // 
            // label_timeInterval
            // 
            this.label_timeInterval.AutoSize = true;
            this.label_timeInterval.Location = new System.Drawing.Point(11, 142);
            this.label_timeInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_timeInterval.Name = "label_timeInterval";
            this.label_timeInterval.Size = new System.Drawing.Size(75, 12);
            this.label_timeInterval.TabIndex = 10;
            this.label_timeInterval.Text = "time interval：";
            // 
            // label_maxIndex
            // 
            this.label_maxIndex.AutoSize = true;
            this.label_maxIndex.Location = new System.Drawing.Point(11, 104);
            this.label_maxIndex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_maxIndex.Name = "label_maxIndex";
            this.label_maxIndex.Size = new System.Drawing.Size(87, 12);
            this.label_maxIndex.TabIndex = 11;
            this.label_maxIndex.Text = "最大インデックス：";
            // 
            // checkBox_showrowChart
            // 
            this.checkBox_showrowChart.AutoSize = true;
            this.checkBox_showrowChart.Location = new System.Drawing.Point(13, 46);
            this.checkBox_showrowChart.Name = "checkBox_showrowChart";
            this.checkBox_showrowChart.Size = new System.Drawing.Size(104, 16);
            this.checkBox_showrowChart.TabIndex = 12;
            this.checkBox_showrowChart.Text = "show row Chart";
            this.checkBox_showrowChart.UseVisualStyleBackColor = true;
            // 
            // label_testname
            // 
            this.label_testname.AutoSize = true;
            this.label_testname.Location = new System.Drawing.Point(6, 24);
            this.label_testname.Name = "label_testname";
            this.label_testname.Size = new System.Drawing.Size(91, 12);
            this.label_testname.TabIndex = 13;
            this.label_testname.Text = "input test name：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_windowFunc);
            this.groupBox1.Controls.Add(this.label_maxFrequency);
            this.groupBox1.Controls.Add(this.label_samplingFrequency);
            this.groupBox1.Controls.Add(this.label_timeInterval);
            this.groupBox1.Controls.Add(this.checkBox_showrowChart);
            this.groupBox1.Controls.Add(this.label_maxIndex);
            this.groupBox1.Controls.Add(this.label_Free);
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 175);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fourier transform";
            // 
            // comboBox_windowFunc
            // 
            this.comboBox_windowFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_windowFunc.FormattingEnabled = true;
            this.comboBox_windowFunc.Location = new System.Drawing.Point(13, 18);
            this.comboBox_windowFunc.Name = "comboBox_windowFunc";
            this.comboBox_windowFunc.Size = new System.Drawing.Size(197, 20);
            this.comboBox_windowFunc.TabIndex = 15;
            // 
            // label_maxFrequency
            // 
            this.label_maxFrequency.AutoSize = true;
            this.label_maxFrequency.Location = new System.Drawing.Point(11, 86);
            this.label_maxFrequency.Name = "label_maxFrequency";
            this.label_maxFrequency.Size = new System.Drawing.Size(86, 12);
            this.label_maxFrequency.TabIndex = 15;
            this.label_maxFrequency.Text = "Max frequency：";
            // 
            // label_samplingFrequency
            // 
            this.label_samplingFrequency.AutoSize = true;
            this.label_samplingFrequency.Location = new System.Drawing.Point(11, 124);
            this.label_samplingFrequency.Name = "label_samplingFrequency";
            this.label_samplingFrequency.Size = new System.Drawing.Size(110, 12);
            this.label_samplingFrequency.TabIndex = 15;
            this.label_samplingFrequency.Text = "sampling frequency：";
            // 
            // chart_windowFunc
            // 
            chartArea3.Name = "ChartArea1";
            this.chart_windowFunc.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_windowFunc.Legends.Add(legend3);
            this.chart_windowFunc.Location = new System.Drawing.Point(231, 20);
            this.chart_windowFunc.Name = "chart_windowFunc";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart_windowFunc.Series.Add(series6);
            this.chart_windowFunc.Size = new System.Drawing.Size(549, 292);
            this.chart_windowFunc.TabIndex = 15;
            this.chart_windowFunc.Text = "chart3";
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(6, 46);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(124, 20);
            this.comboBox_COM.TabIndex = 16;
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(305, 3);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(53, 12);
            this.label_date.TabIndex = 17;
            this.label_date.Text = "date time";
            // 
            // groupBox_COM
            // 
            this.groupBox_COM.Controls.Add(this.textBox_testname);
            this.groupBox_COM.Controls.Add(this.checkBox_serialport);
            this.groupBox_COM.Controls.Add(this.groupBox2);
            this.groupBox_COM.Controls.Add(this.comboBox_COM);
            this.groupBox_COM.Controls.Add(this.textBox1);
            this.groupBox_COM.Controls.Add(this.button_Connect);
            this.groupBox_COM.Controls.Add(this.button_Disconnect);
            this.groupBox_COM.Controls.Add(this.groupBox_log);
            this.groupBox_COM.Controls.Add(this.label_testname);
            this.groupBox_COM.Controls.Add(this.checkBox_zeroset);
            this.groupBox_COM.Location = new System.Drawing.Point(11, 6);
            this.groupBox_COM.Name = "groupBox_COM";
            this.groupBox_COM.Size = new System.Drawing.Size(298, 325);
            this.groupBox_COM.TabIndex = 18;
            this.groupBox_COM.TabStop = false;
            this.groupBox_COM.Text = "waiting for select COM";
            // 
            // textBox_testname
            // 
            this.textBox_testname.Location = new System.Drawing.Point(101, 21);
            this.textBox_testname.Name = "textBox_testname";
            this.textBox_testname.Size = new System.Drawing.Size(111, 19);
            this.textBox_testname.TabIndex = 21;
            // 
            // checkBox_serialport
            // 
            this.checkBox_serialport.AutoSize = true;
            this.checkBox_serialport.Location = new System.Drawing.Point(7, 173);
            this.checkBox_serialport.Name = "checkBox_serialport";
            this.checkBox_serialport.Size = new System.Drawing.Size(106, 16);
            this.checkBox_serialport.TabIndex = 20;
            this.checkBox_serialport.Text = "show serial port";
            this.checkBox_serialport.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_chartClear);
            this.groupBox2.Controls.Add(this.checkBox_CH3);
            this.groupBox2.Controls.Add(this.checkBox_CH2);
            this.groupBox2.Controls.Add(this.checkBox_CH0);
            this.groupBox2.Controls.Add(this.checkBox_CH1);
            this.groupBox2.Location = new System.Drawing.Point(6, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 65);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "show realtime chart";
            // 
            // button_chartClear
            // 
            this.button_chartClear.Location = new System.Drawing.Point(124, 18);
            this.button_chartClear.Name = "button_chartClear";
            this.button_chartClear.Size = new System.Drawing.Size(64, 38);
            this.button_chartClear.TabIndex = 21;
            this.button_chartClear.Text = "Clear Chart";
            this.button_chartClear.UseVisualStyleBackColor = true;
            this.button_chartClear.Click += new System.EventHandler(this.button_chartClear_Click_1);
            // 
            // checkBox_CH3
            // 
            this.checkBox_CH3.AutoSize = true;
            this.checkBox_CH3.Location = new System.Drawing.Point(61, 44);
            this.checkBox_CH3.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.checkBox_CH3.Name = "checkBox_CH3";
            this.checkBox_CH3.Size = new System.Drawing.Size(46, 16);
            this.checkBox_CH3.TabIndex = 22;
            this.checkBox_CH3.Text = "CH3";
            this.checkBox_CH3.UseVisualStyleBackColor = true;
            // 
            // checkBox_CH2
            // 
            this.checkBox_CH2.AutoSize = true;
            this.checkBox_CH2.Location = new System.Drawing.Point(11, 44);
            this.checkBox_CH2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.checkBox_CH2.Name = "checkBox_CH2";
            this.checkBox_CH2.Size = new System.Drawing.Size(46, 16);
            this.checkBox_CH2.TabIndex = 21;
            this.checkBox_CH2.Text = "CH2";
            this.checkBox_CH2.UseVisualStyleBackColor = true;
            // 
            // checkBox_CH0
            // 
            this.checkBox_CH0.AutoSize = true;
            this.checkBox_CH0.Location = new System.Drawing.Point(11, 21);
            this.checkBox_CH0.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.checkBox_CH0.Name = "checkBox_CH0";
            this.checkBox_CH0.Size = new System.Drawing.Size(46, 16);
            this.checkBox_CH0.TabIndex = 20;
            this.checkBox_CH0.Text = "CH0";
            this.checkBox_CH0.UseVisualStyleBackColor = true;
            // 
            // checkBox_CH1
            // 
            this.checkBox_CH1.AutoSize = true;
            this.checkBox_CH1.Location = new System.Drawing.Point(61, 21);
            this.checkBox_CH1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.checkBox_CH1.Name = "checkBox_CH1";
            this.checkBox_CH1.Size = new System.Drawing.Size(46, 16);
            this.checkBox_CH1.TabIndex = 21;
            this.checkBox_CH1.Text = "CH1";
            this.checkBox_CH1.UseVisualStyleBackColor = true;
            // 
            // chart_row
            // 
            chartArea4.Name = "ChartArea1";
            this.chart_row.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_row.Legends.Add(legend4);
            this.chart_row.Location = new System.Drawing.Point(3, 331);
            this.chart_row.Name = "chart_row";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.chart_row.Series.Add(series7);
            this.chart_row.Size = new System.Drawing.Size(222, 196);
            this.chart_row.TabIndex = 19;
            this.chart_row.Text = "chart3";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chart_row);
            this.panel1.Controls.Add(this.chart_FFTmagnitude);
            this.panel1.Controls.Add(this.chart_windowFunc);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label_date);
            this.panel1.Location = new System.Drawing.Point(315, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 627);
            this.panel1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "↓↓Fourier Transform↓↓";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1100, 635);
            this.Controls.Add(this.groupBox_COM);
            this.Controls.Add(this.chart_realtime);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_realtime)).EndInit();
            this.groupBox_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_FFTmagnitude)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_windowFunc)).EndInit();
            this.groupBox_COM.ResumeLayout(false);
            this.groupBox_COM.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_row)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_realtime;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Button button_Disconnect;
        private System.Windows.Forms.Label label_Free;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button_logon;
        private System.Windows.Forms.Button button_logoff;
        private System.Windows.Forms.GroupBox groupBox_log;
        private System.Windows.Forms.CheckBox checkBox_zeroset;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_FFTmagnitude;
        private System.Windows.Forms.Label label_timeInterval;
        private System.Windows.Forms.Label label_maxIndex;
        private System.Windows.Forms.CheckBox checkBox_showrowChart;
        private System.Windows.Forms.Label label_testname;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_samplingFrequency;
        private System.Windows.Forms.Label label_maxFrequency;
        private System.Windows.Forms.ComboBox comboBox_windowFunc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_windowFunc;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.GroupBox groupBox_COM;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_row;
        private System.Windows.Forms.CheckBox checkBox_CH3;
        private System.Windows.Forms.CheckBox checkBox_CH2;
        private System.Windows.Forms.CheckBox checkBox_CH1;
        private System.Windows.Forms.CheckBox checkBox_CH0;
        private System.Windows.Forms.CheckBox checkBox_serialport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_chartClear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_testname;
    }
}

