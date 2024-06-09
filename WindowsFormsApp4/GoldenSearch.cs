using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using parserDecimal.Parser;
using System.Diagnostics;
using info.lundin.math;

namespace WindowsFormsApp4
{
    class GoldenSearch
    {
        public int iteration { get; set; }    //итерации
        public decimal F1 { get; set; }     //функция
        public decimal F2 { get; set; }     //функция
        public string time { get; set; }   //время
        public string otv { get; set; }   //сообщение
       

        private string func;            //Функция
        private int err;    //ошибка
        private decimal X1;
        private decimal X2;
        private decimal r;
        private string tim;
        private int second;
        Stopwatch timer = new Stopwatch();         //таймер

        public decimal Search(ComboBox function, TextBox A, TextBox B,decimal tol, TextBox max_k, ProgressBar pb, int maxt_time, TextBox maxtime, TextBox b_a,int var)
        {
            func = function.Text;
            tim = "";
            decimal  a = Convert.ToDecimal(A.Text.Trim());
            decimal b = Convert.ToDecimal(B.Text.Trim());
            int limit = int.Parse(max_k.Text, System.Globalization.NumberStyles.Float);
            pb.Visible = true;
            r = Convert.ToDecimal((Math.Sqrt(5) - 1) / 2);
            X1 = a + (1 - r) * (b - a);
            F1 = (decimal)f(Convert.ToDecimal(X1));
            X2 = a + r * (b - a);
            F2 = (decimal)f(Convert.ToDecimal(X2));
            iteration = 0;
            if(var==0)
            {
                timer.Start();
                do
                {
                    err = 0;
                    iteration = iteration + 1;
                    pb.Maximum = iteration + 1;
                    pb.Value = iteration;
                    maxtime.Text = Convert.ToString(maxt_time);
                    if (F1 > F2)
                    {
                        a = X1;
                        X1 = X2;
                        F1 = F2;
                        X2 = a + r * (b - a);
                        F2 = (decimal)f(Convert.ToDecimal(X2));
                    }
                    else
                    {
                        b = X2;
                        X2 = X1;
                        F2 = F1;
                        X1 = a + (1 - r) * (b - a);
                        F1 = (decimal)f(Convert.ToDecimal(X1));
                    }
                    tim = timer.ElapsedMilliseconds.ToString();
                    time = tim;
                    if ((Math.Abs(b - a) <= tol))
                    {
                        pb.Value = pb.Maximum;
                        pb.Visible = false;
                        err = 3;
                    }
                    if ((Math.Abs(b - a) > tol) && (iteration >= limit))
                    {
                        pb.Value = pb.Maximum;
                        pb.Visible = false;
                        err = 1;
                    }
                    if (Convert.ToInt32(timer.ElapsedMilliseconds) >= maxt_time)
                    {
                        pb.Value = pb.Maximum;
                        pb.Visible = false;
                        err = 2;
                    }

                } while (err == 0 && Math.Abs(b - a) > tol);
             }
            else if(var==1)
            {
                timer.Start();
                do
                {
                    err = 0;
                    iteration = iteration + 1;
                    pb.Maximum = iteration + 1;
                    pb.Value = iteration;
                    maxtime.Text = Convert.ToString(maxt_time);
                    if (F1 <= F2)
                    {
                        a = X1;
                        X1 = X2;
                        F1 = F2;
                        X2 = a + r * (b - a);
                        F2 = (decimal)f(Convert.ToDecimal(X2));
                    }
                    else
                    {
                        b = X2;
                        X2 = X1;
                        F2 = F1;
                        X1 = a + (1 - r) * (b - a);
                        F1 = (decimal)f(Convert.ToDecimal(X1));
                    }
                    tim = timer.ElapsedMilliseconds.ToString();
                    time = tim;
                    if ((Math.Abs(b - a) <= tol))
                    {
                        pb.Value = pb.Maximum;
                        pb.Visible = false;
                        err = 3;
                    }
                    if ((Math.Abs(b - a) > tol) && (iteration >= limit))
                    {
                        pb.Value = pb.Maximum;
                        pb.Visible = false;
                        err = 1;
                    }
                    if (Convert.ToInt32(timer.ElapsedMilliseconds) >= maxt_time)
                    {
                        pb.Value = pb.Maximum;
                        pb.Visible = false;
                        err = 2;
                    }

                } while (err == 0 && Math.Abs(b - a) > tol);
            }
               
                b_a.Text = string.Format("{0:G1}", Math.Abs(b - a));
                timer.Stop();
                pb.Value = pb.Maximum;
                pb.Visible = false;
            //Итерации превысили максимальное количество и решение не найдено с заданной погрешностью
            if(err==3)
            {
                otv = "Найдено оптимальное решение с точностью  " + string.Format("{0:G1}", Math.Abs(b - a));
            }
            if (err == 1)
            {
                var result = MessageBox.Show("Система совершила " + iteration + " итераций. Не найдено решение с заданной точностью, продолжить поиск?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limit = limit + limit;
                    max_k.Text = (Convert.ToString(limit));
                    Search(function, A,B, tol, max_k, pb, maxt_time, maxtime, b_a,var);
                    pb.Value = pb.Maximum;
                    pb.Visible = false;
                   
                }
                else
                {
                    otv = "Решение не найдено из-за превышения количества итераций";
                }
            }
            //Время превышено и не найдено решение
            if (err == 2)
            {
                var result = MessageBox.Show("Система за " + maxt_time + " миллисекунд не нашла решение c заданной погрешностью, добавить время? ", "Ошибка", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    maxt_time = maxt_time + maxt_time;
                    maxtime.Text = (Convert.ToString(maxt_time));
                    Search(function, A,B,  tol, max_k, pb, maxt_time, maxtime, b_a,var);
                    pb.Value = pb.Maximum;
                    pb.Visible = false;
                }
                else
                {
                    otv = "Решение не найдено из-за превышения времени";
                }
            }
            pb.Visible = false;
            return X1;
        }

        //нахождение значения функции при x
        public double f(decimal x)
        {
            ExpressionParser Parser = new ExpressionParser();
            Parser.Values.Add("x", Convert.ToDouble(x));
            return Parser.Parse(func);
        }

        public void clear()
        {
            iteration = 0;
        }
    }
}
