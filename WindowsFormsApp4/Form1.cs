using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using parserDecimal.Parser;
using Excel = Microsoft.Office.Interop.Excel;


namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private decimal result;
        private decimal tol;
        GoldenSearch GS = new GoldenSearch();   //Модуль для нахождения оптимального решения методом золотого сечения
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            clear();
            int maxt = int.Parse(maxtime.Text, System.Globalization.NumberStyles.Float);
            tol = Decimal.Parse(tolerance.Text.Trim(), System.Globalization.NumberStyles.Float);
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            int var = 0;
            if (Min.Checked)
            {
                var = 0;
            }
            else
            {
                var = 1;
            }
            try
            {
                    result = GS.Search(function, A,B, tol, max_k, progressBar1, maxt, maxtime, b_a, var);
                    func_value.Text = Convert.ToString(GS.F1);
                    func_value2.Text = Convert.ToString(GS.F2);
                    k.Text = Convert.ToString(GS.iteration);
                    x.Text = result.ToString();
                    time.Text = Convert.ToString(GS.time);
                    otvet.Text = (GS.otv);
            }
            catch (info.lundin.math.ParserException)
            {
                MessageBox.Show("Ошибка!Возможно вы ввели неправильные данные!");
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            x.Text = "";
            func_value.Text = "";
            func_value2.Text = "";
            k.Text = "";
            time.Text = "";
            b_a.Text = "";
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            otvet.Text = "Ответ";
            GS.clear();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Проверка ввода функции

        private void function_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (e.Handled = c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || c >= 'ё' && c <= 'Ё')
            {
                e.Handled = true;
            }
        }


        //Проверка погрешности
        private void tolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == 'E') && (tolerance.Text.IndexOf("E") == -1) && (tolerance.Text.IndexOf("e") == -1) && (tolerance.Text.Length != 0))
              && !((e.KeyChar == 'e') && (tolerance.Text.IndexOf("e") == -1) && (tolerance.Text.IndexOf("E") == -1) && (tolerance.Text.Length != 0)) && ((e.KeyChar != '-') && (tolerance.Text.IndexOf("-") == -1)))
            {
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            }
        }
        //Проверка начального значения
        private void x0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 44)
                e.Handled = true;
        }
        
        //Проверка максимального количества итераций
        private void max_k_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
        //Проверка максимального количества времени
        private void maxtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void Excel_Click(object sender, EventArgs e)
        {

            Excel.Application xla;
            xla = new Excel.Application();
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook wb = xla.Workbooks.Add(@"C:\Users\user\source\repos\Golden Search\WindowsFormsApp4\bin\Debug\LookingForOneOptPoint.xls");
            Excel.Worksheet ws = (Excel.Worksheet)xla.ActiveSheet;
            xla.Visible = true;
            string func;
            func = function.Text.Replace("x", "D4");
            ws.Cells[4, 9] = A.Text;
            ws.Cells[4, 10] = B.Text;
            ws.Cells[4, 5] = "=" + func;

        }

        private void func_value2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
