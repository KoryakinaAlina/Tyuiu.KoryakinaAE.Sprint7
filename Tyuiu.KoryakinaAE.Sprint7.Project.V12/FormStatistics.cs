using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    public partial class FormStatistics : Form
    {
        private const string PriceColumn = "Цена";

        private readonly DataTable sourceTable_KoryakinaAE;
        private DataTable resultTable_KoryakinaAE = new();

        public FormStatistics(DataTable table)
        {
            InitializeComponent();

            sourceTable_KoryakinaAE = table ?? new DataTable();

            comboBoxMode_KoryakinaAE.Items.AddRange(new[]
            {
                "Общая статистика",
                "По производителям",
                "По моделям",
                "По типу процессора"
            });

            comboBoxMode_KoryakinaAE.SelectedIndex = 0;
            BuildStatistics();
        }

        // ================= BUILD =================
        private void BuildStatistics()
        {
            if (comboBoxMode_KoryakinaAE.SelectedItem is not string mode) return;

            resultTable_KoryakinaAE = mode switch
            {
                "Общая статистика" => BuildTotalStatistics(),
                "По производителям" => BuildGroupedStatistics("Фирма-изготовитель"),
                "По моделям" => BuildGroupedStatistics("Примечание"), // или отдельная колонка с моделью, если есть
                "По типу процессора" => BuildGroupedStatistics("Процессор"),
                _ => new DataTable()
            };

            dataGridViewStats_KoryakinaAE.DataSource = resultTable_KoryakinaAE;
        }

        // ================= TOTAL =================
        private DataTable BuildTotalStatistics()
        {
            DataTable t = new();
            t.Columns.Add("Показатель");
            t.Columns.Add("Значение");

            var prices = GetPriceValues();

            t.Rows.Add("Всего ПК", sourceTable_KoryakinaAE.Rows.Count);
            t.Rows.Add("Средняя цена", prices.Any() ? prices.Average().ToString("N0") : "—");
            t.Rows.Add("Минимальная цена", prices.Any() ? prices.Min().ToString("N0") : "—");
            t.Rows.Add("Максимальная цена", prices.Any() ? prices.Max().ToString("N0") : "—");

            return t;
        }

        // ================= GROUPED =================
        private DataTable BuildGroupedStatistics(string groupColumn)
        {
            DataTable t = new();
            t.Columns.Add(groupColumn);
            t.Columns.Add("Количество");
            t.Columns.Add("Средняя цена");
            t.Columns.Add("Сумма цен");

            if (!sourceTable_KoryakinaAE.Columns.Contains(groupColumn) ||
                !sourceTable_KoryakinaAE.Columns.Contains(PriceColumn))
                return t;

            var groups = sourceTable_KoryakinaAE.AsEnumerable()
                .GroupBy(r => r[groupColumn]?.ToString() ?? "—");

            foreach (var g in groups)
            {
                var prices = g
                    .Select(TryGetPrice)
                    .Where(v => v.HasValue)
                    .Select(v => v!.Value)
                    .ToList();

                t.Rows.Add(
                    g.Key,
                    g.Count(),
                    prices.Any() ? prices.Average().ToString("N0") : "—",
                    prices.Any() ? prices.Sum().ToString("N0") : "—"
                );
            }

            return t;
        }

        // ================= HELPERS =================
        private double? TryGetPrice(DataRow row)
        {
            return double.TryParse(row[PriceColumn]?.ToString(), out var v) ? v : null;
        }

        private List<double> GetPriceValues()
        {
            if (!sourceTable_KoryakinaAE.Columns.Contains(PriceColumn))
                return new();

            return sourceTable_KoryakinaAE.AsEnumerable()
                .Select(TryGetPrice)
                .Where(v => v.HasValue)
                .Select(v => v!.Value)
                .ToList();
        }

        // ================= EVENTS =================
        private void comboBoxMode_KoryakinaAE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildStatistics();
        }

        private void buttonExportCsv_KoryakinaAE_Click(object sender, EventArgs e)
        {
            Export("csv");
        }

        // ================= EXPORT =================
        private void Export(string ext)
        {
            if (resultTable_KoryakinaAE.Rows.Count == 0) return;

            using SaveFileDialog dlg = new()
            {
                Filter = $"{ext.ToUpper()} files (*.{ext})|*.{ext}",
                FileName = $"statistics.{ext}"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            var sb = new StringBuilder();
            sb.AppendLine(string.Join(";", resultTable_KoryakinaAE.Columns
                .Cast<DataColumn>()
                .Select(c => c.ColumnName)));

            foreach (DataRow r in resultTable_KoryakinaAE.Rows)
                sb.AppendLine(string.Join(";", r.ItemArray));

            File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
        }
    }
}