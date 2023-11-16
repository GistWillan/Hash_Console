using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace HashReader
{
    class Program
    {
        // 添加一个静态变量来记录上一个任务的类型
        static string lastTask = "";

        // 添加一个列表用于存储历史查询记录
        static List<string> historyQueries = new List<string>();

        // 添加一个对象用于锁定历史查询记录
        static object historyLock = new object();

        // 添加一个常量用于存储历史记录文件的路径
        const string HistoryFilePath = "history.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用哈希值读取程序。");

            // 加载历史记录
            LoadHistory();

            ShowMenu();
        }

        // 显示菜单
        static void ShowMenu()
        {
            Console.Clear(); // 清空控制台内容
            Console.WriteLine("请选择一个操作：");
            Console.WriteLine("1. 打开文件");
            Console.WriteLine("2. 比较文件");
            Console.WriteLine("3. 历史查询记录");
            Console.WriteLine("4. 退出程序");
            Console.Write("请输入你的选择：");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    OpenFile();
                    break;
                case "2":
                    CompareFiles();
                    break;
                case "3":
                    ShowHistory();
                    break;
                case "4":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("无效的输入，请重新选择。");
                    ShowMenu();
                    break;
            }
        }

        // 打开文件并显示哈希值
        static void OpenFile()
        {
            Console.Clear(); // 清空控制台内容
            // 更新上一个任务的类型
            lastTask = "打开文件";
            Console.Write("请输入要打开的文件路径：");
            string filePath = Console.ReadLine();
            if (File.Exists(filePath))
            {
                Console.WriteLine("文件已打开，正在计算哈希值...");
                string hashMD5 = HashHelper.ComputeMD5(filePath);
                string hashSHA1 = HashHelper.ComputeSHA1(filePath);
                Console.WriteLine($"文件的 MD5 值是：{hashMD5}");
                Console.WriteLine($"文件的 SHA1 值是：{hashSHA1}");

                // 将查询记录添加到历史查询列表
                AddToHistory($"类型: {lastTask}, 文件路径: {filePath}, MD5: {hashMD5}, SHA1: {hashSHA1}");

                // 显示选项
                ShowOptions();
            }
            else
            {
                Console.WriteLine("文件不存在，请检查路径是否正确。");
                // 显示选项
                ShowOptions();
            }
        }

        // 比较两个文件的哈希值
        static void CompareFiles()
        {
            Console.Clear(); // 清空控制台内容
            // 更新上一个任务的类型
            lastTask = "比较文件";
            Console.Write("请输入第一个文件的路径：");
            string filePath1 = Console.ReadLine();
            Console.Write("请输入第二个文件的路径：");
            string filePath2 = Console.ReadLine();
            if (File.Exists(filePath1) && File.Exists(filePath2))
            {
                Console.WriteLine("文件已打开，正在比较哈希值...");
                string hashMD51 = HashHelper.ComputeMD5(filePath1);
                string hashMD52 = HashHelper.ComputeMD5(filePath2);
                string hashSHA11 = HashHelper.ComputeSHA1(filePath1);
                string hashSHA12 = HashHelper.ComputeSHA1(filePath2);
                Console.WriteLine($"第一个文件的 MD5 值是：{hashMD51}");
                Console.WriteLine($"第二个文件的 MD5 值是：{hashMD52}");
                Console.WriteLine($"第一个文件的 SHA1 值是：{hashSHA11}");
                Console.WriteLine($"第二个文件的 SHA1 值是：{hashSHA12}");

                // 将查询记录添加到历史查询列表
                AddToHistory($"类型: {lastTask}, 文件1路径: {filePath1}, MD5: {hashMD51}, SHA1: {hashSHA11}");
                AddToHistory($"类型: {lastTask}, 文件2路径: {filePath2}, MD5: {hashMD52}, SHA1: {hashSHA12}");

                if (hashMD51 == hashMD52)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("两个文件的 MD5 值相同。");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("两个文件的 MD5 值不同。");
                    Console.ResetColor();
                }
                if (hashSHA11 == hashSHA12)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("两个文件的 SHA1 值相同。");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("两个文件的 SHA1 值不同。");
                    Console.ResetColor();
                }

                // 显示选项
                ShowOptions();
            }
            else
            {
                Console.WriteLine("文件不存在，请检查路径是否正确。");
                // 显示选项
                ShowOptions();
            }
        }

        // 显示历史查询记录
        static void ShowHistory()
        {
            Console.Clear(); // 清空控制台内容
            // 更新上一个任务的类型
            lastTask = "历史查询记录";

            if (historyQueries.Count > 0)
            {
                Console.WriteLine("历史查询记录：");
                Console.WriteLine("------------------------------------------");
                for (int i = 0; i < historyQueries.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {historyQueries[i]}");
                    Console.WriteLine("------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("暂无历史查询记录。");
            }

            // 移除继续操作选项
            ShowOptions(false);
        }

        // 退出程序
        static void ExitProgram()
        {
            Console.Clear(); // 清空控制台内容
            Console.WriteLine("感谢使用哈希值读取程序，再见。");

            // 保存历史记录
            SaveHistory();

            Environment.Exit(0);
        }

        // 显示选项
        static void ShowOptions(bool showContinueOption = true)
        {
            Console.WriteLine();
            Console.WriteLine("请选择一个选项：");
            if (showContinueOption)
            {
                Console.WriteLine("1. 继续操作");
            }
            Console.WriteLine("2. 返回菜单");
            Console.WriteLine("3. 退出程序");
            Console.Write("请输入你的选择：");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    // 根据上一个任务的类型来决定继续操作的行为
                    if (lastTask == "打开文件")
                    {
                        OpenFile();
                    }
                    else if (lastTask == "比较文件")
                    {
                        CompareFiles();
                    }
                    else if (lastTask == "历史查询记录")
                    {
                        ShowMenu();
                    }
                    break;
                case "2":
                    if (lastTask == "打开文件" || lastTask == "比较文件")
                    {
                        ShowMenu();
                    }
                    else
                    {
                        ExitProgram();
                    }
                    break;
                case "3":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("无效的输入，请重新选择。");
                    ShowOptions(showContinueOption);
                    break;
            }
        }

        // 将查询记录添加到历史查询列表
        static void AddToHistory(string query)
        {
            // 使用锁确保多线程安全访问列表
            lock (historyLock)
            {
                historyQueries.Add(query);
            }

            // 保存历史记录
            SaveHistory();
        }

        // 保存历史记录到文件
        static void SaveHistory()
        {
            try
            {
                File.WriteAllLines(HistoryFilePath, historyQueries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"无法保存历史记录：{ex.Message}");
            }
        }

        // 从文件加载历史记录
        static void LoadHistory()
        {
            if (File.Exists(HistoryFilePath))
            {
                try
                {
                    historyQueries.AddRange(File.ReadAllLines(HistoryFilePath));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法加载历史记录：{ex.Message}");
                }
            }
        }
    }

    // 提供用于计算指定文件哈希值的方法
    public sealed class HashHelper
    {
        // 计算指定文件的MD5值
        public static String ComputeMD5(String fileName)
        {
            String hashMD5 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的MD5值
                    System.Security.Cryptography.MD5 calculator = System.Security.Cryptography.MD5.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashMD5 = stringBuilder.ToString();
                }//关闭文件流
            }//结束计算
            return hashMD5;
        }//ComputeMD5

        // 计算指定文件的SHA1值
        public static String ComputeSHA1(String fileName)
        {
            String hashSHA1 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的SHA1值
                    System.Security.Cryptography.SHA1 calculator = System.Security.Cryptography.SHA1.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashSHA1 = stringBuilder.ToString();
                }//关闭文件流
            }
            return hashSHA1;
        }//ComputeSHA1
    }
}