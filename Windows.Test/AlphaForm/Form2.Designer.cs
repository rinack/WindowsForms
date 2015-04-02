namespace Windows.Test.AlphaForm
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.alphaFormTransformer1 = new Windows.Forms.Controls.AlphaForm.AlphaFormTransformer();
            this.surfButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.infoLabel = new System.Windows.Forms.Label();
            this.gbLink = new System.Windows.Forms.LinkLabel();
            this.rdLink = new System.Windows.Forms.LinkLabel();
            this.blogLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.offButton = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.surfURL = new System.Windows.Forms.TextBox();
            this.alphaFormTransformer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // alphaFormTransformer1
            // 
            this.alphaFormTransformer1.Controls.Add(this.surfButton);
            this.alphaFormTransformer1.Controls.Add(this.panel1);
            this.alphaFormTransformer1.Controls.Add(this.gbLink);
            this.alphaFormTransformer1.Controls.Add(this.rdLink);
            this.alphaFormTransformer1.Controls.Add(this.blogLink);
            this.alphaFormTransformer1.Controls.Add(this.label1);
            this.alphaFormTransformer1.Controls.Add(this.label2);
            this.alphaFormTransformer1.Controls.Add(this.offButton);
            this.alphaFormTransformer1.Controls.Add(this.webBrowser);
            this.alphaFormTransformer1.Controls.Add(this.surfURL);
            this.alphaFormTransformer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alphaFormTransformer1.DragSleep = ((uint)(30u));
            this.alphaFormTransformer1.Location = new System.Drawing.Point(0, 0);
            this.alphaFormTransformer1.Name = "alphaFormTransformer1";
            this.alphaFormTransformer1.Size = new System.Drawing.Size(950, 671);
            this.alphaFormTransformer1.TabIndex = 0;
            this.alphaFormTransformer1.Paint += new System.Windows.Forms.PaintEventHandler(this.alphaFormTransformer1_Paint);
            // 
            // surfButton
            // 
            this.surfButton.Location = new System.Drawing.Point(54, 390);
            this.surfButton.Name = "surfButton";
            this.surfButton.Size = new System.Drawing.Size(75, 23);
            this.surfButton.TabIndex = 25;
            this.surfButton.Text = "Surf";
            this.surfButton.UseVisualStyleBackColor = true;
            this.surfButton.Click += new System.EventHandler(this.surfButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.Controls.Add(this.infoLabel);
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(300, 539);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 25);
            this.panel1.TabIndex = 29;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.infoLabel.Location = new System.Drawing.Point(3, 4);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(537, 18);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Our Most Extraordinary 3D Software";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoLabel.Click += new System.EventHandler(this.infoLabel_Click);
            // 
            // gbLink
            // 
            this.gbLink.BackColor = System.Drawing.Color.Transparent;
            this.gbLink.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLink.LinkColor = System.Drawing.Color.Lavender;
            this.gbLink.Location = new System.Drawing.Point(862, 249);
            this.gbLink.Name = "gbLink";
            this.gbLink.Size = new System.Drawing.Size(68, 50);
            this.gbLink.TabIndex = 28;
            this.gbLink.TabStop = true;
            this.gbLink.Text = "GroBoto";
            this.gbLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gbLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.gbLink_LinkClicked);
            // 
            // rdLink
            // 
            this.rdLink.BackColor = System.Drawing.Color.Transparent;
            this.rdLink.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdLink.LinkColor = System.Drawing.Color.Lavender;
            this.rdLink.Location = new System.Drawing.Point(868, 403);
            this.rdLink.Name = "rdLink";
            this.rdLink.Size = new System.Drawing.Size(57, 58);
            this.rdLink.TabIndex = 27;
            this.rdLink.TabStop = true;
            this.rdLink.Text = "R&&D";
            this.rdLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.rdLink_LinkClicked);
            // 
            // blogLink
            // 
            this.blogLink.BackColor = System.Drawing.Color.Transparent;
            this.blogLink.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blogLink.LinkColor = System.Drawing.Color.Lavender;
            this.blogLink.Location = new System.Drawing.Point(865, 463);
            this.blogLink.Name = "blogLink";
            this.blogLink.Size = new System.Drawing.Size(70, 60);
            this.blogLink.TabIndex = 26;
            this.blogLink.TabStop = true;
            this.blogLink.Text = "Blog";
            this.blogLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.blogLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.blogLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(20, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 29);
            this.label1.TabIndex = 22;
            this.label1.Text = "GroBoto TV";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(16, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "All GroBoto All the Time";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // offButton
            // 
            this.offButton.BackColor = System.Drawing.Color.Transparent;
            this.offButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.offButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.offButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.offButton.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.offButton.Location = new System.Drawing.Point(76, 106);
            this.offButton.Name = "offButton";
            this.offButton.Size = new System.Drawing.Size(93, 94);
            this.offButton.TabIndex = 21;
            this.offButton.Text = "Off";
            this.offButton.UseVisualStyleBackColor = false;
            this.offButton.Click += new System.EventHandler(this.offButton_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(309, 170);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(534, 363);
            this.webBrowser.TabIndex = 20;
            this.webBrowser.Url = new System.Uri("http://www.groboto.com", System.UriKind.Absolute);
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // surfURL
            // 
            this.surfURL.Location = new System.Drawing.Point(22, 364);
            this.surfURL.Name = "surfURL";
            this.surfURL.Size = new System.Drawing.Size(138, 21);
            this.surfURL.TabIndex = 24;
            this.surfURL.Text = "http://www.groboto.com";
            this.surfURL.TextChanged += new System.EventHandler(this.surfURL_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 671);
            this.Controls.Add(this.alphaFormTransformer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.alphaFormTransformer1.ResumeLayout(false);
            this.alphaFormTransformer1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.Controls.AlphaForm.AlphaFormTransformer alphaFormTransformer1;
        private System.Windows.Forms.Button surfButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.LinkLabel gbLink;
        private System.Windows.Forms.LinkLabel rdLink;
        private System.Windows.Forms.LinkLabel blogLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button offButton;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox surfURL;

    }
}