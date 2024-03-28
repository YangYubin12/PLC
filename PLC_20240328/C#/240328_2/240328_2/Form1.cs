using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device; //네임스페이스 추가

namespace _240328_2
{
    public partial class Form1 : Form
    {
        TcpClient tc = new TcpClient(); //C#
        ModbusIpMaster mim; //라이브러리

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //접속 버튼 클릭
            tc.Connect(textBox1.Text, 502);
            mim = ModbusIpMaster.CreateIp(tc);

            mim.Transport.WriteTimeout = 100;
            mim.Transport.ReadTimeout = 100;
            mim.Transport.Retries = 0;

            if (tc.Connected)
            {
                timer1.Start();
                MessageBox.Show("연결");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //공정 시작
            try
            {
                //M00100에 사각 펄스 전송
                //쓰기코일 0번지
                mim.WriteSingleCoil(0, false);
                mim.WriteSingleCoil(0, true);
                mim.WriteSingleCoil(0, false);
            }
            catch
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //0.1초 마다 작동
            //읽기 코일에 0번지부터 4개를 읽어옴
            try
            {
                bool[] data = mim.ReadInputs(0, 4);
                if (data[0])
                {
                    //P0005 (센서명: M2_S1)
                    label1.BackColor = Color.Green;
                }
                else
                {
                    label1.BackColor = Color.Red;
                }

                if (data[1])
                {
                    label2.BackColor = Color.Green;
                }
                else
                {
                    label2.BackColor = Color.Red;
                }

                if (data[2])
                {
                    label3.BackColor = Color.Green;
                }
                else
                {
                    label3.BackColor = Color.Red;
                }

                if (data[3])
                {
                    label4.BackColor = Color.Green;
                }
                else
                {
                    label4.BackColor = Color.Red;
                }

                //읽기 레지스터에서 2개 읽음
                ushort[] data2 = mim.ReadInputRegisters(0, 2);
                textBox2.Text = data2[0].ToString();
                textBox3.Text = data2[1].ToString();

                //쓰기 레지스터에서 2개 읽음
                ushort[] data3 = mim.ReadHoldingRegisters(0, 2);
                textBox7.Text = data3[0].ToString();
                textBox6.Text = data3[1].ToString();
            }
            catch
            {

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //M00100에 사각 펄스 전송
                //쓰기코일 0번지
                mim.WriteSingleCoil(1, false);
                mim.WriteSingleCoil(1, true);
                mim.WriteSingleCoil(1, false);
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //M00100에 사각 펄스 전송
                //쓰기코일 0번지
                mim.WriteSingleCoil(2, false);
                mim.WriteSingleCoil(2, true);
                mim.WriteSingleCoil(2, false);
            }
            catch
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //쓰기 레지스터 0번지에 설정값을 쓴다
                ushort num = ushort.Parse(textBox5.Text);
                mim.WriteSingleRegister(0, num);
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //쓰기 레지스터 0번지에 설정값을 쓴다
                ushort num = ushort.Parse(textBox4.Text);
                mim.WriteSingleRegister(1, num);
            }
            catch
            {

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
