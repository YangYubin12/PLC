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

namespace _20240321
{
    public partial class Form1 : Form
    {
        //전역변수
        TcpClient tc = new TcpClient();
        ModbusIpMaster mim;

        Label[] mylabels = new Label[16];

        public Form1()
        {
            InitializeComponent();
            mylabels[0] = label1; // 공급후진감지
            mylabels[1] = label4; // 공급전진감지
            mylabels[2] = label3; // 매거진자재감지
            mylabels[3] = label2; // 스토퍼상승감지
            mylabels[4] = label5; // 스토퍼하강감지
            mylabels[5] = label6; // 포토센서_1
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //접속 버튼 클릭 시
            tc.Connect(textBox1.Text, 502);
            mim = ModbusIpMaster.CreateIp(tc);


            //통신 관련 기본 설정
            mim.Transport.WriteTimeout = 100;
            mim.Transport.ReadTimeout = 100;
            mim.Transport.Retries = 0;

            if (tc.Connected)
            {
                timer1.Start();
                MessageBox.Show("접속 완료");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //M00100 가상 접점에 HIGH(true) 신호 전송
            try
            {
                mim.WriteSingleCoil(0, false);
                mim.WriteSingleCoil(0, true);
                mim.WriteSingleCoil(0, false);
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                mim.WriteSingleCoil(1, false);
                mim.WriteSingleCoil(1, true);
                mim.WriteSingleCoil(1, false);
            }
            catch
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //0.1초 간격
            try
            {
                //M2 read coil 정보 읽어옴
                bool[] data = mim.ReadInputs(3, 6);
                //data length == 6

                //읽기 레지스터 0번지에서 레지스터 1개를 읽어온다.
                ushort[] data2 = mim.ReadInputRegisters(0, 1);
                //data2[0]
                textBox2.Text = data2[0].ToString();

                //쓰기 레지스터 0번지에서 1워드 읽어오기
                ushort[] data3 = mim.ReadHoldingRegisters(0, 1);
                textBox3.Text = data3[0].ToString();


                for (int i = 0; i < 6; i++)
                {
                    if (data[i])
                    {
                        //on
                        mylabels[i].BackColor = Color.Green;
                    }
                    else
                    {
                        //off
                        mylabels[i].BackColor = Color.Red;
                    }
                }

            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //쓰기 레지스터 0번지에 설정값을 쓴다
                ushort num = ushort.Parse(textBox4.Text);
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
                //쓰기 코일 M00102(2번지)에 사각펄스를 전송
                mim.WriteSingleCoil(2, false);
                mim.WriteSingleCoil(2, true);
                mim.WriteSingleCoil(2, false);

            }
            catch
            {

            }

        }
    }
}
