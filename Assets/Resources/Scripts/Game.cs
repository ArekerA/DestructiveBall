using UnityEngine;
using System.Xml;
using System.IO;

public static class GameStatic
{
    public static int levels = 18;
    private static int score = 0;
    private static int allScore = 0;
    private static int maxScore = 0;
    private static int lifes = 3;
    private static bool isDead = false;
    private static int balls = 1;
    private static bool next = false;
    public static int ZeroScore()
    {
        return score = 0;
    }
    public static int ZeroAllScore()
    {
        return allScore = 0;
    }
    public static int GetScore()
    {
        return score;
    }
    public static int GetAllScore()
    {
        return allScore;
    }
    public static int AddScore()
    {
        ++allScore;
        return ++score;
    }
    public static int AddScore(int a)
    {
        allScore += a;
        return (score += a);
    }
    public static int SetMaxScore(int i)
    {
        return maxScore = i;
    }
    public static int GetMaxScore()
    {
        return maxScore;
    }
    public static int SetLifes(int i)
    {
        return lifes = i;
    }
    public static int GetLifes()
    {
        return lifes;
    }
    public static bool SetNext(bool i)
    {
        return next = i;
    }
    public static bool IsNext()
    {
        return next;
    }
    public static bool CheckScore()
    {
        return maxScore <= score;
    }
    public static bool Revive()
    {
        return (isDead = false);
    }
    public static bool Kill()
    {
        return (isDead = true);
    }
    public static int GetBalls()
    {
        return balls;
    }
    public static void SetBalls(int i)
    {
        balls = i;
    }
    public static void SaveScore(string name)
    {
        bool isHight = true;
        string filepath = Application.persistentDataPath + "/scores.xml";
        XmlDocument xml = new XmlDocument();
        if (!File.Exists(filepath))
        {
            xml.AppendChild(xml.CreateElement("scores"));
            xml.Save(filepath);
        }
        xml.Load(filepath);
        XmlNodeList xmlList = xml.GetElementsByTagName("score");
        XmlNode temp = xmlList.Item(0);
        foreach (XmlNode scoreS in xmlList)
        {
            XmlNodeList scoreContent = scoreS.ChildNodes;
            foreach (XmlNode scoreItem in scoreContent)
            {
                if (scoreItem.Name == "value")
                {
                    if(int.Parse(scoreItem.InnerText)>allScore)
                    {
                        temp = scoreS;
                        isHight = false;
                    }
                }
            }
        }
        XmlElement elRoot = xml.DocumentElement;
        XmlElement elScore = xml.CreateElement("score");
        XmlElement elName = xml.CreateElement("name");
        elName.InnerText = name;
        XmlElement elValue = xml.CreateElement("value");
        elValue.InnerText = allScore.ToString();
        elScore.AppendChild(elName);
        elScore.AppendChild(elValue);
        if(isHight)
            elRoot.InsertBefore(elScore, temp);
        else
            elRoot.InsertAfter(elScore, temp);
        xml.Save(filepath);
        ZeroAllScore();
        ZeroScore();
        SetNext(false);
        SetLifes(3);
    }
    public static string[] LoadScore()
    {
        Debug.Log(Application.persistentDataPath);
        string[] str = { "", "", "" };
        int lines = 0;
        string filepath = Application.persistentDataPath + "/scores.xml";
        XmlDocument xml = new XmlDocument();
        if (File.Exists(filepath))
        {
            xml.Load(filepath);
            XmlNodeList xmlList = xml.GetElementsByTagName("score");

            foreach (XmlNode score in xmlList)
            {
                XmlNodeList scoreContent = score.ChildNodes;
                foreach (XmlNode scoreItem in scoreContent)
                {
                    if (scoreItem.Name == "name")
                    {
                        str[0] += scoreItem.InnerText + "\n";
                    }
                    if (scoreItem.Name == "value")
                    {
                        str[1] += scoreItem.InnerText + "\n";
                    }
                }
                lines++;
            }
        }
        str[2] = lines.ToString();
        return str;
    }
    public static void SaveUnlockedLevels(int world, int lvl)
    {
        string filepath = Application.dataPath + @"/Resources/Data/unlocked.xml";
        XmlDocument xml = new XmlDocument();
        if (!File.Exists(filepath))
        {
            XmlElement el1 = xml.CreateElement("unlocked");
            XmlElement el2 = xml.CreateElement("worlds");
            XmlElement el3 = xml.CreateElement("world1");
            XmlElement el4 = xml.CreateElement("world2");
            el3.InnerText = "1";
            el4.InnerText = "1";
            el2.AppendChild(el3);
            el2.AppendChild(el4);
            el1.AppendChild(el2);
            xml.AppendChild(el1);
            xml.Save(filepath);
        }
        xml.Load(filepath);
        if (world == 1)
        {
            XmlNodeList aNodes = xml.SelectNodes("/unlocked/worlds/world1");
            foreach (XmlNode aNode in aNodes)
            {
                aNode.InnerText = lvl.ToString();
            }
        }
        else if (world == 2)
        {
            XmlNodeList aNodes = xml.SelectNodes("/unlocked/worlds/world2");
            foreach (XmlNode aNode in aNodes)
            {
                aNode.InnerText = lvl.ToString();
            }
        }
        xml.Save(filepath);

    }
    public static int[] LoadUnlockedLevels()
    {
        int[] lvl = { 1,1};
        string filepath = Application.dataPath + @"/Resources/Data/unlocked.xml";
        XmlDocument xml = new XmlDocument();
        if (File.Exists(filepath))
        {
            xml.Load(filepath);
            XmlNodeList xmlList = xml.GetElementsByTagName("worlds");

            foreach (XmlNode world in xmlList)
            {
                XmlNodeList worldContent = world.ChildNodes;
                foreach (XmlNode worldItem in worldContent)
                {
                    if (worldItem.Name == "world1")
                    {
                        lvl[0] += int.Parse(worldItem.InnerText);
                    }
                    if (worldItem.Name == "world2")
                    {
                        lvl[1] += int.Parse(worldItem.InnerText);
                    }
                }
            }
        }
        return lvl;
    }
}
