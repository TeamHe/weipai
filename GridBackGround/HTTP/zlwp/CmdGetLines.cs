﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using Newtonsoft.Json.Linq;
//using ResModel.EQU;
//using DB_Operation.EQUManage;


//namespace GridBackGround.HTTP.zlwp
//{
//    public class CmdGetLines
//    {
//        /// <summary>
//        /// http 请求上下文
//        /// </summary>
//        public HttpListenerContext Context { get; set; }

//        /// <summary>
//        /// 请求命令json
//        /// </summary>
//        public JObject JObject { get; set; }

//        public CmdGetLines()
//        {

//        }

//        public CmdGetLines(HttpListenerContext context,JObject jObject)
//        {
//            this.Context = context;
//            this.JObject = jObject;
//        }
       
//        public void Deal()
//        {
//            try
//            {
//                var lineList = DB_Line.List_LineTowerEqu();
//                JObject jObject = new GetLinesResponse(lineList).ToJson();
//                ReSendMsgService.SendResponse(this.Context, jObject);
//                this.Context.Response.Close();
                
//            }
//            catch(Exception ex) 
//            {
//                Zlwp.SendError(this.Context, "CmdGetLines error. " + ex.Message);
//                Context.Response.Close();
//            }
//        }
//    }

//    public class GetLinesResponse{
//        public string Root { get { return "GetLinesRespnse"; } }

//        public List<Line> Lines { get; set; }

//        public GetLinesResponse()
//        {

//        }

//        public GetLinesResponse(List<Line> lines)
//        {
//            this.Lines = lines;
//        }

//        private JArray lines_to_json()
//        {
//            JArray array = new JArray();
//            if (this.Lines == null) return array;

//            foreach (Line line in this.Lines)
//            {
//                JObject jObject = new JObject();
//                jObject.Add("linno", line.NO);
//                if(line.LineID != null)
//                    jObject.Add("lineID", line.LineID);
//                jObject.Add("lineName", line.Name);
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
