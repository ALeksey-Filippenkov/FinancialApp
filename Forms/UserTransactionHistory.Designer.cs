namespace FinancialApp.Forms
{
    partial class UserTransactionHistory
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
            backButton = new Button();
            historyOperationDataGridView = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)historyOperationDataGridView).BeginInit();
            SuspendLayout();
            // 
            // backButton
            // 
            backButton.Location = new Point(80, 406);
            backButton.Name = "backButton";
            backButton.Size = new Size(75, 23);
            backButton.TabIndex = 1;
            backButton.Text = "Назад";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += BackButton_Click;
            // 
            // historyOperationDataGridView
            // 
            historyOperationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyOperationDataGridView.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6 });
            historyOperationDataGridView.Location = new Point(80, 12);
            historyOperationDataGridView.Name = "historyOperationDataGridView";
            historyOperationDataGridView.RowTemplate.Height = 25;
            historyOperationDataGridView.Size = new Size(645, 342);
            historyOperationDataGridView.TabIndex = 8;
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
            // UserTransactionHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(historyOperationDataGridView);
            Controls.Add(backButton);
            Name = "UserTransactionHistory";
            Text = "Операции пользователя";
            ((System.ComponentModel.ISupportInitialize)historyOperationDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button backButton;
        private DataGridView historyOperationDataGridView;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
    }
}