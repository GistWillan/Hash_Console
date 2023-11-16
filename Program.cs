﻿using System;
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
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用哈希值读取程序。");
            ShowMenu();
        }

        // 显示菜单
        static void ShowMenu()
        {
            Console.WriteLine("请选择一个操作：");
            Console.WriteLine("1. 打开文件");
            Console.WriteLine("2. 比较文件");
            Console.WriteLine("3. 退出程序");
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
            Console.Write("请输入要打开的文件路径：");
            string filePath = Console.ReadLine();
            if (File.Exists(filePath))
            {
                Console.WriteLine("文件已打开，正在计算哈希值...");
                string hashMD5 = HashHelper.ComputeMD5(filePath);
                string hashSHA1 = HashHelper.ComputeSHA1(filePath);
                Console.WriteLine($"文件的 MD5 值是：{hashMD5}");
                Console.WriteLine($"文件的 SHA1 值是：{hashSHA1}");
            }
            else
            {
                Console.WriteLine("文件不存在，请检查路径是否正确。");
            }
            ShowOptions();
        }

        // 比较两个文件的哈希值
        static void CompareFiles()
        {
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
            }
            else
            {
                Console.WriteLine("文件不存在，请检查路径是否正确。");
            }
            ShowOptions();
        }

        // 退出程序
        static void ExitProgram()
        {
            Console.WriteLine("感谢使用哈希值读取程序，再见。");
            Environment.Exit(0);
        }

        // 显示选项
        static void ShowOptions()
        {
            Console.WriteLine("请选择一个选项：");
            Console.WriteLine("1. 继续操作");
            Console.WriteLine("2. 返回菜单");
            Console.WriteLine("3. 退出程序");
            Console.Write("请输入你的选择：");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    if (Console.Title == "打开文件")
                    {
                        OpenFile();
                    }
                    else if (Console.Title == "比较文件")
                    {
                        CompareFiles();
                    }
                    break;
                case "2":
                    ShowMenu();
                    break;
                case "3":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("无效的输入，请重新选择。");
                    ShowOptions();
                    break;
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
                    StringBuilder stringBuilder = new StringBuilder();
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
                    StringBuilder stringBuilder = new StringBuilder();
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

