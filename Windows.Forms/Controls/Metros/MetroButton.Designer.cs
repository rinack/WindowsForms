namespace Windows.Forms.Controls.Metros
{
    partial class MetroButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LabText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabText
            // 
            this.LabText.AutoSize = true;
            this.LabText.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.LabText.ForeColor = System.Drawing.Color.White;
            this.LabText.Location = new System.Drawing.Point(66, 73);
            this.LabText.Name = "LabText";
            this.LabText.Size = new System.Drawing.Size(53, 20);
            this.LabText.TabIndex = 0;
            this.LabText.Text = "登    录";
            this.LabText.Visible = false;
            this.LabText.Click += new System.EventHandler(this.LabText_Click);
            this.LabText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabText_MouseDown);
            this.LabText.MouseEnter += new System.EventHandler(this.LabText_MouseEnter);
            // 
            // MetroButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = AssemblyHelper.GetImage("StanForm.Button.button_login_normal.png");
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.LabText);
            this.DoubleBuffered = true;
            this.Name = "MetroButton";
            this.Size = new System.Drawing.Size(194, 30);
            this.Load += new System.EventHandler(this.MetroButton_Load);
            this.Click += new System.EventHandler(this.MetroButton_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MetroButton_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MetroButton_MouseDown_1);
            this.MouseEnter += new System.EventHandler(this.MetroButton_MouseEnter_1);
            this.MouseLeave += new System.EventHandler(this.MetroButton_MouseLeave_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabText;
    }
}
