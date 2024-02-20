namespace FinancialApp.Forms
{
    partial class HistoryAdministratorsActions
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
            historyAdministratorsActionsDataGridView = new DataGridView();
            label1 = new Label();
            nameTextBox = new TextBox();
            backButton = new Button();
            excelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)historyAdministratorsActionsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // historyAdministratorsActionsDataGridView
            // 
            historyAdministratorsActionsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyAdministratorsActionsDataGridView.Location = new Point(35, 45);
            historyAdministratorsActionsDataGridView.Name = "historyAdministratorsActionsDataGridView";
            historyAdministratorsActionsDataGridView.RowTemplate.Height = 25;
            historyAdministratorsActionsDataGridView.Size = new Size(903, 348);
            historyAdministratorsActionsDataGridView.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 16);
            label1.Name = "label1";
            label1.Size = new Size(158, 15);
            label1.TabIndex = 25;
            label1.Text = "Введите данные для поиска";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(270, 16);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(456, 23);
            nameTextBox.TabIndex = 24;
            nameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // backButton
            // 
            backButton.Location = new Point(35, 415);
            backButton.Name = "backButton";
            backButton.Size = new Size(75, 23);
            backButton.TabIndex = 26;
            backButton.Text = "Назад";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += BackButton_Click;
            // 
            // excelButton
            // 
            excelButton.Location = new Point(793, 415);
            excelButton.Name = "excelButton";
            excelButton.Size = new Size(145, 23);
            excelButton.TabIndex = 27;
            excelButton.Text = "Выгрузить в Excel";
            excelButton.UseVisualStyleBackColor = true;
            excelButton.Click += excelButton_Click;
            // 
            // HistoryAdministratorsActions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 450);
            Controls.Add(excelButton);
            Controls.Add(backButton);
            Controls.Add(label1);
            Controls.Add(nameTextBox);
            Controls.Add(historyAdministratorsActionsDataGridView);
            Name = "HistoryAdministratorsActions";
            Text = "HistoryAdministratorsActions";
            ((System.ComponentModel.ISupportInitialize)historyAdministratorsActionsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView historyAdministratorsActionsDataGridView;
        private Label label1;
        private TextBox nameTextBox;
        private Button backButton;
        private Button excelButton;
    }
}