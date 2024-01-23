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
            numberPerson = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            nameTextBox = new TextBox();
            label1 = new Label();
            seachButton = new Button();
            ((System.ComponentModel.ISupportInitialize)userInformationDataGridView).BeginInit();
            SuspendLayout();
            // 
            // exitButton
            // 
            exitButton.Location = new Point(12, 497);
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
            userInformationDataGridView.Columns.AddRange(new DataGridViewColumn[] { numberPerson, Column1, Column2 });
            userInformationDataGridView.Location = new Point(287, 22);
            userInformationDataGridView.Name = "userInformationDataGridView";
            userInformationDataGridView.RowTemplate.Height = 25;
            userInformationDataGridView.Size = new Size(722, 474);
            userInformationDataGridView.TabIndex = 16;
            userInformationDataGridView.Visible = false;
            // 
            // numberPerson
            // 
            numberPerson.FillWeight = 130F;
            numberPerson.Frozen = true;
            numberPerson.HeaderText = "Пользователь №";
            numberPerson.Name = "numberPerson";
            numberPerson.ReadOnly = true;
            numberPerson.Width = 130;
            // 
            // Column1
            // 
            Column1.Frozen = true;
            Column1.HeaderText = "Данные";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.Frozen = true;
            Column2.HeaderText = "Значение";
            Column2.Name = "Column2";
            Column2.Width = 400;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(39, 54);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(217, 23);
            nameTextBox.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 22);
            label1.Name = "label1";
            label1.Size = new Size(217, 15);
            label1.TabIndex = 18;
            label1.Text = "Введите имя пользователя для поиска";
            // 
            // seachButton
            // 
            seachButton.Location = new Point(39, 93);
            seachButton.Name = "seachButton";
            seachButton.Size = new Size(75, 23);
            seachButton.TabIndex = 19;
            seachButton.Text = "Поиск";
            seachButton.UseVisualStyleBackColor = true;
            seachButton.Click += seachButton_Click;
            // 
            // ShowUserInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1051, 532);
            Controls.Add(seachButton);
            Controls.Add(label1);
            Controls.Add(nameTextBox);
            Controls.Add(userInformationDataGridView);
            Controls.Add(exitButton);
            Name = "ShowUserInformation";
            Text = "Информация о пользователе";
            ((System.ComponentModel.ISupportInitialize)userInformationDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exitButton;
        private DataGridView userInformationDataGridView;
        private TextBox nameTextBox;
        private Label label1;
        private Button seachButton;
        private DataGridViewTextBoxColumn numberPerson;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}