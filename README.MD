# VisionMaster Helper Script

这是一个为海康机器人 VisionMaster 平台开发的辅助脚本，由作者 **SHISUYO** 个人兴趣完成。  
脚本使用 VisionMaster 提供的公开接口编写，用于执行：

> 基于开源项目 [Umi-OCR](https://github.com/hiroi-sora/Umi-OCR) 提供的 HTTP 接口，  
> 由 VisionMaster 中的脚本实现图片裁剪、转码（Base64）、上传并获取 OCR 数据。

> ⚠️ 本项目与任何单位或工作项目无关，脚本运行依赖 VisionMaster 平台，仅供学习与参考用途。

---

## ✨ 功能简介

- 将软件内的 `IMAGE` 变量转换为 Base64 编码并上传至服务器，获取文字识别结果。
- 将 `IMAGE` 变量保存为 JPG、BMP、PNG、TIF 等格式至本地磁盘。
- 支持将图像保存至共享文件夹或网络驱动器（前提是已正确挂载并拥有读写权限）。

---

## 🚀 使用方式

> ⚙️ 本脚本仅在 VisionMaster 4.3 以上版本测试通过，  
> 4.2 及更低版本用户请自行测试，使用造成的时间或学习成本与作者无关。

### 🗂 脚本结构

- `Umi-OCR-Socket.sol`：主程序文件
  - `Image`
  - `Cut-Image`
  - `OCR-Socket`（核心模块）
  - `Save-Test`

> `Save-Test` 与 `Cut-Image` 为测试模块，实际使用推荐仅保留 `OCR-Socket`以及`Image`。

### 📌 使用步骤

1. 打开 `OCR-Socket` 模块的编辑程序集。
2. 删除以下 DLL 引用：
   - `Microsoft.Extensions.Identity.Core.DLL`
   - `Newtonsoft.Json.DLL`
   - `System.Drawing.DLL`
3. 重新导入上述 DLL 文件（在DLL文件夹内已经有了）。
4. 按脚本内备注提示使用相应变量。

### 📥 变量说明

- **in1**：接收脚本外部传入的图像变量。  
  请勿使用灰度图变量，务必选择原始图像源。

---

## 📦 项目依赖

- VisionMaster 平台（及其 `Script.Methods` 类库）
- [.NET Framework 标准库](https://learn.microsoft.com/dotnet/)
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)（MIT License）

> 以上库用户需在本地环境中确保可用，无需随项目一同分发。

---

## 📄 License

本项目遵循 [Apache License 2.0](./LICENSE) 开源协议。  
你可以自由使用、修改、分发，但需保留原作者署名。

---

## 👤 作者

**SHISUYO (施苏悠)**  
🔗 [https://github.com/SHISUYO](https://github.com/SHISUYO)
