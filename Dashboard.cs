using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GOS_FxApps
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadchartLS();
            LoadchartRB();
        }

        private void LoadchartLS()
        {
            // Bersihkan data lama (supaya tidak dobel kalau reload)
            chartLowestStock.Series[0].Points.Clear();

            // Tambahkan data
            chartLowestStock.Series[0].Points.AddXY("CO2 Welding Wire 400 kg", 50);
            chartLowestStock.Series[0].Points.AddXY("CO2 Welding Wire 15 kg / 20 kg", 30);
            chartLowestStock.Series[0].Points.AddXY("Aluminium Weld. Wire 2.4 mm 5 kg", 50);
            chartLowestStock.Series[0].Points.AddXY("Cutting Blade SGLB 4570 X 41 X 1.1 X 2/3 TPI", 30);
            chartLowestStock.Series[0].Points.AddXY("Cutting Blade SGLB 6000 X 41 X 1.1 X 2/3 TPI", 30);

            // Styling
            chartLowestStock.Series[0].Points[0].Color = Color.Lime;
            chartLowestStock.Series[0].Points[1].Color = Color.Aqua;
            chartLowestStock.Series[0].Points[2].Color = Color.Orange;
            chartLowestStock.Series[0].Points[3].Color = Color.Violet;
            chartLowestStock.Series[0].Points[4].Color = Color.Yellow;
        }
        
        private void LoadchartRB()
        {
            
            // Buat series untuk data kolom
            chartRoundbar.Series.Clear();
            Series series = new Series();
            series.Name = "DataSeries";
            series.ChartType = SeriesChartType.Column;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Gainsboro;

            

            // Tambahkan data ke series
            series.Points.AddXY("Roundbar Stock", 50);
            series.Points.AddXY("Rounbar Sawing E1", 30);
            series.Points.AddXY("Rounbar Sawing E2", 50);
            series.Points.AddXY("Rounbar Lathe E1", 30);
            series.Points.AddXY("Rounbar Lathe E2", 30);
            series.Points.AddXY("Welding Pieces Sawing E1", 130);
            series.Points.AddXY("Welding Pieces Sawing E2", 250);
            series.Points.AddXY("Welding Pieces Lathe E1", 330);
            series.Points.AddXY("Welding Pieces Lathe E2", 330);

            //Styling
            series.Points[0].Color = Color.Red;
            series.Points[1].Color = Color.Lime;
            series.Points[2].Color = Color.Lime;
            series.Points[3].Color = Color.Aqua;
            series.Points[4].Color = Color.Aqua;
            series.Points[5].Color = Color.Orange;
            series.Points[6].Color = Color.Orange;
            series.Points[7].Color = Color.Violet;
            series.Points[8].Color = Color.Violet;

            // Tambahkan series ke chart
            chartRoundbar.Series.Add(series);

            chartRoundbar.ChartAreas[0].AxisX.Interval = 1;   
        }

        private void LoadchartUM()
        {

            // Buat series untuk data kolom
            chartUssageMaterial.Series.Clear();
            Series series = new Series();
            series.Name = "DataSeries";
            series.ChartType = SeriesChartType.Column;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Gainsboro;



            // Tambahkan data ke series
            series.Points.AddXY("Roundbar Stock", 50);
            series.Points.AddXY("Rounbar Sawing E1", 30);
            series.Points.AddXY("Rounbar Sawing E2", 50);
            series.Points.AddXY("Rounbar Lathe E1", 30);
            series.Points.AddXY("Rounbar Lathe E2", 30);
            series.Points.AddXY("Welding Pieces Sawing E1", 130);
            series.Points.AddXY("Welding Pieces Sawing E2", 250);
            series.Points.AddXY("Welding Pieces Lathe E1", 330);
            series.Points.AddXY("Welding Pieces Lathe E2", 330);

            //Styling
            series.Points[0].Color = Color.Red;
            series.Points[1].Color = Color.Lime;
            series.Points[2].Color = Color.Lime;
            series.Points[3].Color = Color.Aqua;
            series.Points[4].Color = Color.Aqua;
            series.Points[5].Color = Color.Orange;
            series.Points[6].Color = Color.Orange;
            series.Points[7].Color = Color.Violet;
            series.Points[8].Color = Color.Violet;

            // Tambahkan series ke chart
            chartUssageMaterial.Series.Add(series);

            chartUssageMaterial.ChartAreas[0].AxisX.Interval = 1;
        }

        private void LoadchartSM()
        {

            // Buat series untuk data kolom
            chartStockMaterial.Series.Clear();
            Series series = new Series();
            series.Name = "DataSeries";
            series.ChartType = SeriesChartType.Column;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Gainsboro;



            // Tambahkan data ke series
            series.Points.AddXY("Roundbar Stock", 50);
            series.Points.AddXY("Rounbar Sawing E1", 30);
            series.Points.AddXY("Rounbar Sawing E2", 50);
            series.Points.AddXY("Rounbar Lathe E1", 30);
            series.Points.AddXY("Rounbar Lathe E2", 30);
            series.Points.AddXY("Welding Pieces Sawing E1", 130);
            series.Points.AddXY("Welding Pieces Sawing E2", 250);
            series.Points.AddXY("Welding Pieces Lathe E1", 330);
            series.Points.AddXY("Welding Pieces Lathe E2", 330);

            //Styling
            series.Points[0].Color = Color.Red;
            series.Points[1].Color = Color.Lime;
            series.Points[2].Color = Color.Lime;
            series.Points[3].Color = Color.Aqua;
            series.Points[4].Color = Color.Aqua;
            series.Points[5].Color = Color.Orange;
            series.Points[6].Color = Color.Orange;
            series.Points[7].Color = Color.Violet;
            series.Points[8].Color = Color.Violet;

            // Tambahkan series ke chart
            chartStockMaterial.Series.Add(series);

            chartStockMaterial.ChartAreas[0].AxisX.Interval = 1;
        }
    }
}
