# 哈希值读取程序

## 程序介绍

这是一个使用 C# 语言和 Visual Studio 中的命令行窗口模板编写的哈希值读取程序，它可以打开文件或比较文件的哈希值，支持 MD5 和 SHA1 两种算法。程序的主要功能有：

- 打开文件：输入要打开的文件路径，程序会计算并显示文件的 MD5 值和 SHA1 值。
- 比较文件：输入两个文件的路径，程序会计算并显示两个文件的 MD5 值和 SHA1 值，以及是否相同。
- 退出程序：结束程序的运行。

## 程序运行

要运行这个程序，你需要以下环境和步骤：

- 安装 Visual Studio 2019 或更高版本。
- 下载或克隆本项目的源代码，打开 HashReader.sln 文件。
- 在 Visual Studio 中，选择“调试”菜单，然后选择“开始调试”或“开始不带调试”。
- 在命令行窗口中，根据提示选择操作和输入文件路径。

## 程序原理

这个程序使用了HashHelper类来计算指定文件的哈希值，该类提供了两个静态方法：ComputeMD5 和 ComputeSHA1，分别用于计算文件的 MD5 值和 SHA1 值。这两个方法的实现原理如下：

- 打开文件流，读取文件的字节数据。
- 创建 MD5 或 SHA1 的加密服务提供者对象，调用其 ComputeHash 方法，传入文件的字节数据，得到哈希值的字节数组。
- 释放加密服务提供者对象的资源。
- 将哈希值的字节数组转换成十六进制的字符串形式，返回结果。

程序的主要逻辑在 Program 类中，该类提供了以下几个静态方法：

- Main：程序的入口方法，显示欢迎信息，然后调用 ShowMenu 方法。
- ShowMenu：显示菜单，让用户选择操作，然后根据用户的输入调用相应的方法，或者显示错误信息并重新显示菜单。
- OpenFile：打开文件并显示哈希值，让用户输入文件路径，然后调用 HashHelper 类的方法计算并显示文件的 MD5 值和 SHA1 值，或者显示文件不存在的错误信息，最后调用 ShowOptions 方法。
- CompareFiles：比较两个文件的哈希值，让用户输入两个文件的路径，然后调用 HashHelper 类的方法计算并显示两个文件的 MD5 值和 SHA1 值，以及是否相同，或者显示文件不存在的错误信息，最后调用 ShowOptions 方法。
- ExitProgram：退出程序，显示感谢信息，然后调用 Environment.Exit 方法结束程序的运行。
- ShowOptions：显示选项，让用户选择继续操作，返回菜单，或者退出程序，然后根据用户的输入调用相应的方法，或者显示错误信息并重新显示选项。
