namespace client
{
    partial class Games
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            logoutBttn = new Button();
            clientNameTxtBox = new TextBox();
            nrSeatsTxtBox = new TextBox();
            Name = new Label();
            label1 = new Label();
            buyBttn = new Button();
            availableBttn = new Button();
            refreshBttn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(27, 32);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(502, 340);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            // 
            // logoutBttn
            // 
            logoutBttn.Location = new Point(38, 401);
            logoutBttn.Name = "logoutBttn";
            logoutBttn.Size = new Size(94, 29);
            logoutBttn.TabIndex = 1;
            logoutBttn.Text = "Log out";
            logoutBttn.UseVisualStyleBackColor = true;
            logoutBttn.Click += logoutBttn_Click;
            // 
            // clientNameTxtBox
            // 
            clientNameTxtBox.Location = new Point(637, 32);
            clientNameTxtBox.Name = "clientNameTxtBox";
            clientNameTxtBox.Size = new Size(125, 27);
            clientNameTxtBox.TabIndex = 2;
            // 
            // nrSeatsTxtBox
            // 
            nrSeatsTxtBox.Location = new Point(637, 80);
            nrSeatsTxtBox.Name = "nrSeatsTxtBox";
            nrSeatsTxtBox.Size = new Size(125, 27);
            nrSeatsTxtBox.TabIndex = 3;
            // 
            // Name
            // 
            Name.AutoSize = true;
            Name.Location = new Point(571, 37);
            Name.Name = "Name";
            Name.Size = new Size(49, 20);
            Name.TabIndex = 4;
            Name.Text = "Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(570, 83);
            label1.Name = "label1";
            label1.Size = new Size(44, 20);
            label1.TabIndex = 5;
            label1.Text = "Seats";
            // 
            // buyBttn
            // 
            buyBttn.Location = new Point(602, 273);
            buyBttn.Name = "buyBttn";
            buyBttn.Size = new Size(160, 29);
            buyBttn.TabIndex = 6;
            buyBttn.Text = "Buy";
            buyBttn.UseVisualStyleBackColor = true;
            buyBttn.Click += buyBttn_Click;
            // 
            // availableBttn
            // 
            availableBttn.Location = new Point(602, 308);
            availableBttn.Name = "availableBttn";
            availableBttn.Size = new Size(160, 29);
            availableBttn.TabIndex = 7;
            availableBttn.Text = "See available games";
            availableBttn.UseVisualStyleBackColor = true;
            availableBttn.Click += availableBttn_Click;
            // 
            // refreshBttn
            // 
            refreshBttn.Location = new Point(602, 343);
            refreshBttn.Name = "refreshBttn";
            refreshBttn.Size = new Size(160, 29);
            refreshBttn.TabIndex = 8;
            refreshBttn.Text = "Refresh";
            refreshBttn.UseVisualStyleBackColor = true;
            refreshBttn.Click += refreshBttn_Click;
            // 
            // Games
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(refreshBttn);
            Controls.Add(availableBttn);
            Controls.Add(buyBttn);
            Controls.Add(label1);
            Controls.Add(Name);
            Controls.Add(nrSeatsTxtBox);
            Controls.Add(clientNameTxtBox);
            Controls.Add(logoutBttn);
            Controls.Add(dataGridView1);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatWindow_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dataGridView1;
        private Button logoutBttn;
        private TextBox clientNameTxtBox;
        private TextBox nrSeatsTxtBox;
        private Label Name;
        private Label label1;
        private Button buyBttn;
        private Button availableBttn;
        private Button refreshBttn;
    }
}