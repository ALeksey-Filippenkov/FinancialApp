namespace FinancialApp
{
    partial class MoneyTransfer
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
            phoneNumerTransferInput = new TextBox();
            moneyTransferInput = new TextBox();
            label2 = new Label();
            currencyList = new ListBox();
            label3 = new Label();
            exitButton = new Button();
            transferButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 26);
            label1.Name = "label1";
            label1.Size = new Size(338, 15);
            label1.TabIndex = 0;
            label1.Text = "Введите номер телефона , кому вы хотите отправтиь деньги";
            // 
            // phoneNumerTransferInput
            // 
            phoneNumerTransferInput.Location = new Point(54, 64);
            phoneNumerTransferInput.Name = "phoneNumerTransferInput";
            phoneNumerTransferInput.Size = new Size(338, 23);
            phoneNumerTransferInput.TabIndex = 1;
            // 
            // moneyTransferInput
            // 
            moneyTransferInput.Location = new Point(54, 257);
            moneyTransferInput.Name = "moneyTransferInput";
            moneyTransferInput.Size = new Size(338, 23);
            moneyTransferInput.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 228);
            label2.Name = "label2";
            label2.Size = new Size(165, 15);
            label2.TabIndex = 3;
            label2.Text = "Введите сумму для перевода";
            // 
            // currencyList
            // 
            currencyList.FormattingEnabled = true;
            currencyList.ItemHeight = 15;
            currencyList.Items.AddRange(new object[] { "RUB", "USD", "EUR" });
            currencyList.Location = new Point(54, 133);
            currencyList.Name = "currencyList";
            currencyList.Size = new Size(338, 79);
            currencyList.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(54, 102);
            label3.Name = "label3";
            label3.Size = new Size(104, 15);
            label3.TabIndex = 5;
            label3.Text = "Выберите валюту";
            // 
            // exitButton
            // 
            exitButton.Location = new Point(54, 364);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 6;
            exitButton.Text = "Назад";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // transferButton
            // 
            transferButton.Location = new Point(500, 364);
            transferButton.Name = "transferButton";
            transferButton.Size = new Size(232, 23);
            transferButton.TabIndex = 7;
            transferButton.Text = "Перевести деньги";
            transferButton.UseVisualStyleBackColor = true;
            transferButton.Click += transferButton_Click;
            // 
            // MoneyTransfer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(transferButton);
            Controls.Add(exitButton);
            Controls.Add(label3);
            Controls.Add(currencyList);
            Controls.Add(label2);
            Controls.Add(moneyTransferInput);
            Controls.Add(phoneNumerTransferInput);
            Controls.Add(label1);
            Name = "MoneyTransfer";
            Text = "Перевод денег";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox phoneNumerTransferInput;
        private TextBox moneyTransferInput;
        private Label label2;
        private ListBox currencyList;
        private Label label3;
        private Button exitButton;
        private Button transferButton;
    }
}