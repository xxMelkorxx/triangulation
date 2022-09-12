
namespace Triangulation
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label_Nodes;
            this.pictureBox_Triangulation = new System.Windows.Forms.PictureBox();
            this.button_Draw = new System.Windows.Forms.Button();
            this.numUpDown_Nodes = new System.Windows.Forms.NumericUpDown();
            label_Nodes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Triangulation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Nodes)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Nodes
            // 
            label_Nodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label_Nodes.AutoSize = true;
            label_Nodes.Location = new System.Drawing.Point(344, 626);
            label_Nodes.Name = "label_Nodes";
            label_Nodes.Size = new System.Drawing.Size(74, 13);
            label_Nodes.TabIndex = 3;
            label_Nodes.Text = "Число узлов:";
            // 
            // pictureBox_Triangulation
            // 
            this.pictureBox_Triangulation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Triangulation.BackColor = System.Drawing.Color.Black;
            this.pictureBox_Triangulation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Triangulation.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_Triangulation.Name = "pictureBox_Triangulation";
            this.pictureBox_Triangulation.Size = new System.Drawing.Size(604, 604);
            this.pictureBox_Triangulation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Triangulation.TabIndex = 0;
            this.pictureBox_Triangulation.TabStop = false;
            // 
            // button_Draw
            // 
            this.button_Draw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Draw.Location = new System.Drawing.Point(491, 622);
            this.button_Draw.Name = "button_Draw";
            this.button_Draw.Size = new System.Drawing.Size(125, 20);
            this.button_Draw.TabIndex = 1;
            this.button_Draw.Text = "Отрисовать";
            this.button_Draw.UseVisualStyleBackColor = true;
            this.button_Draw.Click += new System.EventHandler(this.OnClickButtonDraw);
            // 
            // numUpDown_Nodes
            // 
            this.numUpDown_Nodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDown_Nodes.Location = new System.Drawing.Point(424, 622);
            this.numUpDown_Nodes.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUpDown_Nodes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown_Nodes.Name = "numUpDown_Nodes";
            this.numUpDown_Nodes.Size = new System.Drawing.Size(61, 20);
            this.numUpDown_Nodes.TabIndex = 2;
            this.numUpDown_Nodes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(629, 652);
            this.Controls.Add(label_Nodes);
            this.Controls.Add(this.numUpDown_Nodes);
            this.Controls.Add(this.button_Draw);
            this.Controls.Add(this.pictureBox_Triangulation);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "ННГУ ИТФИ | Триангуляция";
            this.Load += new System.EventHandler(this.OnLoadMainForm);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Triangulation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Nodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Triangulation;
        private System.Windows.Forms.Button button_Draw;
        private System.Windows.Forms.NumericUpDown numUpDown_Nodes;
    }
}

