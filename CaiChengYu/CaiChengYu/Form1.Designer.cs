namespace CaiChengYu
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOcrRead = new System.Windows.Forms.Button();
            this.btnChooseImg = new System.Windows.Forms.Button();
            this.textImage = new System.Windows.Forms.TextBox();
            this.picSource = new System.Windows.Forms.PictureBox();
            this.textResults = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOcrRead
            // 
            this.btnOcrRead.Location = new System.Drawing.Point(240, 218);
            this.btnOcrRead.Name = "btnOcrRead";
            this.btnOcrRead.Size = new System.Drawing.Size(75, 23);
            this.btnOcrRead.TabIndex = 0;
            this.btnOcrRead.Text = "识别";
            this.btnOcrRead.UseVisualStyleBackColor = true;
            this.btnOcrRead.Click += new System.EventHandler(this.btnOcrRead_Click);
            // 
            // btnChooseImg
            // 
            this.btnChooseImg.Location = new System.Drawing.Point(1, 218);
            this.btnChooseImg.Name = "btnChooseImg";
            this.btnChooseImg.Size = new System.Drawing.Size(75, 23);
            this.btnChooseImg.TabIndex = 1;
            this.btnChooseImg.Text = "选取图片";
            this.btnChooseImg.UseVisualStyleBackColor = true;
            this.btnChooseImg.Click += new System.EventHandler(this.btnChooseImg_Click);
            // 
            // textImage
            // 
            this.textImage.Location = new System.Drawing.Point(1, 191);
            this.textImage.Name = "textImage";
            this.textImage.Size = new System.Drawing.Size(314, 21);
            this.textImage.TabIndex = 2;
            // 
            // picSource
            // 
            this.picSource.Location = new System.Drawing.Point(1, 1);
            this.picSource.Name = "picSource";
            this.picSource.Size = new System.Drawing.Size(253, 180);
            this.picSource.TabIndex = 3;
            this.picSource.TabStop = false;
            // 
            // textResults
            // 
            this.textResults.Location = new System.Drawing.Point(1, 247);
            this.textResults.Multiline = true;
            this.textResults.Name = "textResults";
            this.textResults.Size = new System.Drawing.Size(314, 96);
            this.textResults.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 362);
            this.Controls.Add(this.picSource);
            this.Controls.Add(this.textResults);
            this.Controls.Add(this.textImage);
            this.Controls.Add(this.btnChooseImg);
            this.Controls.Add(this.btnOcrRead);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOcrRead;
        private System.Windows.Forms.Button btnChooseImg;
        private System.Windows.Forms.TextBox textImage;
        private System.Windows.Forms.PictureBox picSource;
        private System.Windows.Forms.TextBox textResults;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

