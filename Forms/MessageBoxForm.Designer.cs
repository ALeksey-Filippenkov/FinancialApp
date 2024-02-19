namespace FinancialApp.Forms
{
    partial class MessageBoxForm
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
            messageLabel = new Label();
            bankButton = new Button();
            administratorButton = new Button();
            backButton = new Button();
            SuspendLayout();
            // 
            // messageLabel
            // 
            messageLabel.AutoSize = true;
            messageLabel.Location = new Point(78, 20);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(267, 15);
            messageLabel.TabIndex = 0;
            messageLabel.Text = "В какой из личных кабинетов вы хотите войти?";
            // 
            // bankButton
            // 
            bankButton.Location = new Point(23, 72);
            bankButton.Name = "bankButton";
            bankButton.Size = new Size(165, 23);
            bankButton.TabIndex = 1;
            bankButton.Text = "Финансы";
            bankButton.UseVisualStyleBackColor = true;
            bankButton.Click += BankButton_Click;
            // 
            // administratorButton
            // 
            administratorButton.Location = new Point(235, 72);
            administratorButton.Name = "administratorButton";
            administratorButton.Size = new Size(165, 23);
            administratorButton.TabIndex = 2;
            administratorButton.Text = "Администратор";
            administratorButton.UseVisualStyleBackColor = true;
            administratorButton.Click += AdministratorButton_Click;
            // 
            // backButton
            // 
            backButton.Location = new Point(172, 112);
            backButton.Name = "backButton";
            backButton.Size = new Size(80, 23);
            backButton.TabIndex = 3;
            backButton.Text = "Назад";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += BackButton_Click;
            // 
            // MessageBoxForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 162);
            Controls.Add(backButton);
            Controls.Add(administratorButton);
            Controls.Add(bankButton);
            Controls.Add(messageLabel);
            Name = "MessageBoxForm";
            Text = "Выбор личного кабинета";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label messageLabel;
        private Button bankButton;
        private Button administratorButton;
        private Button backButton;
    }
}