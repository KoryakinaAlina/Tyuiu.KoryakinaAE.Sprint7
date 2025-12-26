using System.Windows.Forms;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    partial class FormFilter
    {
        private Panel panelFilters_KAE;
        private Button buttonApply_KAE;
        private Button buttonReset_KAE;

        private void InitializeComponent()
        {
            panelFilters_KAE = new Panel();
            buttonApply_KAE = new Button();
            buttonReset_KAE = new Button();

            SuspendLayout();

            // ===== FORM =====
            Text = "Фильтрация данных персональных ЭВМ";
            ClientSize = new System.Drawing.Size(420, 460);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // ===== PANEL =====
            panelFilters_KAE.Dock = DockStyle.Top;
            panelFilters_KAE.Height = 370;
            panelFilters_KAE.AutoScroll = true;

            // ===== BUTTON APPLY =====
            buttonApply_KAE.Text = "Применить";
            buttonApply_KAE.SetBounds(90, 390, 110, 32);
            buttonApply_KAE.Click += buttonApply_KAE_Click;

            // ===== BUTTON RESET =====
            buttonReset_KAE.Text = "Сбросить";
            buttonReset_KAE.SetBounds(220, 390, 110, 32);
            buttonReset_KAE.Click += buttonReset_KAE_Click;

            // ===== ADD =====
            Controls.Add(panelFilters_KAE);
            Controls.Add(buttonApply_KAE);
            Controls.Add(buttonReset_KAE);

            ResumeLayout(false);
        }
    }
}