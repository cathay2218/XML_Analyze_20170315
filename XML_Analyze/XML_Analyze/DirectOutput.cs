using System;
using System.Xml;

namespace XML_Analyze
{
    class DirectOutput
    {
        public static void Direct_Console_Output()
        {
            XmlDocument doc = new XmlDocument();
            Console.WriteLine(@"Loading XML File...");
            doc.Load(@"http://opendata.hccg.gov.tw/dataset/c298f31a-54a9-410d-804c-ef96a2a75130/resource/7db82690-f637-42a5-8408-40d6fcfba868/download/20170223091823530.xml");

            Console.WriteLine("Analyze XML File...\n");
            XmlNode Node = doc.LastChild;
            XmlNodeList List = Node.ChildNodes;

            Console.WriteLine(@"新竹市公共圖書館圖書借閱量統計");
            Console.WriteLine("==============================================================");
            Console.WriteLine("統計年月  館藏地");
            Console.WriteLine("借閱人次  借閱冊數  還書人次  還書冊數  預約人次  預約冊數");
            Console.WriteLine("==============================================================");

            for (int i = 0; i < List.Count; i++)
            {
                XmlNodeList analyze = List[i].ChildNodes;
                Console.Write("{0,-10}", analyze[0].LastChild.InnerText);
                Console.Write("{0,-16}", analyze[1].LastChild.InnerText);
                Console.WriteLine();
                Console.Write("{0,-10}", analyze[2].LastChild.InnerText);
                Console.Write("{0,-10}", analyze[3].LastChild.InnerText);
                Console.Write("{0,-10}", analyze[6].LastChild.InnerText);
                Console.Write("{0,-10}", analyze[7].LastChild.InnerText);
                Console.Write("{0,-10}", analyze[8].LastChild.InnerText);
                Console.Write("{0,-10}", analyze[9].LastChild.InnerText);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
