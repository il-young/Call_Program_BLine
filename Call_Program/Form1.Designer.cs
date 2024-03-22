namespace Call_Program
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_Call = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bgw_client = new System.ComponentModel.BackgroundWorker();
            this.bgw_Display = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.pb_red = new System.Windows.Forms.PictureBox();
            this.pb_green = new System.Windows.Forms.PictureBox();
            this.lb_st = new System.Windows.Forms.Label();
            this.pb_callOff = new System.Windows.Forms.PictureBox();
            this.pb_callOn = new System.Windows.Forms.PictureBox();
            this.pb_MGZOff = new System.Windows.Forms.PictureBox();
            this.pb_MGZOn = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_callOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_callOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_MGZOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_MGZOn)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Call
            // 
            this.btn_Call.Font = new System.Drawing.Font("굴림", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Call.Location = new System.Drawing.Point(12, 12);
            this.btn_Call.Name = "btn_Call";
            this.btn_Call.Size = new System.Drawing.Size(362, 116);
            this.btn_Call.TabIndex = 0;
            this.btn_Call.Text = "AMB 호출";
            this.btn_Call.UseVisualStyleBackColor = true;
            this.btn_Call.Click += new System.EventHandler(this.btn_Call_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(299, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "숨기기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bgw_client
            // 
            this.bgw_client.WorkerSupportsCancellation = true;
            this.bgw_client.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_client_DoWork);
            // 
            // bgw_Display
            // 
            this.bgw_Display.WorkerSupportsCancellation = true;
            this.bgw_Display.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Display_DoWork);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(12, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "서버 재시작";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pb_red
            // 
            this.pb_red.Image = global::Call_Program.Properties.Resources.circle_red;
            this.pb_red.Location = new System.Drawing.Point(9, 222);
            this.pb_red.Name = "pb_red";
            this.pb_red.Size = new System.Drawing.Size(35, 35);
            this.pb_red.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_red.TabIndex = 5;
            this.pb_red.TabStop = false;
            // 
            // pb_green
            // 
            this.pb_green.Image = global::Call_Program.Properties.Resources.circle_green;
            this.pb_green.Location = new System.Drawing.Point(9, 222);
            this.pb_green.Name = "pb_green";
            this.pb_green.Size = new System.Drawing.Size(35, 35);
            this.pb_green.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_green.TabIndex = 4;
            this.pb_green.TabStop = false;
            // 
            // lb_st
            // 
            this.lb_st.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_st.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_st.Location = new System.Drawing.Point(12, 175);
            this.lb_st.Name = "lb_st";
            this.lb_st.Size = new System.Drawing.Size(84, 46);
            this.lb_st.TabIndex = 9;
            this.lb_st.Text = "상태:";
            this.lb_st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_callOff
            // 
            this.pb_callOff.Image = global::Call_Program.Properties.Resources.circle_red;
            this.pb_callOff.Location = new System.Drawing.Point(153, 179);
            this.pb_callOff.Name = "pb_callOff";
            this.pb_callOff.Size = new System.Drawing.Size(35, 35);
            this.pb_callOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_callOff.TabIndex = 11;
            this.pb_callOff.TabStop = false;
            this.pb_callOff.Click += new System.EventHandler(this.pb_callOff_Click);
            // 
            // pb_callOn
            // 
            this.pb_callOn.Image = global::Call_Program.Properties.Resources.circle_green;
            this.pb_callOn.Location = new System.Drawing.Point(153, 179);
            this.pb_callOn.Name = "pb_callOn";
            this.pb_callOn.Size = new System.Drawing.Size(35, 35);
            this.pb_callOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_callOn.TabIndex = 10;
            this.pb_callOn.TabStop = false;
            // 
            // pb_MGZOff
            // 
            this.pb_MGZOff.Image = global::Call_Program.Properties.Resources.circle_red;
            this.pb_MGZOff.Location = new System.Drawing.Point(299, 179);
            this.pb_MGZOff.Name = "pb_MGZOff";
            this.pb_MGZOff.Size = new System.Drawing.Size(35, 35);
            this.pb_MGZOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_MGZOff.TabIndex = 13;
            this.pb_MGZOff.TabStop = false;
            // 
            // pb_MGZOn
            // 
            this.pb_MGZOn.Image = global::Call_Program.Properties.Resources.circle_green;
            this.pb_MGZOn.Location = new System.Drawing.Point(299, 179);
            this.pb_MGZOn.Name = "pb_MGZOn";
            this.pb_MGZOn.Size = new System.Drawing.Size(35, 35);
            this.pb_MGZOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_MGZOn.TabIndex = 12;
            this.pb_MGZOn.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(102, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Call";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(240, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "MGZ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 301);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pb_MGZOff);
            this.Controls.Add(this.pb_MGZOn);
            this.Controls.Add(this.pb_callOff);
            this.Controls.Add(this.pb_callOn);
            this.Controls.Add(this.lb_st);
            this.Controls.Add(this.pb_red);
            this.Controls.Add(this.pb_green);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Call);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Call Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_callOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_callOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_MGZOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_MGZOn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Call;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker bgw_client;
        private System.ComponentModel.BackgroundWorker bgw_Display;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pb_green;
        private System.Windows.Forms.PictureBox pb_red;
        private System.Windows.Forms.Label lb_st;
        private System.Windows.Forms.PictureBox pb_callOff;
        private System.Windows.Forms.PictureBox pb_callOn;
        private System.Windows.Forms.PictureBox pb_MGZOff;
        private System.Windows.Forms.PictureBox pb_MGZOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

