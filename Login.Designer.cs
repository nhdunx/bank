namespace KLFixLag
{
    // Token: 0x02000002 RID: 2
    public partial class Login : global::System.Windows.Forms.Form
    {
        // Token: 0x06000011 RID: 17 RVA: 0x0000215C File Offset: 0x0000035C
        protected override void Dispose(bool disposing)
        {
            bool flag = disposing && this.components != null;
            if (flag)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002194 File Offset: 0x00000394
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            label1 = new Label();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            label4 = new Label();
            label6 = new Label();
            pictureBox2 = new PictureBox();
            guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(components);
            label2 = new Label();
            guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2DragControl3 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2DragControl4 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2DragControl5 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2DragControl6 = new Guna.UI2.WinForms.Guna2DragControl(components);
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(-1, 209);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 23);
            label1.TabIndex = 22;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.9D;
            guna2DragControl1.DragEndTransparencyValue = 0.9D;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 12;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(13, 511);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(80, 29);
            label4.TabIndex = 54;
            label4.Text = "Verison :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(86, 511);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(39, 25);
            label6.TabIndex = 56;
            label6.Text = "3.0";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Location = new Point(29, 26);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(395, 329);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 59;
            pictureBox2.TabStop = false;
            // 
            // guna2Elipse2
            // 
            guna2Elipse2.BorderRadius = 20;
            guna2Elipse2.TargetControl = this;
            // 
            // guna2Elipse3
            // 
            guna2Elipse3.BorderRadius = 12;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(97, 461);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(251, 29);
            label2.TabIndex = 64;
            label2.Text = "Giá bán vĩnh viễn 99.000 VNĐ";
            // 
            // guna2DragControl2
            // 
            guna2DragControl2.DockIndicatorTransparencyValue = 0.9D;
            guna2DragControl2.DragEndTransparencyValue = 0.9D;
            guna2DragControl2.UseTransparentDrag = true;
            // 
            // guna2DragControl3
            // 
            guna2DragControl3.DockIndicatorTransparencyValue = 0.9D;
            guna2DragControl3.DragEndTransparencyValue = 0.9D;
            guna2DragControl3.UseTransparentDrag = true;
            // 
            // guna2DragControl4
            // 
            guna2DragControl4.DockIndicatorTransparencyValue = 0.9D;
            guna2DragControl4.DragEndTransparencyValue = 0.9D;
            guna2DragControl4.UseTransparentDrag = true;
            // 
            // guna2DragControl5
            // 
            guna2DragControl5.DockIndicatorTransparencyValue = 0.9D;
            guna2DragControl5.DragEndTransparencyValue = 0.9D;
            guna2DragControl5.UseTransparentDrag = true;
            // 
            // guna2DragControl6
            // 
            guna2DragControl6.DockIndicatorTransparencyValue = 0.9D;
            guna2DragControl6.DragEndTransparencyValue = 0.9D;
            guna2DragControl6.UseTransparentDrag = true;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.Disable;
            BackColor = Color.FromArgb(8, 7, 11);
            ClientSize = new Size(474, 417);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label1);
            ForeColor = Color.DimGray;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "Login";
            Opacity = 0.93D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Loader";
            TransparencyKey = Color.Maroon;
            Load += Login_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // Token: 0x04000001 RID: 1
        private global::System.ComponentModel.IContainer components = null;


        // Token: 0x0400000A RID: 10
        private global::System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Label label6;
        private Label label4;
        private PictureBox pictureBox2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private Label label2;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl3;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl4;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl5;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl6;
        private System.Windows.Forms.Timer timer1;
    }
}
