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
            monthCalendar1 = new MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)historyAdministratorsActionsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // historyAdministratorsActionsDataGridView
            // 
            historyAdministratorsActionsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyAdministratorsActionsDataGridView.Location = new Point(358, 12);
            historyAdministratorsActionsDataGridView.Name = "historyAdministratorsActionsDataGridView";
            historyAdministratorsActionsDataGridView.RowTemplate.Height = 25;
            historyAdministratorsActionsDataGridView.Size = new Size(743, 351);
            historyAdministratorsActionsDataGridView.TabIndex = 22;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(27, 12);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 23;
            // 
            // HistoryAdministratorsActions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 450);
            Controls.Add(monthCalendar1);
            Controls.Add(historyAdministratorsActionsDataGridView);
            Name = "HistoryAdministratorsActions";
            Text = "HistoryAdministratorsActions";
            ((System.ComponentModel.ISupportInitialize)historyAdministratorsActionsDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView historyAdministratorsActionsDataGridView;
        private MonthCalendar monthCalendar1;
    }
}