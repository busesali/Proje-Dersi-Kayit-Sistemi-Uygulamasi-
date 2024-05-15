namespace WinFormsApp5
{
    partial class Form12
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
            button2 = new Button();
            textBox1 = new TextBox();
            panel1 = new Panel();
            button1 = new Button();
            button3 = new Button();
            panel2 = new Panel();
            textBox2 = new TextBox();
            button4 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            listView1 = new ListView();
            buttonDelete = new Button();
            button5 = new Button();
            listView2 = new ListView();
            label4 = new Label();
            label5 = new Label();
            button6 = new Button();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label6 = new Label();
            textBox7 = new TextBox();
            label7 = new Label();
            textBox8 = new TextBox();
            textBox9 = new TextBox();
            textBox10 = new TextBox();
            button7 = new Button();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            button8 = new Button();
            listView3 = new ListView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Info;
            button2.Location = new Point(123, 86);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 12;
            button2.Text = "Kaydet";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Location = new Point(42, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(256, 27);
            textBox1.TabIndex = 11;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Linen;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button2);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(335, 129);
            panel1.TabIndex = 13;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 255, 192);
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(335, 40);
            button1.TabIndex = 14;
            button1.Text = "      Mesajlaşma İçin Karakter Sayısı Giriniz";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(192, 255, 192);
            button3.Location = new Point(12, 178);
            button3.Name = "button3";
            button3.Size = new Size(335, 49);
            button3.TabIndex = 16;
            button3.Text = "Öğrencinin Bir Ders İçin Seçebileceği Öğretmen Sayısını Giriniz";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Linen;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(button4);
            panel2.Location = new Point(12, 178);
            panel2.Name = "panel2";
            panel2.Size = new Size(335, 128);
            panel2.TabIndex = 15;
            panel2.Paint += panel2_Paint;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.Location = new Point(42, 53);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(256, 27);
            textBox2.TabIndex = 11;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.Info;
            button4.Location = new Point(123, 86);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 12;
            button4.Text = "Kaydet";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(255, 192, 192);
            label1.Location = new Point(654, 370);
            label1.Name = "label1";
            label1.Size = new Size(88, 20);
            label1.TabIndex = 18;
            label1.Text = "Öğrenci Adı";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(255, 192, 192);
            label2.Location = new Point(654, 435);
            label2.Name = "label2";
            label2.Size = new Size(110, 20);
            label2.TabIndex = 19;
            label2.Text = "Öğrenci Soyadı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(255, 192, 192);
            label3.Location = new Point(654, 502);
            label3.Name = "label3";
            label3.Size = new Size(128, 20);
            label3.TabIndex = 20;
            label3.Text = "Öğrenci Numarası";
            // 
            // listView1
            // 
            listView1.Location = new Point(395, 74);
            listView1.Name = "listView1";
            listView1.Size = new Size(477, 232);
            listView1.TabIndex = 22;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.IndianRed;
            buttonDelete.Location = new Point(471, 328);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(121, 29);
            buttonDelete.TabIndex = 23;
            buttonDelete.Text = "Öğrenci Sil";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click_1;
            // 
            // button5
            // 
            button5.BackColor = Color.IndianRed;
            button5.Location = new Point(1000, 328);
            button5.Name = "button5";
            button5.Size = new Size(117, 29);
            button5.TabIndex = 25;
            button5.Text = "Öğretmen Sil";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // listView2
            // 
            listView2.Location = new Point(908, 74);
            listView2.Name = "listView2";
            listView2.Size = new Size(477, 231);
            listView2.TabIndex = 24;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.SelectedIndexChanged += listView2_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Info;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(554, 31);
            label4.Name = "label4";
            label4.Size = new Size(157, 31);
            label4.TabIndex = 26;
            label4.Text = "   Öğrenciler   ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Info;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(1067, 31);
            label5.Name = "label5";
            label5.Size = new Size(204, 31);
            label5.TabIndex = 27;
            label5.Text = "     Öğretmenler     ";
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(192, 255, 192);
            button6.Location = new Point(648, 328);
            button6.Name = "button6";
            button6.Size = new Size(131, 29);
            button6.TabIndex = 28;
            button6.Text = "Öğrenci Ekle";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.White;
            textBox3.Location = new Point(654, 395);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 29;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(654, 458);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 30;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(654, 536);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(125, 27);
            textBox5.TabIndex = 31;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(654, 615);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 27);
            textBox6.TabIndex = 33;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(255, 192, 192);
            label6.Location = new Point(654, 581);
            label6.Name = "label6";
            label6.Size = new Size(105, 20);
            label6.TabIndex = 32;
            label6.Text = "Öğrenci Şifresi";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(1186, 615);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(125, 27);
            textBox7.TabIndex = 42;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(255, 192, 192);
            label7.Location = new Point(1186, 581);
            label7.Name = "label7";
            label7.Size = new Size(120, 20);
            label7.TabIndex = 41;
            label7.Text = "Öğretmen Şifresi";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(1186, 536);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(125, 27);
            textBox8.TabIndex = 40;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(1186, 458);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(125, 27);
            textBox9.TabIndex = 39;
            textBox9.TextChanged += textBox9_TextChanged;
            // 
            // textBox10
            // 
            textBox10.Location = new Point(1186, 395);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(125, 27);
            textBox10.TabIndex = 38;
            textBox10.TextChanged += textBox10_TextChanged;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(192, 255, 192);
            button7.Location = new Point(1180, 328);
            button7.Name = "button7";
            button7.Size = new Size(131, 29);
            button7.TabIndex = 37;
            button7.Text = "Öğretmen Ekle";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(255, 192, 192);
            label8.Location = new Point(1186, 502);
            label8.Name = "label8";
            label8.Size = new Size(103, 20);
            label8.TabIndex = 36;
            label8.Text = "Sicil Numarası";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(255, 192, 192);
            label9.Location = new Point(1186, 435);
            label9.Name = "label9";
            label9.Size = new Size(125, 20);
            label9.TabIndex = 35;
            label9.Text = "Öğretmen Soyadı";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(255, 192, 192);
            label10.Location = new Point(1186, 370);
            label10.Name = "label10";
            label10.Size = new Size(103, 20);
            label10.TabIndex = 34;
            label10.Text = "Öğretmen Adı";
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(255, 192, 128);
            button8.Location = new Point(12, 328);
            button8.Name = "button8";
            button8.Size = new Size(364, 29);
            button8.TabIndex = 44;
            button8.Text = "Ders Talepleri";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click_1;
            // 
            // listView3
            // 
            listView3.Location = new Point(12, 379);
            listView3.Name = "listView3";
            listView3.Size = new Size(364, 259);
            listView3.TabIndex = 45;
            listView3.UseCompatibleStateImageBehavior = false;
            listView3.SelectedIndexChanged += listView3_SelectedIndexChanged;
            // 
            // Form12
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1399, 650);
            Controls.Add(listView3);
            Controls.Add(button8);
            Controls.Add(textBox7);
            Controls.Add(label7);
            Controls.Add(textBox8);
            Controls.Add(textBox9);
            Controls.Add(textBox10);
            Controls.Add(button7);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(textBox6);
            Controls.Add(label6);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(button6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button5);
            Controls.Add(listView2);
            Controls.Add(buttonDelete);
            Controls.Add(listView1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(panel2);
            Controls.Add(button1);
            Controls.Add(panel1);
            Name = "Form12";
            Text = "Form12";
            Load += Form12_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private TextBox textBox1;
        private Panel panel1;
        private Button button1;
        private Button button3;
        private Panel panel2;
        private TextBox textBox2;
        private Button button4;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView listView1;
        private Button buttonDelete;
        private Button button5;
        private ListView listView2;
        private Label label4;
        private Label label5;
        private Button button6;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label6;
        private TextBox textBox7;
        private Label label7;
        private TextBox textBox8;
        private TextBox textBox9;
        private TextBox textBox10;
        private Button button7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button button8;
        private ListView listView3;
    }
}