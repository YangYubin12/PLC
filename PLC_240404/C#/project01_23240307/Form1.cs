using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device; //네임스페이스 추가

namespace project01_23240307
{
    public partial class Form1 : Form
    {
        //C# 전역변수 위치
        TcpClient tc = new TcpClient();
        ModbusIpMaster nim;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //장비와 TCP 연결
            tc.Connect(textBox1.Text, 502); //502 고정
            nim = ModbusIpMaster.CreateIp(tc);
            nim.Transport.ReadTimeout = 100;
            nim.Transport.WriteTimeout = 100;
            nim.Transport.Retries = 0;

            if (tc.Connected)
            {
                MessageBox.Show("접속");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //실린더 전진
            try
            {
                nim.WriteSingleCoil(0, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //실린더 후진
            try
            {
                nim.WriteSingleCoil(0, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bool[] data = nim.ReadInputs(0, 1);
                if (data[0])
                {
                    //실린더가 전진해있는 상태
                    label1.Text = "결과: 전진";
                }
                else
                {
                    //실린더가 후진해있는 상태
                    label1.Text = "결과: 후진";
                }
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(1, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(1, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                bool[] data = nim.ReadInputs(1, 1);
                if (data[0])
                {
                    label2.Text = "결과: 작동 중";
                }
                else
                {
                    label2.Text = "결과: 멈춤";
                }
            }
            catch
            {
                label1.Text = "실패";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
