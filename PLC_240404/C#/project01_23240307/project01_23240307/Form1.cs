using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
// using System.Reflection.Emit;
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

        Label[] mylabel = new Label[16];

        public Form1()
        {
            InitializeComponent();
            mylabel[0] = label1; // 공급후진감지
            mylabel[1] = label4; // 공급전진감지
            mylabel[2] = label3; // 매거진자재감지
            mylabel[3] = label2; // 스토퍼상승감지
            mylabel[4] = label5; // 스토퍼하강감지
            mylabel[5] = label6; // 포토센서_1
            mylabel[6] = label7; // 금속감지센서
            mylabel[7] = label8; // 비금속감지센서
            mylabel[8] = label9; // 포토센서_3
            mylabel[9] = label10; // 하강감지
            mylabel[10] = label11; // 상승감지
            mylabel[11] = label13; // 후진감지
            mylabel[12] = label15; // 전진감지
            mylabel[13] = label14; // 자재감지(M3)
            mylabel[14] = label16; // 자재감지(1층)
            mylabel[15] = label17; // 자재감지(2층)
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
                timer1.Start();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 타이머가 작동 중일 때
            try
            {
                bool[] data = nim.ReadInputs(0, 16);
                for (int i = 0; i < 16; i++)
                {
                    if (data[i])
                    {
                        mylabel[i].BackColor = Color.Green;
                    }
                    else
                    {
                        mylabel[i].BackColor = Color.Red;
                    }
                }
                
                /*
                if (data[0])
                {
                    // 실린더가 전진해있는 상태
                    // label1.Text = "결과: 전진";
                    // 공급후진감지 센서 작동 중
                    // label1.Text = "실린더후진센서: 감지";
                    label1.BackColor = Color.Green;
                }
                else
                {
                    //실린더가 후진해있는 상태
                    // label1.Text = "결과: 후진";
                    // 감지가 안 된 상태
                    // label1.Text = "실린더후진센서: 미감지";
                    label1.BackColor = Color.Red;
                }

                if (data[1])
                {
                    // 실린더 전진센서 감지
                    // label4.Text = "실린더전진센서: 감지";
                    label4.BackColor = Color.Green;
                }
                else
                {
                    // 감지가 안 된 상태
                    // label4.Text = "실린더전진센서: 미감지";
                    label4.BackColor = Color.Red;
                }

                if (data[2])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label3.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label3.BackColor = Color.Red;
                }

                if (data[3])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label2.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label2.BackColor = Color.Red;
                }

                if (data[4])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label5.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label5.BackColor = Color.Red;
                }

                if (data[5])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label6.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label6.BackColor = Color.Red;
                }

                if (data[6])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label7.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label7.BackColor = Color.Red;
                }

                if (data[7])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label8.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label8.BackColor = Color.Red;
                }

                if (data[8])
                {
                    // 자재 감지
                    // label3.Text = "자재 감지: 감지";
                    label9.BackColor = Color.Green;
                }
                else
                {
                    // 자재 감지가 안 된 상태
                    // label3.Text = "자재 감지: 미감지";
                    label9.BackColor = Color.Red;
                }
            */

            }
            catch
            {
                // MessageBox.Show("실패");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(3, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(3, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(2, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(2, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(4, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(4, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(5, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(5, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(6, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(6, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(7, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(7, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(8, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(8, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(9, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(9, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(10, true);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                nim.WriteSingleCoil(10, false);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                bool[] state = {true, true, true};
                nim.WriteMultipleCoils(8, state);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                bool[] state = {false, false, false };
                nim.WriteMultipleCoils(8, state);
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            // PLC에 값 쓰기
            try
            {
                ushort num = ushort.Parse(textBox2.Text);
                nim.WriteSingleRegister(0, num);
            }
            catch
            {
                MessageBox.Show("에러");
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            //레지스터 읽기
            ushort[] data = nim.ReadHoldingRegisters(0, 1);

            label21.Text = data[0].ToString();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                //M100번지(쓰기 코일 0번지) 사각 펄스 전송
                nim.WriteSingleCoil(0, false);
                nim.WriteSingleCoil(0, true);
                nim.WriteSingleCoil(0, false);
            }
            catch
            {

            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //전체 공정 종료
            try
            {
                //M101번지(쓰기 코일 0번지) 사각 펄스 전송
                nim.WriteSingleCoil(1, false);
                nim.WriteSingleCoil(1, true);
                nim.WriteSingleCoil(1, false);
            }
            catch
            {

            }
        }
    }
}
