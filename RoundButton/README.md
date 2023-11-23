# 1、效果演示

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

<div align="center"><button class="button">圆角按钮</button></div>

# 2、使用方法

> 📌 <font color="#FF6699">**扩展属性**</font>
>
> `RoundButton` 提供了一些扩展属性：
> * **RgnRadius**：获取或设置圆角半径；
> * **RoundBackColor**：获取或设置圆角按钮背景颜色，默认为浅蓝色；
> * **RoundBorderColor**：获取或设置圆角按钮轮廓颜色，默认为蓝色；
> * **RoundBorderSize**：获取或设置一个值，该值指定圆角按钮周围的边框的大小（以像素为单位），默认为 1；
> * **RoundButtonPressedColor**：获取或设置圆角按钮按下时背景色，默认为深蓝色;<br><div align="center"><img src="./images/2-ExtensionProperties.png" alt="扩展属性"></div>

> 📌 <font color="#FF6699">**注意**</font>
>
> 由于 Windows 11 对系统 UI 视觉设计的更改，Win11 默认的按钮样式已经更改为圆角按钮了。而 Winform Button 控件会根据操作系统的主题和样式进行渲染，所以 Winform 的按钮控件在 Win11 上已经默认显示为圆角了。
> > **已经...可以不用再战斗了**😢

# 3、[完整源码](RoundButton.cs)