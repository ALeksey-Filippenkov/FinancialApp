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
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            operationHistory = new Label();
            exitButton = new Button();
            seachButton = new Button();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(302, 39);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 45);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 1;
            label1.Text = "Введите дату";
            // 
            // operationHistory
            // 
            operationHistory.AutoSize = true;
            operationHistory.Location = new Point(42, 304);
            operationHistory.Name = "operationHistory";
            operationHistory.Size = new Size(115, 15);
            operationHistory.TabIndex = 2;
            operationHistory.Text = "История переводов";
            // 
            // exitButton
            // 
            exitButton.Location = new Point(42, 397);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 3;
            exitButton.Text = "Назад";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // seachButton
            // 
            seachButton.Location = new Point(670, 397);
            seachButton.Name = "seachButton";
            seachButton.Size = new Size(75, 23);
            seachButton.TabIndex = 4;
            seachButton.Text = "Поиск";
            seachButton.UseVisualStyleBackColor = true;
            seachButton.Click += seachButton_Click;
            // 
            // OperationSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(seachButton);
            Controls.Add(exitButton);
            Controls.Add(operationHistory);
            Controls.Add(label1);
            Controls.Add(dateTimePicker1);
            Name = "OperationSearch";
            Text = "Поиск операции";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Label operationHistory;
        private Button exitButton;
        private Button seachButton;
    }
}