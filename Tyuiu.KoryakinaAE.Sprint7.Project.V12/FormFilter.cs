using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    public partial class FormFilter : Form
    {
        private readonly DataTable table_KAE;
        public string ResultFilter_KAE { get; private set; } = "";

        public FormFilter(DataTable table)
        {
            InitializeComponent();
            table_KAE = table;
            BuildFilterUI_KAE();
        }

        // ================= BUILD UI =================

        private void BuildFilterUI_KAE()
        {
            panelFilters_KAE.Controls.Clear();
            int y = 10;

            foreach (DataColumn col in table_KAE.Columns)
            {
                Label labelColumnName_KAE = new Label
                {
                    Text = col.ColumnName,
                    Left = 10,
                    Top = y + 4,
                    Width = 160
                };

                if (IsNumericColumn_KAE(col))
                {
                    TextBox textBoxFrom_KAE = new TextBox
                    {
                        Left = 180,
                        Top = y,
                        Width = 80,
                        Tag = col
                    };

                    TextBox textBoxTo_KAE = new TextBox
                    {
                        Left = 270,
                        Top = y,
                        Width = 80,
                        Tag = col
                    };

                    panelFilters_KAE.Controls.Add(labelColumnName_KAE);
                    panelFilters_KAE.Controls.Add(textBoxFrom_KAE);
                    panelFilters_KAE.Controls.Add(textBoxTo_KAE);
                }
                else
                {
                    TextBox textBoxValue_KAE = new TextBox
                    {
                        Left = 180,
                        Top = y,
                        Width = 170,
                        Tag = col
                    };

                    panelFilters_KAE.Controls.Add(labelColumnName_KAE);
                    panelFilters_KAE.Controls.Add(textBoxValue_KAE);
                }

                y += 30;
            }
        }

        // ================= APPLY =================

        private void buttonApply_KAE_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new();

            foreach (Control control in panelFilters_KAE.Controls)
            {
                if (control is TextBox tb && tb.Tag is DataColumn col)
                {
                    string value = tb.Text.Trim();
                    if (string.IsNullOrEmpty(value))
                        continue;

                    if (IsNumericColumn_KAE(col))
                    {
                        if (tb.Left < 220 && double.TryParse(value, out double from))
                        {
                            Append_KAE(sb, $"[{col.ColumnName}] >= {from}");
                        }
                        else if (tb.Left > 220 && double.TryParse(value, out double to))
                        {
                            Append_KAE(sb, $"[{col.ColumnName}] <= {to}");
                        }
                    }
                    else
                    {
                        value = value.Replace("'", "''");
                        Append_KAE(sb, $"[{col.ColumnName}] LIKE '%{value}%'");
                    }
                }
            }

            ResultFilter_KAE = sb.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        // ================= RESET =================

        private void buttonReset_KAE_Click(object sender, EventArgs e)
        {
            ResultFilter_KAE = "";
            DialogResult = DialogResult.OK;
            Close();
        }

        // ================= HELPERS =================

        private static void Append_KAE(StringBuilder sb, string expr)
        {
            if (sb.Length > 0)
                sb.Append(" AND ");
            sb.Append(expr);
        }

        private static bool IsNumericColumn_KAE(DataColumn col)
        {
            foreach (DataRow row in col.Table.Rows)
            {
                if (row[col] == DBNull.Value)
                    continue;

                if (!double.TryParse(row[col].ToString(), out _))
                    return false;
            }
            return true;
        }
    }
}
