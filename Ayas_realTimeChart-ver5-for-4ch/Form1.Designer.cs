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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            this.checkBox_FFT_Yaxis_Fixed = new System.Windows.Forms.CheckBox();
            this.label_rawData = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_windowFunc = new System.Windows.Forms.ComboBox();
            this.label_maxFrequency = new System.Windows.Forms.Label();
            this.label_samplingFrequency = new System.Windows.Forms.Label();
            this.chart_windowFunc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox_COM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_COM = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_FFTmagnitude)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_windowFunc)).BeginInit();
            this.groupBox_COM.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(271, 9);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart1.Name = "chart1";
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
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(461, 329);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(6, 76);
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
            this.button_Disconnect.Location = new System.Drawing.Point(87, 76);
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
            this.label_Free.Location = new System.Drawing.Point(9, 239);
            this.label_Free.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Free.Name = "label_Free";
            this.label_Free.Size = new System.Drawing.Size(55, 12);
            this.label_Free.TabIndex = 3;
            this.label_Free.Text = "point num";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 447);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 186);
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
            this.groupBox_log.Location = new System.Drawing.Point(6, 120);
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
            this.checkBox_zeroset.Location = new System.Drawing.Point(133, 141);
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
            this.chart_FFTmagnitude.Location = new System.Drawing.Point(751, 9);
            this.chart_FFTmagnitude.Name = "chart_FFTmagnitude";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "complex data";
            this.chart_FFTmagnitude.Series.Add(series5);
            this.chart_FFTmagnitude.Size = new System.Drawing.Size(461, 329);
            this.chart_FFTmagnitude.TabIndex = 9;
            this.chart_FFTmagnitude.Text = "chart2";
            // 
            // label_timeInterval
            // 
            this.label_timeInterval.AutoSize = true;
            this.label_timeInterval.Location = new System.Drawing.Point(11, 87);
            this.label_timeInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_timeInterval.Name = "label_timeInterval";
            this.label_timeInterval.Size = new System.Drawing.Size(75, 12);
            this.label_timeInterval.TabIndex = 10;
            this.label_timeInterval.Text = "time interval：";
            // 
            // label_maxIndex
            // 
            this.label_maxIndex.AutoSize = true;
            this.label_maxIndex.Location = new System.Drawing.Point(11, 47);
            this.label_maxIndex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_maxIndex.Name = "label_maxIndex";
            this.label_maxIndex.Size = new System.Drawing.Size(87, 12);
            this.label_maxIndex.TabIndex = 11;
            this.label_maxIndex.Text = "最大インデックス：";
            // 
            // checkBox_FFT_Yaxis_Fixed
            // 
            this.checkBox_FFT_Yaxis_Fixed.AutoSize = true;
            this.checkBox_FFT_Yaxis_Fixed.Checked = true;
            this.checkBox_FFT_Yaxis_Fixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_FFT_Yaxis_Fixed.Location = new System.Drawing.Point(13, 107);
            this.checkBox_FFT_Yaxis_Fixed.Name = "checkBox_FFT_Yaxis_Fixed";
            this.checkBox_FFT_Yaxis_Fixed.Size = new System.Drawing.Size(136, 16);
            this.checkBox_FFT_Yaxis_Fixed.TabIndex = 12;
            this.checkBox_FFT_Yaxis_Fixed.Text = "FFT-Yaxis_max_Fixed";
            this.checkBox_FFT_Yaxis_Fixed.UseVisualStyleBackColor = true;
            // 
            // label_rawData
            // 
            this.label_rawData.AutoSize = true;
            this.label_rawData.Location = new System.Drawing.Point(6, 181);
            this.label_rawData.Name = "label_rawData";
            this.label_rawData.Size = new System.Drawing.Size(55, 12);
            this.label_rawData.TabIndex = 13;
            this.label_rawData.Text = "raw data：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_windowFunc);
            this.groupBox1.Controls.Add(this.label_maxFrequency);
            this.groupBox1.Controls.Add(this.label_samplingFrequency);
            this.groupBox1.Controls.Add(this.label_timeInterval);
            this.groupBox1.Controls.Add(this.checkBox_FFT_Yaxis_Fixed);
            this.groupBox1.Controls.Add(this.label_maxIndex);
            this.groupBox1.Location = new System.Drawing.Point(11, 254);
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
            this.comboBox_windowFunc.Location = new System.Drawing.Point(13, 133);
            this.comboBox_windowFunc.Name = "comboBox_windowFunc";
            this.comboBox_windowFunc.Size = new System.Drawing.Size(197, 20);
            this.comboBox_windowFunc.TabIndex = 15;
            // 
            // label_maxFrequency
            // 
            this.label_maxFrequency.AutoSize = true;
            this.label_maxFrequency.Location = new System.Drawing.Point(11, 24);
            this.label_maxFrequency.Name = "label_maxFrequency";
            this.label_maxFrequency.Size = new System.Drawing.Size(86, 12);
            this.label_maxFrequency.TabIndex = 15;
            this.label_maxFrequency.Text = "Max frequency：";
            // 
            // label_samplingFrequency
            // 
            this.label_samplingFrequency.AutoSize = true;
            this.label_samplingFrequency.Location = new System.Drawing.Point(11, 68);
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
            this.chart_windowFunc.Location = new System.Drawing.Point(271, 344);
            this.chart_windowFunc.Name = "chart_windowFunc";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart_windowFunc.Series.Add(series6);
            this.chart_windowFunc.Size = new System.Drawing.Size(683, 290);
            this.chart_windowFunc.TabIndex = 15;
            this.chart_windowFunc.Text = "chart3";
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(6, 35);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(204, 20);
            this.comboBox_COM.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "serial port";
            // 
            // groupBox_COM
            // 
            this.groupBox_COM.Controls.Add(this.comboBox_COM);
            this.groupBox_COM.Controls.Add(this.button_Connect);
            this.groupBox_COM.Controls.Add(this.button_Disconnect);
            this.groupBox_COM.Controls.Add(this.groupBox_log);
            this.groupBox_COM.Controls.Add(this.label_rawData);
            this.groupBox_COM.Controls.Add(this.checkBox_zeroset);
            this.groupBox_COM.Location = new System.Drawing.Point(11, 15);
            this.groupBox_COM.Name = "groupBox_COM";
            this.groupBox_COM.Size = new System.Drawing.Size(222, 207);
            this.groupBox_COM.TabIndex = 18;
            this.groupBox_COM.TabStop = false;
            this.groupBox_COM.Text = "waiting for select COM";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 645);
            this.Controls.Add(this.groupBox_COM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart_windowFunc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart_FFTmagnitude);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label_Free);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_FFTmagnitude)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_windowFunc)).EndInit();
            this.groupBox_COM.ResumeLayout(false);
            this.groupBox_COM.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
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
        private System.Windows.Forms.CheckBox checkBox_FFT_Yaxis_Fixed;
        private System.Windows.Forms.Label label_rawData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_samplingFrequency;
        private System.Windows.Forms.Label label_maxFrequency;
        private System.Windows.Forms.ComboBox comboBox_windowFunc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_windowFunc;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_COM;
    }
}

