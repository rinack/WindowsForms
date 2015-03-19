using ControlsEx.Controls.TextBoxEx;
namespace Windows.Forms.Controls.Metros
{
    partial class MetroTextBoxSearch
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
            this.main = new System.Windows.Forms.Panel();
            this.Btn = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.myTextBox1 = new MyTextBox();
            this.main.SuspendLayout();
            this.Btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // main
            // 
            this.main.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_hover_reversed.png");
            this.main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.main.Controls.Add(this.myTextBox1);
            this.main.Controls.Add(this.Btn);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Font = new System.Drawing.Font("宋体", 9F);
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(252, 31);
            this.main.TabIndex = 0;
            // 
            // Btn
            // 
            this.Btn.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.serbtnnomar.png");
            this.Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn.Controls.Add(this.label1);
            this.Btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn.Location = new System.Drawing.Point(163, 0);
            this.Btn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Btn.Name = "Btn";
            this.Btn.Size = new System.Drawing.Size(89, 31);
            this.Btn.TabIndex = 6;
            this.Btn.Click += new System.EventHandler(this.label1_Click);
            this.Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Btn_MouseClick);
            this.Btn.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.Btn.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(25, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "搜  索";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // myTextBox1
            // 
            this.myTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.myTextBox1.Location = new System.Drawing.Point(9, 7);
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.Size = new System.Drawing.Size(149, 16);
            this.myTextBox1.TabIndex = 7;
            this.myTextBox1.WaterText = "";
            this.myTextBox1.TextChanged += new System.EventHandler(this.myTextBox1_TextChanged);
            // 
            // MetroTextBoxSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.main);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximumSize = new System.Drawing.Size(500, 31);
            this.MinimumSize = new System.Drawing.Size(0, 31);
            this.Name = "MetroTextBoxSearch";
            this.Size = new System.Drawing.Size(252, 31);
            this.main.ResumeLayout(false);
            this.main.PerformLayout();
            this.Btn.ResumeLayout(false);
            this.Btn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Btn;
        private MyTextBox myTextBox1;
    }
}
