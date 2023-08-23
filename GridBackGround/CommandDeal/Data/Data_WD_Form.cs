using ResModel;
using System;
using System.Collections.Generic;
using System.Text;
using ResModel.PowerPole;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 舞动波形数据报
    /// </summary>
    public class Data_WD_Form
    {
        public static List<WD_Form> List_WD;
        
        private static string CMD_ID;
        /// <summary>
        /// 舞动波形数据解析
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


            float[] value = new float[(data.Length - 25) / 4];
            pacMsg += "数据: ";
            for (int i = 0; i < (data.Length - 25) / 12; i++)
            {   pacMsg += "(";
                value[i*3] = BitConverter.ToSingle(data, 25 + 12 * i);
                pacMsg += value[i * 3].ToString() + ", ";
                value[i*3+1] = BitConverter.ToSingle(data, 25 + 12 * i +4);
                pacMsg += value[i*3+1].ToString() + ", ";
                value[i*3+2] = BitConverter.ToSingle(data, 25 + 12 * i + 8);
                pacMsg += value[i*3+2].ToString() + "";
                pacMsg += "), ";
            }
            if (List_WD == null)
            {
                List_WD = new List<WD_Form>();
            }
            //更新保存链表
            int Forms_NO = -1;
            for (int i = 0; i < List_WD.Count; i++)
            {
                if (List_WD[i].CMD_ID == pole.CMD_ID)      // 找到采集ID
                {
                    Forms_NO = i;
                    break;
                }
               
            }
            if (Forms_NO == -1)              //没找到ID新建一个曲线序列
            {
                var forms = new WD_Form(pole.CMD_ID);
                List_WD.Add(forms);
                Forms_NO = List_WD.Count - 1;
            }
            //曲线
            int Form_NO = -1;

            var cur_forms = List_WD[Forms_NO];
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
                List_WD[Forms_NO].Forms.Add(form);
                Form_NO = List_WD[Forms_NO].Forms.Count - 1;
            }

            float[] temp = (List_WD[Forms_NO].Forms[Form_NO].Data); ;
            int position = (int)(500 / SamplePack_Sum * ((int)SamplePack_No - 1) * 3);
            int length = value.Length;
            for (int i = 0; i < value.Length; i++)
                temp[i + position] = value[i];
            List_WD[Forms_NO].Forms[Form_NO].Data = temp;
            //显示发送的数据
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "舞动波形数据",
                    pacMsg));

        }        
    }

    public class WD_Form
    {
        public List<Form> Forms { get; set; }
        public string CMD_ID { get; set; }
        public int Unit_Num { get; set; }

        public WD_Form(string cmd_ID)
        {
            CMD_ID = cmd_ID;
            Forms = new List<Form>();   
        }
    }

}
