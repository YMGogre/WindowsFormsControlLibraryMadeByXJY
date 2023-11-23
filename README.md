# WindowsFormsControlLibraryMadeByXJY

本网站是 [.NET Framework](https://dotnet.microsoft.com/zh-cn/learn/dotnet/what-is-dotnet-framework "什么是.NET Framework? 一个软件开发框架") 下 [WinForms](https://learn.microsoft.com/zh-cn/dotnet/desktop/winforms/?view=netframeworkdesktop-4.8 ".NET 的 Windows 窗体相关文档 \| Microsoft Learn") 框架[自定义控件仓库](https://github.com/YMGogre/WindowsFormsControlLibraryMadeByXJY)的在线预览网站。

## 1、先决条件

* [Visual Studio 2022 版本 17.4 或更高版本](https://visualstudio.microsoft.com/zh-hans/downloads/)
  * 选择 [.NET 桌面开发工作负载](https://learn.microsoft.com/zh-cn/visualstudio/install/modify-visual-studio?view=vs-2022&preserve-view=true#modify-workloads)

## 2、仓库组成

<style>
    .button {
        width: 110px;                           /* 控制按钮的宽度 */
        height: 45px;                           /* 控制按钮的高度 */
        border-radius: 7px;                     /* 控制按钮的圆角尺寸 */
        color: #409EFF;                         /* 控制文本颜色 */
        background-color: #ECF5FF;              /* 控制按钮底色 */
        border-color: #409EFF;                  /* 控制轮廓颜色 */
        border-width: 1px;                      /* 控制边框大小 */
        border-style: solid;                    /* 设置边框样式为实线 */
        font-family: 'Microsoft YaHei';         /* 设置字体为微软雅黑 */
        font-size: 12pt;                        /* 设置字体大小为12pt */
        cursor: pointer;                        /* 设置鼠标移入按钮时光标样式为“手” */
    }
    .button:hover {
        color: #FFFFFF;                         /* 控制鼠标移入按钮时的文本颜色 */
        background-color: #409EFF;              /* 控制鼠标移入按钮时的按钮底色 */
    }
    .button:active{
        background-color: #3A8EE6;              /* 控制按钮被按下时的按钮底色 */
    }
</style>

<style>
    #circle {
        width: 42px;
        height: 42px;
        border-radius: 50%;
        position: relative;
        margin: 0 auto;
    }
</style>

|控件名称|效果预览|
|:---|:---:|
|[IP 地址输入框控件](./IPAddrInputer/README.md)|![IPAddrInputer](./images/IPAddrInputer.PNG)|
|[开关控件](./Switch/README.md)|![Switch](./images/Switch.png)|
|[水印文本框控件](./WatermarkTextBox/README.md)|![WatermarkTextBox](./images/WatermarkTextBox.PNG)|
|[选择器控件](./Selector/README.md)|![Selector](./images/Selector.PNG)|
|[圆角按钮控件](./RoundButton/README.md)|<button class="button">圆角按钮</button>|
|[指示灯控件](./IndicatorLight/README.md)|<div id="circle"></div>|
 
<script>
    var colors = ["#C8C9CC", "#409EFF", "#67C23A", "#E6A23C", "#F56C6C"];
    var i = 0;
    setInterval(function() {
        document.getElementById('circle').style.backgroundColor = colors[i];
        i = (i + 1) % colors.length;
    }, 1000);
</script>

## 3、使用方法

如果您想要了解如何创建 .NET Framework 下 WinForms 自定义控件库或者想了解如何在其他项目中引用本仓库，请参考：
> [在Visual Studio中创建自定义Winform控件库并在其他解决方案中引用](https://blog.csdn.net/YMGogre/article/details/126508042 "【入门级图文教程】在Visual Studio中创建自定义Winform控件库并在其他解决方案中引用 - CSDN 博客")
> <div align="center"><img src="./images/csdn.png" alt="csdn" width="128"></div>