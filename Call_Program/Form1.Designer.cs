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
            this.lMessage = new System.Windows.Forms.Label();
            this.bgw_client = new System.ComponentModel.BackgroundWorker();
            this.bgw_Display = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.pb_red = new System.Windows.Forms.PictureBox();
            this.pb_green = new System.Windows.Forms.PictureBox();
            this.lb_AREA = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_st = new System.Windows.Forms.Label();
            this.l_ST = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_green)).BeginInit();
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
            // lMessage
            // 
            this.lMessage.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lMessage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lMessage.Location = new System.Drawing.Point(50, 217);
            this.lMessage.Name = "lMessage";
            this.lMessage.Size = new System.Drawing.Size(321, 46);
            this.lMessage.TabIndex = 2;
            this.lMessage.Text = "Message";
            this.lMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // lb_AREA
            // 
            this.lb_AREA.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_AREA.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_AREA.Location = new System.Drawing.Point(102, 129);
            this.lb_AREA.Name = "lb_AREA";
            this.lb_AREA.Size = new System.Drawing.Size(272, 46);
            this.lb_AREA.TabIndex = 6;
            this.lb_AREA.Text = "AREA";
            this.lb_AREA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 46);
            this.label1.TabIndex = 7;
            this.label1.Text = "위치:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // l_ST
            // 
            this.l_ST.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_ST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.l_ST.Location = new System.Drawing.Point(102, 175);
            this.l_ST.Name = "l_ST";
            this.l_ST.Size = new System.Drawing.Size(272, 46);
            this.l_ST.TabIndex = 8;
            this.l_ST.Text = "STATUS";
            this.l_ST.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 301);
            this.Controls.Add(this.lb_st);
            this.Controls.Add(this.l_ST);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_AREA);
            this.Controls.Add(this.pb_red);
            this.Controls.Add(this.pb_green);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lMessage);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Call;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lMessage;
        private System.ComponentModel.BackgroundWorker bgw_client;
        private System.ComponentModel.BackgroundWorker bgw_Display;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pb_green;
        private System.Windows.Forms.PictureBox pb_red;
        private System.Windows.Forms.Label lb_AREA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_st;
        private System.Windows.Forms.Label l_ST;
    }
}

