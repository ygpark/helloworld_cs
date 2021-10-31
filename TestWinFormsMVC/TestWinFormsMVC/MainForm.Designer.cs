
namespace TestWinFormsMVC
{
    partial class MainForm
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
            this.btnGetNameFromNewForm = new System.Windows.Forms.Button();
            this.btnGetEventFromModel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGetNameFromNewForm
            // 
            this.btnGetNameFromNewForm.Location = new System.Drawing.Point(112, 60);
            this.btnGetNameFromNewForm.Name = "btnGetNameFromNewForm";
            this.btnGetNameFromNewForm.Size = new System.Drawing.Size(138, 23);
            this.btnGetNameFromNewForm.TabIndex = 1;
            this.btnGetNameFromNewForm.Text = "새 창에서 이름 입력";
            this.btnGetNameFromNewForm.UseVisualStyleBackColor = true;
            this.btnGetNameFromNewForm.Click += new System.EventHandler(this.btnGetNameFromNewForm_Click);
            // 
            // btnGetEventFromModel
            // 
            this.btnGetEventFromModel.Location = new System.Drawing.Point(112, 89);
            this.btnGetEventFromModel.Name = "btnGetEventFromModel";
            this.btnGetEventFromModel.Size = new System.Drawing.Size(170, 23);
            this.btnGetEventFromModel.TabIndex = 2;
            this.btnGetEventFromModel.Text = "모델 바꿔 이벤트 받기";
            this.btnGetEventFromModel.UseVisualStyleBackColor = true;
            this.btnGetEventFromModel.Click += new System.EventHandler(this.btnGetEventFromModel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "이름 :";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(57, 65);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 12);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "______";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 132);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetEventFromModel);
            this.Controls.Add(this.btnGetNameFromNewForm);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGetNameFromNewForm;
        private System.Windows.Forms.Button btnGetEventFromModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelName;
    }
}

