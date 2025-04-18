# VisionMaster Helper Script

这是一个为海康机器人 VisionMaster 平台开发的辅助脚本，由作者：SHISUYO个人兴趣完成。  
脚本使用 VisionMaster 提供的公开接口编写，用于执行 [基于开源项目Umi-OCR(https://github.com/hiroi-sora/Umi-OCR)提供的HTTP接口,由海康机器人视觉算法软件VisionMaster中的脚本来进行图片的裁剪，转码Base64，上传，获取OCR数据]。

> ⚠️ 本项目与任何单位或工作项目无关，脚本运行依赖 VisionMaster 平台，仅供学习与参考用途。

---

## ✨ 功能简介

- 可直接将软件内的IMAGE变量直接转换为Base64编码名上传到服务器中，并获取图片中文字信息(注意断句)
- 可直接将软件内的IMAGE变量直接以Jpg,Bmp,Png.Tif等格式存入计算机本地磁盘中
- (或者是共享文件夹或者是网络驱动器,前提是你已经挂载到你的计算机内并且你已经为其分配盘符并且你有增删改查权限)

---

## 🚀 使用方式

(本脚本仅在VM4.3以上版本进行测试,4.2及其更低版本还请使用者自行测试,由使用者自行测试导致的时间以及学习成本与作者无关)
双击打开“Umi-OCR-Socket.sol”文件即可打开程序文件
文件内有Image,Cut-Image,OCR-Socket,Save-Test模块
其中Save-Test和Cut-Image仅供测试使用,核心以OCR-Socket模块为主
使用前,请先打开OCR-Socket模块中的编辑程序集
并将  “Microsoft.Extensions.Identity.Core.DLL”,
      “Newtonsoft.Json.DLL”,
      “System.Drawing.DLL”删除
并重新导入这三个必要DLL文件
否则预编译时会出现缺少Using指令集报错
详细使用信息及其关键指令信息在脚本备注里已做尽数提示

必要变量提示信息：
in1:  此变量接收由脚本外部输入的图像变量
      请注意,这里不可选择选项中的灰度图像,无论Image模块里面的是RGB24还是MONO8
      请务必选择图像源或者是不带灰度图像后缀的图像变量

---

## 📦 项目依赖

本脚本项目依赖以下外部库：

- 脚本运行依赖 VisionMaster 工业视觉平台，特别是其提供的 `Script.Methods` 类库。
- [.NET Framework 标准库](https://learn.microsoft.com/dotnet/) （由 Microsoft 提供）
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) — JSON 序列化工具，遵循 MIT License

以上库无需随项目一起分发，用户在使用时需确保本地环境支持。

---

## 📄 License

本项目遵循 [Apache License 2.0](./LICENSE) 开源协议。  
你可以自由使用、修改、分发，但需保留原作者署名。

---

## 👤 作者

SHISUYO(施苏悠) - [https://github.com/SHISUYO]
