using Windows.Forms.Controls.TextBoxEx;
namespace Windows.Forms.Controls.Metros
{
    partial class MetroTextBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.myTextBox2 = new  MyTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_normal_reversed.png");
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.myTextBox2);
            this.panel1.Font = new System.Drawing.Font("宋体", 9F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 30);
            this.panel1.TabIndex = 0;
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.MetroTextBox_MouseLeave);
            // 
            // myTextBox2
            // 
            this.myTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTextBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.myTextBox2.Location = new System.Drawing.Point(11, 7);
            this.myTextBox2.Name = "myTextBox2";
            this.myTextBox2.Size = new System.Drawing.Size(172, 16);
            this.myTextBox2.TabIndex = 3;
            this.myTextBox2.WaterText = "";
            this.myTextBox2.TextChanged += new System.EventHandler(this.myTextBox1_TextChanged);
            this.myTextBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.myTextBox1_MouseDown);
            this.myTextBox2.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // MetroTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "MetroTextBox";
            this.Size = new System.Drawing.Size(195, 30);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyTextBox myTextBox2;
    }
}
