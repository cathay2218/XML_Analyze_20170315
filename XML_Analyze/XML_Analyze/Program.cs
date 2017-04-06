using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace XML_Analyze
{
    class Program
    {
        static void Main(string[] args)
        {
            //20170315 直接使用字串處理法輸出
            //DirectOutput.Direct_Console_Output();

            //20170324 類別輸出
            Output_Data(Parser());

            Console.Write(@"請按Enter鍵繼續...");
            Console.ReadLine();
        }

        public static List<Data_Parser> Parser()
        {
            //http://data.gov.tw/node/6076  ->  紫外線即時監測資料
            List<Data_Parser> ParserResult = new List<Data_Parser>();
            
            Console.WriteLine(@"Loading XML File...");
            XElement xml = XElement.Load(@"http://opendata.epa.gov.tw/ws/Data/UV/?format=xml");

            Console.WriteLine("Analyze XML File...\n");
            IEnumerable<XElement> StationNode = xml.Descendants("Data");

            StationNode.ToList().ForEach(stationNode =>
            {
                string StationIdentifier = stationNode.Element("SiteName").Value.Trim();
                string UV_Value = stationNode.Element("UVI").Value.Trim();
                string PublishAgency = stationNode.Element("PublishAgency").Value.Trim();
                string County = stationNode.Element("County").Value.Trim();
                string WGS84Lon = stationNode.Element("WGS84Lon").Value.Trim();
                string WGS84Lat = stationNode.Element("WGS84Lat").Value.Trim();
                string RecordTime = stationNode.Element("PublishTime").Value.Trim();

                Data_Parser parser_repository = new Data_Parser();
                parser_repository._Parser_SiteName = StationIdentifier;
                parser_repository._Parser_UVI = UV_Value;
                parser_repository._Parser_PublishAgency = PublishAgency;
                parser_repository._Parser_County = County;
                parser_repository._Parser_WGS84Lon = WGS84Lon;
                parser_repository._Parser_WGS84Lat = WGS84Lat;
                parser_repository._Parser_PublishTime = RecordTime;

                ParserResult.Add(parser_repository);
                //DataBase_Connect(parser_repository);
            });
            
            return ParserResult;
        }

        public static void Output_Data(List<Data_Parser> input)
        {
            Console.WriteLine(@"已解析{0}筆資料", input.Count);
            Console.WriteLine(@"站點名稱  所在縣市  資料發布單位  資料發布日期");
            Console.WriteLine(@"紫外線數值  站點緯度(Lat)  站點經度(Lon)");
            Console.WriteLine(@"==================================================");

            input.ForEach(temp =>
            {
                Console.WriteLine("{0,-5}{1,-5}{2,-7}{3}", temp._Parser_SiteName, temp._Parser_County, temp._Parser_PublishAgency, temp._Parser_PublishTime);
                Console.WriteLine("{0,-12}{1}\t{2}\n", temp._Parser_UVI, temp._Parser_WGS84Lat, temp._Parser_WGS84Lon);
            });
        }

        //public static void DataBase_Connect(Data_Parser insert)
        //{
        //    const string _connect_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Analyze_DataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //    SqlConnection connection = new SqlConnection(_connect_str);
        //    connection.Open();

        //    SqlCommand command = new SqlCommand("", connection);
        //    command.CommandText = string.Format(@"INSERT INTO ParserResult(SiteName, UVI, PublishAgency, County, WGS84Lon, WGS84Lat, PublishTime)
        //         VALUE ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
        //         insert._Parser_SiteName,insert._Parser_UVI,insert._Parser_PublishAgency,insert._Parser_County,insert._Parser_WGS84Lon,insert._Parser_WGS84Lat,insert._Parser_PublishTime);

        //    command.ExecuteNonQuery();
        //    connection.Close();
        //}
    }
}
