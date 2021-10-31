namespace TestDynamicDll
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
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnDll1 = new System.Windows.Forms.Button();
			this.btnDll2 = new System.Windows.Forms.Button();
			this.txtOut = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnDll1_Del = new System.Windows.Forms.Button();
			this.btnDll2_Del = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnDll1
			// 
			this.btnDll1.Location = new System.Drawing.Point(6, 20);
			this.btnDll1.Name = "btnDll1";
			this.btnDll1.Size = new System.Drawing.Size(75, 23);
			this.btnDll1.TabIndex = 0;
			this.btnDll1.Text = "Dll 1";
			this.btnDll1.UseVisualStyleBackColor = true;
			this.btnDll1.Click += new System.EventHandler(this.btnDll1_Click);
			// 
			// btnDll2
			// 
			this.btnDll2.Location = new System.Drawing.Point(87, 20);
			this.btnDll2.Name = "btnDll2";
			this.btnDll2.Size = new System.Drawing.Size(75, 23);
			this.btnDll2.TabIndex = 1;
			this.btnDll2.Text = "Dll 2";
			this.btnDll2.UseVisualStyleBackColor = true;
			this.btnDll2.Click += new System.EventHandler(this.btnDll2_Click);
			// 
			// txtOut
			// 
			this.txtOut.Location = new System.Drawing.Point(42, 128);
			this.txtOut.Multiline = true;
			this.txtOut.Name = "txtOut";
			this.txtOut.Size = new System.Drawing.Size(217, 103);
			this.txtOut.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDll1);
			this.groupBox1.Controls.Add(this.btnDll2);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(174, 49);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "인보크 방식";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnDll2_Del);
			this.groupBox2.Controls.Add(this.btnDll1_Del);
			this.groupBox2.Location = new System.Drawing.Point(12, 67);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(174, 55);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "대리자 방식";
			// 
			// btnDll1_Del
			// 
			this.btnDll1_Del.Location = new System.Drawing.Point(6, 20);
			this.btnDll1_Del.Name = "btnDll1_Del";
			this.btnDll1_Del.Size = new System.Drawing.Size(75, 23);
			this.btnDll1_Del.TabIndex = 0;
			this.btnDll1_Del.Text = "Dll 1";
			this.btnDll1_Del.UseVisualStyleBackColor = true;
			this.btnDll1_Del.Click += new System.EventHandler(this.btnDll1_Del_Click);
			// 
			// btnDll2_Del
			// 
			this.btnDll2_Del.Location = new System.Drawing.Point(87, 20);
			this.btnDll2_Del.Name = "btnDll2_Del";
			this.btnDll2_Del.Size = new System.Drawing.Size(75, 23);
			this.btnDll2_Del.TabIndex = 1;
			this.btnDll2_Del.Text = "Dll 2";
			this.btnDll2_Del.UseVisualStyleBackColor = true;
			this.btnDll2_Del.Click += new System.EventHandler(this.btnDll2_Del_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(345, 248);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtOut);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDll1;
		private System.Windows.Forms.Button btnDll2;
		private System.Windows.Forms.TextBox txtOut;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnDll2_Del;
		private System.Windows.Forms.Button btnDll1_Del;
	}
}

