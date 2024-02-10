namespace FinancialApp.Forms
{
    partial class ShowUserInformation
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
            exitButton = new Button();
            userInformationDataGridView = new DataGridView();
            nameTextBox = new TextBox();
            label1 = new Label();
            printExcelButton = new Button();
            userStatusComboBox = new ComboBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)userInformationDataGridView).BeginInit();
            SuspendLayout();
            // 
            // exitButton
            // 
            exitButton.Location = new Point(39, 497);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 0;
            exitButton.Text = "Назад";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // userInformationDataGridView
            // 
            userInformationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            userInformationDataGridView.Location = new Point(12, 51);
            userInformationDataGridView.Name = "userInformationDataGridView";
            userInformationDataGridView.RowTemplate.Height = 25;
            userInformationDataGridView.Size = new Size(1206, 428);
            userInformationDataGridView.TabIndex = 21;
            userInformationDataGridView.CellEndEdit += DataGridViewBooks_CellEndEdit;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(247, 22);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(456, 23);
            nameTextBox.TabIndex = 17;
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(217, 15);
            label1.TabIndex = 18;
            label1.Text = "Введите имя пользователя для поиска";
            // 
            // printExcelButton
            // 
            printExcelButton.Location = new Point(1001, 497);
            printExcelButton.Name = "printExcelButton";
            printExcelButton.Size = new Size(217, 23);
            printExcelButton.TabIndex = 20;
            printExcelButton.Text = "Выгрузить в EXCEL";
            printExcelButton.UseVisualStyleBackColor = true;
            printExcelButton.Click += printExcelButton_Click;
            // 
            // userStatusComboBox
            // 
            userStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            userStatusComboBox.FormattingEnabled = true;
            userStatusComboBox.Items.AddRange(new object[] { "Все", "Забанен", "Не забаненные" });
            userStatusComboBox.Location = new Point(969, 22);
            userStatusComboBox.Name = "userStatusComboBox";
            userStatusComboBox.Size = new Size(121, 23);
            userStatusComboBox.TabIndex = 23;
            userStatusComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(769, 25);
            label2.Name = "label2";
            label2.Size = new Size(176, 15);
            label2.TabIndex = 24;
            label2.Text = "Выберите статус пользователя";
            // 
            // ShowUserInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 532);
            Controls.Add(label2);
            Controls.Add(userStatusComboBox);
            Controls.Add(printExcelButton);
            Controls.Add(label1);
            Controls.Add(nameTextBox);
            Controls.Add(userInformationDataGridView);
            Controls.Add(exitButton);
            Name = "ShowUserInformation";
            Text = "Информация о пользователе";
            Load += ShowUserInformation_Load;
            ((System.ComponentModel.ISupportInitialize)userInformationDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exitButton;
        private DataGridView userInformationDataGridView;
        private TextBox nameTextBox;
        private Label label1;
        private Button printExcelButton;
        private ComboBox userStatusComboBox;
        private Label label2;
    }
}