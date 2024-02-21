using ResModel;
using System;
using System.Collections.Generic;
using System.Text;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 微风振动波形数据报
    /// </summary>
    public class Data_ZD_Form
    {
        private static string CMD_ID;

        public static List<WFZD_Forms> List_WFZD;

        //private static string Component_ID;
       // private static int PacLength = 17 + 1 + 1 + 4 + 2 + 4 + 4;  //被测设备ID，采集单元总数，采集单元序列号，采集时间，动弯应变幅值，弯曲振幅，微分振动频率
        /// <summary>
        /// 微风振动波形解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
        public static void Deal(IPowerPole pole, byte frame_NO, byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string Component_ID;       //被测设备ID
            uint Unit_Sum;              //采集单元总数
            uint Unit_No;               //采集单元序号
            DateTime Time_Stamp;        //采集时间     
            uint SamplePack_Sum;        //数据拆包总数
            uint SamplePack_No;         //数据报包序

            if (List_WFZD == null)
            {
                List_WFZD = new List<WFZD_Forms>();
            }

            string pacMsg = "";
            //被测设备ID
            Component_ID = Encoding.Default.GetString(data, 0, 17);
            pacMsg += "被测设备ID:" + Component_ID + " ";
            //采集单元总数
            Unit_Sum = (uint)data[17];
            pacMsg += "采集单元总数:" + Unit_Sum.ToString() + " ";

            //采集单元序号
            Unit_No = (uint)data[18];
            pacMsg += "采集单元序号:" + Unit_No.ToString() + " ";

            //采集时间
            Time_Stamp = Tools.TimeUtil.BytesToDate(data, 19);
            pacMsg += "采集时间:" + Time_Stamp.ToString() + " ";

            //数据拆包总数
            SamplePack_Sum = (uint)data[23];
            pacMsg += "包数:" + SamplePack_Sum.ToString() + " ";

            //包号
            SamplePack_No = (uint)data[24];
            pacMsg += "包号:" + SamplePack_No.ToString() + " ";

            
            int[] value = new int[(data.Length -25)/2];
            //数据
            pacMsg += "数据:";
            for (int i = 0; i < (data.Length -25)/2; i++)
            {
                
                value[i] = BitConverter.ToUInt16(data, 25 + 2 * i);
                pacMsg += value[i].ToString() +", ";
            }

            //更新保存链表
            int Forms_NO = -1;
            for (int i = 0; i < List_WFZD.Count; i++)
            {
                if (List_WFZD[i].CMD_ID == pole.CMD_ID)      // 找到采集ID
                {
                    Forms_NO = i;
                    break;
                }
               
            }
            if (Forms_NO == -1)              //没找到ID新建一个曲线序列
            {
                var forms = new WFZD_Forms(pole.CMD_ID);
                List_WFZD.Add(forms);
                Forms_NO = List_WFZD.Count - 1;
            } 
            //曲线
            int Form_NO = -1;
           
            var cur_forms = List_WFZD[Forms_NO];
            for (int i = 0; i < cur_forms.Forms.Count; i++)
            {
                var form = cur_forms.Forms[i];
                if (form.Unit_No != Unit_No)        //判断采集单元序号
                    continue;

                if (form.Time != Time_Stamp)
                {
                    form.Data = new float[value.Length * SamplePack_Sum];
                    form.Time = Time_Stamp;
                }
                Form_NO = i;
                break;
            }

            if (Form_NO == -1)
            {
                var form = new Form(Time_Stamp, (int)Unit_No, (int)(value.Length * SamplePack_Sum));
                List_WFZD[Forms_NO].Forms.Add(form);
                Form_NO = List_WFZD[Forms_NO].Forms.Count - 1;
            }

            float[] temp = List_WFZD[Forms_NO].Forms[Form_NO].Data;
            int position = (int)(1024/SamplePack_Sum*((int)SamplePack_No-1));
            for (int i = 0; i < value.Length; i++)
            temp[i + position] = value[i];
            List_WFZD[Forms_NO].Forms[Form_NO].Data = temp;
            
            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "微风振动波形数据",
                    pacMsg));
        }
    }

    public class WFZD_Forms
    {
        public List<Form> Forms { get; set; }
        public string CMD_ID { get; set; }
        
        public int Unit_Num { get; set; }

        public WFZD_Forms(string cmd_ID)
        {
            CMD_ID = cmd_ID;
            Forms = new List<Form>();           
        }
    }
    public class Form
    {
        public DateTime Time { get; set; }
        public int Unit_No { get; set; }
        public float[] Data { get; set; }

        public Form(DateTime time,int no,int length)
        {
            this.Time = time;
            Unit_No = no;
            Data = new float[length];
        }

    }
}
