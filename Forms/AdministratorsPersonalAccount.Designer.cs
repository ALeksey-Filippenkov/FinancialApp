namespace FinancialApp.Forms
{
    partial class AdministratorsPersonalAccount
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
            button1 = new Button();
            dateTime = new Label();
            workingWithUserButton = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            saveButton = new Button();
            showUserPersonalDataButton = new Button();
            banUserButton = new Button();
            deleteUserButton = new Button();
            findUserOperationsButton = new Button();
            restoreUserButton = new Button();
            workingWithAdministratorButton = new Button();
            searchAdministratorActionsButton = new Button();
            deleteAdministratorButton = new Button();
            showAdministratorDataButton = new Button();
            addAdministratorButton = new Button();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 415);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Выход";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // dateTime
            // 
            dateTime.AutoSize = true;
            dateTime.Location = new Point(41, 18);
            dateTime.Name = "dateTime";
            dateTime.Size = new Size(80, 15);
            dateTime.TabIndex = 1;
            dateTime.Text = "Текущая дата";
            // 
            // workingWithUserButton
            // 
            workingWithUserButton.Location = new Point(41, 59);
            workingWithUserButton.Name = "workingWithUserButton";
            workingWithUserButton.Size = new Size(202, 23);
            workingWithUserButton.TabIndex = 2;
            workingWithUserButton.Text = "Работа с пользователями";
            workingWithUserButton.UseVisualStyleBackColor = true;
            workingWithUserButton.Click += WorkingWithUserButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(727, 93);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 23);
            textBox1.TabIndex = 4;
            textBox1.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(306, 18);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 5;
            label1.Text = "Выберите действие";
            label1.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(727, 63);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 6;
            label2.Text = "Введите имя";
            label2.Visible = false;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(727, 140);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(161, 23);
            saveButton.TabIndex = 7;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Visible = false;
            saveButton.Click += SaveButton_Click;
            // 
            // showUserPersonalDataButton
            // 
            showUserPersonalDataButton.Location = new Point(344, 59);
            showUserPersonalDataButton.Name = "showUserPersonalDataButton";
            showUserPersonalDataButton.Size = new Size(207, 23);
            showUserPersonalDataButton.TabIndex = 8;
            showUserPersonalDataButton.Text = "Показать личные данные пользоавателя";
            showUserPersonalDataButton.UseVisualStyleBackColor = true;
            showUserPersonalDataButton.Visible = false;
            showUserPersonalDataButton.Click += ShowUserPersonalDataButton_Click;
            // 
            // banUserButton
            // 
            banUserButton.Location = new Point(344, 102);
            banUserButton.Name = "banUserButton";
            banUserButton.Size = new Size(207, 23);
            banUserButton.TabIndex = 9;
            banUserButton.Text = "Забанить пользователя";
            banUserButton.UseVisualStyleBackColor = true;
            banUserButton.Visible = false;
            banUserButton.Click += BanUserButton_Click;
            // 
            // deleteUserButton
            // 
            deleteUserButton.Location = new Point(344, 146);
            deleteUserButton.Name = "deleteUserButton";
            deleteUserButton.Size = new Size(207, 23);
            deleteUserButton.TabIndex = 10;
            deleteUserButton.Text = "Удалить пользователя";
            deleteUserButton.UseVisualStyleBackColor = true;
            deleteUserButton.Visible = false;
            deleteUserButton.Click += DeleteUserButton_Click;
            // 
            // findUserOperationsButton
            // 
            findUserOperationsButton.Location = new Point(344, 194);
            findUserOperationsButton.Name = "findUserOperationsButton";
            findUserOperationsButton.Size = new Size(204, 23);
            findUserOperationsButton.TabIndex = 11;
            findUserOperationsButton.Text = "Найти операции пользователя";
            findUserOperationsButton.UseVisualStyleBackColor = true;
            findUserOperationsButton.Visible = false;
            findUserOperationsButton.Click += FindUserOperationsButton_Click;
            // 
            // restoreUserButton
            // 
            restoreUserButton.Location = new Point(344, 238);
            restoreUserButton.Name = "restoreUserButton";
            restoreUserButton.Size = new Size(204, 23);
            restoreUserButton.TabIndex = 12;
            restoreUserButton.Text = "Восстановить пользователя";
            restoreUserButton.UseVisualStyleBackColor = true;
            restoreUserButton.Visible = false;
            restoreUserButton.Click += RestoreUserButton_Click;
            // 
            // workingWithAdministratorButton
            // 
            workingWithAdministratorButton.Location = new Point(41, 102);
            workingWithAdministratorButton.Name = "workingWithAdministratorButton";
            workingWithAdministratorButton.Size = new Size(202, 23);
            workingWithAdministratorButton.TabIndex = 13;
            workingWithAdministratorButton.Text = "Работа с администраторами";
            workingWithAdministratorButton.UseVisualStyleBackColor = true;
            workingWithAdministratorButton.Visible = false;
            workingWithAdministratorButton.Click += WorkingWithAdministratorButton_Click;
            // 
            // searchAdministratorActionsButton
            // 
            searchAdministratorActionsButton.Location = new Point(306, 194);
            searchAdministratorActionsButton.Name = "searchAdministratorActionsButton";
            searchAdministratorActionsButton.Size = new Size(204, 23);
            searchAdministratorActionsButton.TabIndex = 17;
            searchAdministratorActionsButton.Text = "Найти операции администратора";
            searchAdministratorActionsButton.UseVisualStyleBackColor = true;
            searchAdministratorActionsButton.Visible = false;
            // 
            // deleteAdministratorButton
            // 
            deleteAdministratorButton.Location = new Point(306, 146);
            deleteAdministratorButton.Name = "deleteAdministratorButton";
            deleteAdministratorButton.Size = new Size(207, 23);
            deleteAdministratorButton.TabIndex = 16;
            deleteAdministratorButton.Text = "Удалить администратора";
            deleteAdministratorButton.UseVisualStyleBackColor = true;
            deleteAdministratorButton.Visible = false;
            // 
            // showAdministratorDataButton
            // 
            showAdministratorDataButton.Location = new Point(306, 59);
            showAdministratorDataButton.Name = "showAdministratorDataButton";
            showAdministratorDataButton.Size = new Size(207, 23);
            showAdministratorDataButton.TabIndex = 14;
            showAdministratorDataButton.Text = "Показать данные администратора";
            showAdministratorDataButton.UseVisualStyleBackColor = true;
            showAdministratorDataButton.Visible = false;
            // 
            // addAdministratorButton
            // 
            addAdministratorButton.Location = new Point(306, 102);
            addAdministratorButton.Name = "addAdministratorButton";
            addAdministratorButton.Size = new Size(207, 23);
            addAdministratorButton.TabIndex = 15;
            addAdministratorButton.Text = "Добавить администратора";
            addAdministratorButton.UseVisualStyleBackColor = true;
            addAdministratorButton.Visible = false;
            addAdministratorButton.Click += AddAdministratorButton_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Добавить администратора из пользователей", "Добавить нового администратора" });
            comboBox1.Location = new Point(576, 102);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(312, 23);
            comboBox1.TabIndex = 18;
            comboBox1.Visible = false;
            // 
            // AdministratorsPersonalAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1153, 450);
            Controls.Add(comboBox1);
            Controls.Add(searchAdministratorActionsButton);
            Controls.Add(deleteAdministratorButton);
            Controls.Add(addAdministratorButton);
            Controls.Add(showAdministratorDataButton);
            Controls.Add(workingWithAdministratorButton);
            Controls.Add(restoreUserButton);
            Controls.Add(findUserOperationsButton);
            Controls.Add(deleteUserButton);
            Controls.Add(banUserButton);
            Controls.Add(showUserPersonalDataButton);
            Controls.Add(saveButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(workingWithUserButton);
            Controls.Add(dateTime);
            Controls.Add(button1);
            Name = "AdministratorsPersonalAccount";
            Text = "Личный кабинет администратора";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label dateTime;
        private Button workingWithUserButton;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Button saveButton;
        private Button showUserPersonalDataButton;
        private Button banUserButton;
        private Button deleteUserButton;
        private Button findUserOperationsButton;
        private Button restoreUserButton;
        private Button workingWithAdministratorButton;
        private Button searchAdministratorActionsButton;
        private Button deleteAdministratorButton;
        private Button showAdministratorDataButton;
        private Button addAdministratorButton;
        private ComboBox comboBox1;
    }
}