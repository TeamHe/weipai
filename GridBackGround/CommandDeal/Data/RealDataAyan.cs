using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;
using ResModel;
using System.ComponentModel;
using System.Reflection;
using Tools;
using ResModel.CollectData;
using DB_Operation.RealData;

namespace GridBackGround.CommandDeal.Data
{
    class RealDataAyan
    {
        #region Public Variable
        /// <summary>
        /// 装置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 装置ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 装置类型
        /// </summary>
        public ICMP Type { get; set; }
        #endregion
        
        /// <summary>
        /// 数据解析信息
        /// </summary>
        private string AyanMsg = "";  

        #region Construction
        public RealDataAyan()
        { 
        
        }
        /// <summary>
        /// 实时数据内容解析
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="datatype"></param>
        /// <param name="data"></param>
        public RealDataAyan(IPowerPole pole, int datatype,byte[] data)
            :this(data,(ICMP)datatype,pole.Name ,pole.CMD_ID)
        {
            DisAyanMsg();
        }
        /// <summary>
        /// 实时数据内容解析
        /// </summary>
        /// <param name="cmd_name"></param>
        /// <param name="cmd_id"></param>
        /// <param name="datatype"></param>
        /// <param name="data"></param>
        public RealDataAyan(string cmd_name, string cmd_id, int datatype,byte[] data)
            :this(data,(ICMP)datatype,cmd_name,cmd_id)
        {
            DisAyanMsg();
        }

        /// <summary>
        /// 实时数据内容解析
        /// </summary>
        /// <param name="data"></param>
        public RealDataAyan(byte[] data,ICMP type,string cmdName,string cmdID)
        {
            try
            {
                this.Type = type;
                this.Name = cmdName;
                this.ID = cmdID;

                var ayanData = GetCollectData(data);                //数据解析
                CollectData collectedData = (CollectData)ayanData;  //数据类型强制转换
                this.AyanMsg += collectedData.AyanMsg;              //获取数据解析结果
                if (collectedData.Maintime.Year == 2106)            //验证数据采集时间
                    throw new Exception("装置时间异常，暂不保存数据");
                DataSave(ayanData);                                 //数据存储
            }
            catch (Exception ex)
            {
                this.AyanMsg += ex.Message;
            }
        }
        #endregion

        #region Private Method     
        
        /// <summary>
        /// 获取采集数据的值
        /// </summary>
        /// <param name="data"></param>
        private object GetCollectData(byte[] data)
        {
            //根据装置类型不同，按照不同的方式解析
            switch ((ICMP)Type)
            { 
                //微气象
                case ICMP.Weather:
                    return new Weather(this.Name, this.ID, data);
                case ICMP.Inclination:
                    return new Inclinations(this.Name,this.ID,data);
                case ICMP.Ice:
                    return new Ice(this.Name,this.ID,data);
            }
            throw new Exception("不支持的数据解析类型");
        }
        /// <summary>
        /// 保存实时采集数据
        /// </summary>
        private void DataSave(object data)
        {
            if (data == null)
                throw new ArgumentNullException("未找到待存储的数据");
            try
            {
                IRealData_OP op =  Real_Data_Op.Creat(Type);
                var sql = op.DataSave(data);                //数据保存
                if (sql == ErrorCode.NoError)                   //数据保存结果验证
                    this.AyanMsg += "数据保存成功";
                else
                    this.AyanMsg += Tools.EnumUtil.GetDescription(sql); //添加数据保存信息
            }
            catch (Exception ex)
            {
                this.AyanMsg += "数据保存失败,失败原因：" + ex.Message; 
            }
        }

        /// <summary>
        /// 显示数据解析内容
        /// </summary>
        private void DisAyanMsg()
        {
            PacketAnaLysis.DisPacket.NewRecord(
                        new PacketAnaLysis.DataInfo(
                            PacketAnaLysis.DataRecSendState.rec,
                            this.Name,
                            this.ID,
                            Type.GetDescription() + "数据报",
                            AyanMsg));
        }
        #endregion
    }
    
}
