namespace clientFX
{
    partial class Login
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
            usernameTxtBox = new TextBox();
            passwordTxtBox = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(170, 146);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 0;
            label1.Text = "Username:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(170, 192);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 1;
            label2.Text = "Password:";
            // 
            // usernameTxtBox
            // 
            usernameTxtBox.Location = new Point(285, 139);
            usernameTxtBox.Name = "usernameTxtBox";
            usernameTxtBox.Size = new Size(125, 27);
            usernameTxtBox.TabIndex = 2;
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Location = new Point(285, 192);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.PasswordChar = '*';
            passwordTxtBox.Size = new Size(125, 27);
            passwordTxtBox.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(299, 279);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 4;
            button1.Text = "Log in";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(passwordTxtBox);
            Controls.Add(usernameTxtBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox usernameTxtBox;
        private TextBox passwordTxtBox;
        private Button button1;
    }
}