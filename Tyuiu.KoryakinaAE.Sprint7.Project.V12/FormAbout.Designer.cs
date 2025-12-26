namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    partial class FormAbout
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxPhoto_KAE;
        private System.Windows.Forms.Label labelInfo_KAE;
        private System.Windows.Forms.Button buttonClose_KAE;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            

            tableLayoutPanelMain = new();
            pictureBoxPhoto_KAE = new();
            labelInfo_KAE = new();
            buttonClose_KAE = new();

            SuspendLayout();

            // ===== TABLE LAYOUT =====
            tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.RowCount = 2;

            tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200));
            tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            tableLayoutPanelMain.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));

            // ===== PHOTO =====
            pictureBoxPhoto_KAE.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxPhoto_KAE.Margin = new System.Windows.Forms.Padding(15);
            pictureBoxPhoto_KAE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxPhoto_KAE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ===== TEXT =====
            labelInfo_KAE.Dock = System.Windows.Forms.DockStyle.Fill;
            labelInfo_KAE.Font = new System.Drawing.Font("Segoe UI", 10F);
            labelInfo_KAE.Padding = new System.Windows.Forms.Padding(10);
            labelInfo_KAE.AutoSize = false;
            labelInfo_KAE.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            labelInfo_KAE.Text =
    @"Персональные электронно-вычислительные машины — Вариант 12

Автор:
Корякина Алина Эльдаяговна

Направление:
Информационные системы и технологии
в геологии и нефтегазовой отрасли

Учебное заведение:
Тюменский индустриальный университет

Год:
2025";

            // ===== BUTTON =====
            buttonClose_KAE.Text = "Закрыть";
            buttonClose_KAE.Dock = System.Windows.Forms.DockStyle.Right;
            buttonClose_KAE.Width = 120;
            buttonClose_KAE.Margin = new System.Windows.Forms.Padding(10);
            buttonClose_KAE.Click += (s, e) => Close();

            // ===== ADD CONTROLS =====
            tableLayoutPanelMain.Controls.Add(pictureBoxPhoto_KAE, 0, 0);
            tableLayoutPanelMain.Controls.Add(labelInfo_KAE, 1, 0);
            tableLayoutPanelMain.Controls.Add(buttonClose_KAE, 1, 1);

            Controls.Add(tableLayoutPanelMain);

            // ===== FORM =====
            Text = "О программе";
            ClientSize = new System.Drawing.Size(700, 350);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            MinimumSize = new System.Drawing.Size(600, 300);

            ResumeLayout(false);
        }
    }
}