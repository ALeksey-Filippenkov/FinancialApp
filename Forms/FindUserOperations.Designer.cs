namespace FinancialApp.Forms
{
    partial class FindUserOperations
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
            label2 = new Label();
            label3 = new Label();
            currencyTypeTextBox = new TextBox();
            personNameTextBox = new TextBox();
            button1 = new Button();
            historyOperationDataGridView = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            backButton = new Button();
            pritnExcelButton = new Button();
            monthCalendar1 = new MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)historyOperationDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 30);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 0;
            label1.Text = "Выберите дату";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 229);
            label2.Name = "label2";
            label2.Size = new Size(118, 15);
            label2.TabIndex = 1;
            label2.Text = "Введите тип валюты";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 277);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 2;
            label3.Text = "Введите имя";
            // 
            // currencyTypeTextBox
            // 
            currencyTypeTextBox.Location = new Point(207, 229);
            currencyTypeTextBox.Name = "currencyTypeTextBox";
            currencyTypeTextBox.Size = new Size(164, 23);
            currencyTypeTextBox.TabIndex = 3;
            // 
            // personNameTextBox
            // 
            personNameTextBox.Location = new Point(207, 277);
            personNameTextBox.Name = "personNameTextBox";
            personNameTextBox.Size = new Size(164, 23);
            personNameTextBox.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(30, 334);
            button1.Name = "button1";
            button1.Size = new Size(341, 23);
            button1.TabIndex = 6;
            button1.Text = "Поиск";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // historyOperationDataGridView
            // 
            historyOperationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyOperationDataGridView.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6 });
            historyOperationDataGridView.Location = new Point(427, 30);
            historyOperationDataGridView.Name = "historyOperationDataGridView";
            historyOperationDataGridView.RowTemplate.Height = 25;
            historyOperationDataGridView.Size = new Size(644, 327);
            historyOperationDataGridView.TabIndex = 7;
            // 
            // Column1
            // 
            Column1.Frozen = true;
            Column1.HeaderText = "Дата";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.Frozen = true;
            Column2.HeaderText = "Тип операции";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.Frozen = true;
            Column3.HeaderText = "Отправитель";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.Frozen = true;
            Column4.HeaderText = "Тип валюты";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.Frozen = true;
            Column5.HeaderText = "Сумма";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.Frozen = true;
            Column6.HeaderText = "Получатель";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // backButton
            // 
            backButton.Location = new Point(30, 396);
            backButton.Name = "backButton";
            backButton.Size = new Size(75, 23);
            backButton.TabIndex = 8;
            backButton.Text = "Назад";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += BackButton_Click;
            // 
            // pritnExcelButton
            // 
            pritnExcelButton.Location = new Point(657, 391);
            pritnExcelButton.Name = "pritnExcelButton";
            pritnExcelButton.Size = new Size(335, 23);
            pritnExcelButton.TabIndex = 9;
            pritnExcelButton.Text = "Выгрузить в Excel";
            pritnExcelButton.UseVisualStyleBackColor = true;
            pritnExcelButton.Click += PrintExcelButton_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(207, 30);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 10;
            // 
            // FindUserOperations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1108, 450);
            Controls.Add(monthCalendar1);
            Controls.Add(pritnExcelButton);
            Controls.Add(backButton);
            Controls.Add(historyOperationDataGridView);
            Controls.Add(button1);
            Controls.Add(personNameTextBox);
            Controls.Add(currencyTypeTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FindUserOperations";
            Text = "Поиcк операций пользователя";
            ((System.ComponentModel.ISupportInitialize)historyOperationDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox currencyTypeTextBox;
        private TextBox personNameTextBox;
        private Button button1;
        private DataGridView historyOperationDataGridView;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private Button backButton;
        private Button pritnExcelButton;
        private MonthCalendar monthCalendar1;
    }
}