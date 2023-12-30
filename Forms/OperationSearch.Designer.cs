namespace FinancialApp
{
    partial class OperationSearch
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
            label1 = new Label();
            operationHistory = new Label();
            exitButton = new Button();
            seachButton = new Button();
            label2 = new Label();
            currencyTypeTextBox = new TextBox();
            label3 = new Label();
            personRecipientTextBox = new TextBox();
            monthCalendar1 = new MonthCalendar();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 44);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 1;
            label1.Text = "Введите дату";
            // 
            // operationHistory
            // 
            operationHistory.AutoSize = true;
            operationHistory.Location = new Point(496, 36);
            operationHistory.Name = "operationHistory";
            operationHistory.Size = new Size(115, 15);
            operationHistory.TabIndex = 2;
            operationHistory.Text = "История переводов";
            // 
            // exitButton
            // 
            exitButton.Location = new Point(42, 485);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 3;
            exitButton.Text = "Назад";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // seachButton
            // 
            seachButton.Location = new Point(418, 485);
            seachButton.Name = "seachButton";
            seachButton.Size = new Size(424, 23);
            seachButton.TabIndex = 4;
            seachButton.Text = "Поиск";
            seachButton.UseVisualStyleBackColor = true;
            seachButton.Click += seachButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 210);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 5;
            label2.Text = "Тип валюты";
            // 
            // currencyTypeTextBox
            // 
            currencyTypeTextBox.Location = new Point(302, 210);
            currencyTypeTextBox.Name = "currencyTypeTextBox";
            currencyTypeTextBox.Size = new Size(100, 23);
            currencyTypeTextBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 251);
            label3.Name = "label3";
            label3.Size = new Size(98, 15);
            label3.TabIndex = 7;
            label3.Text = "Имя получателя";
            // 
            // personRecipientTextBox
            // 
            personRecipientTextBox.Location = new Point(302, 251);
            personRecipientTextBox.Name = "personRecipientTextBox";
            personRecipientTextBox.Size = new Size(100, 23);
            personRecipientTextBox.TabIndex = 8;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(302, 36);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 9;
            // 
            // OperationSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(854, 520);
            Controls.Add(monthCalendar1);
            Controls.Add(personRecipientTextBox);
            Controls.Add(label3);
            Controls.Add(currencyTypeTextBox);
            Controls.Add(label2);
            Controls.Add(seachButton);
            Controls.Add(exitButton);
            Controls.Add(operationHistory);
            Controls.Add(label1);
            Name = "OperationSearch";
            Text = "Поиск операции";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label operationHistory;
        private Button exitButton;
        private Button seachButton;
        private Label label2;
        private TextBox currencyTypeTextBox;
        private Label label3;
        private TextBox personRecipientTextBox;
        private MonthCalendar monthCalendar1;
    }
}