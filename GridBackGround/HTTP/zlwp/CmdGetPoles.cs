//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using Newtonsoft.Json.Linq;
//using ResModel.EQU;
//using DB_Operation.EQUManage;



//namespace GridBackGround.HTTP.zlwp
//{
//    public class CmdGetEqus
//    {
//        /// <summary>
//        /// http 请求上下文
//        /// </summary>
//        public HttpListenerContext Context { get; set; }

//        /// <summary>
//        /// 请求命令json
//        /// </summary>
//        public JObject JObject { get; set; }


//        public CmdGetEqus()
//        {

//        }

//        public CmdGetEqus(HttpListenerContext context, JObject jObject)
//        {
//            this.Context = context;
//            this.JObject = jObject;
//        }

//        public void Deal()
//        {
//            try
//            {
//                var lineList = DB_Line.List_LineTowerEqu();
//                JObject jObject = new GetEqusRespnse(lineList).ToJson();
//                ReSendMsgService.SendResponse(this.Context, jObject);
//                this.Context.Response.Close();

//            }
//            catch (Exception ex)
//            {
//                Zlwp.SendError(this.Context, "CmdGetLines error. " + ex.Message);
//                Context.Response.Close();
//            }
//        }
//    }
//    public class GetEqusRespnse
//    {
//        public string Root { get { return "GetLinesRespnse"; } }

//        public List<Line> Lines { get; set; }

//        public GetEqusRespnse()
//        {

//        }

//        public GetEqusRespnse(List<Line> lines)
//        {
//            this.Lines = lines;
//        }

//        private JArray Equ_to_json(List<Equ> equs)
//        {
//            JArray jArray = new JArray();
//            if (equs == null) return jArray;

//            foreach (Equ equ in equs)
//            {
//                JObject jObject = new JObject();
//                //jObject.Add("no", equ.ID);
//                jObject.Add("mn", equ.EquID);
//                jObject.Add("name", equ.Name);
//                jArray.Add(jObject);
//            }
//            return jArray;
//        }

//        private JArray towers_to_json(List<Tower> towers)
//        {
//            JArray jArray = new JArray();
//            if (towers == null) return jArray;

//            foreach (Tower tower in towers)
//            {
//                JObject jObject = new JObject();
//                jObject.Add("no", tower.TowerNO);
//                jObject.Add("name", tower.TowerName);
//                JArray jequs = Equ_to_json(tower.EquList);
//                jObject.Add("equs", jequs);
//                jArray.Add(jObject);
//            }
//            return jArray;
//        }

//        private JArray lines_to_json()
//        {
//            JArray array = new JArray();
//            if (this.Lines == null) return array;

//            foreach (Line line in this.Lines)
//            {
//                JObject jObject = new JObject();
//                jObject.Add("linno", line.NO);
//                if (line.LineID != null)
//                    jObject.Add("lineID", line.LineID);
//                jObject.Add("lineName", line.Name);
//                JArray jtowers = towers_to_json(line.TowerList);
//                jObject.Add("towers", jtowers);
//                array.Add(jObject);
//            }
//            return array;

//        }

//        public JObject ToJson()
//        {
//            JObject jObject = new JObject();
//            jObject.Add("root", new JValue(this.Root));
//            JArray jArray = lines_to_json();
//            jObject.Add("lines", jArray);
//            return jObject;
//        }
//    }
//}
