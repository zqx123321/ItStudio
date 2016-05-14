using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// GoogleCode 的摘要说明
/// </summary>
public class Captcha
{

    /// <summary>
    /// 全局随机数生成器
    /// </summary>
    private Random rndNumber;
    public static string mrChineseChars = String.Empty;
    /// <summary>
    /// 英文与数字串
    /// </summary>
    protected static readonly string mrEnglishOrNumChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public Captcha()
    {
        rndNumber = new Random(unchecked((int)DateTime.Now.Ticks));
    }
    // 验证码长度(默认6个验证码的长度)
    int length = 5;
    public int Length
    {
        get { return length; }
        set { length = value; }
    }
    //验证码字体大小
    int fontSize = 18;
    public int FontSize
    {
        get { return fontSize; }
        set { fontSize = value; }
    }
    // 边框补(默认4像素)
    int padding = 4;
    public int Padding
    {
        get { return padding; }
        set { padding = value; }
    }
    // 是否输出燥点(默认输出)
    bool chaos = true;
    public bool Chaos
    {
        get { return chaos; }
        set { chaos = value; }
    }
    // 输出燥点的颜色(默认灰色)
    Color chaosColor = Color.LightGray;
    public Color ChaosColor
    {
        get { return chaosColor; }
        set { chaosColor = value; }
    }
    // 自定义背景色(默认白色)
    Color backgroundColor = Color.White;
    public Color BackgroundColor
    {
        get { return backgroundColor; }
        set { backgroundColor = value; }
    }
    // 自定义随机颜色数组
    Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
    public Color[] Colors
    {
        get { return colors; }
        set { colors = value; }
    }
    // 自定义字体数组
    string[] fonts = { "Arial", "Georgia" };
    public string[] Fonts
    {
        get { return fonts; }
        set { fonts = value; }
    }
    #region 产生扭曲图片
    private const double PI = 3.1415926535897932384626433832795;
    private const double PI2 = 6.283185307179586476925286766559;
    /// <summary>
    /// 该方法用于扭曲图片
    /// </summary>
    /// <param name="srcBmp">图片路径</param>
    /// <param name="bXDir">如果扭曲则选择为True</param>
    /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
    /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
    /// <returns></returns>
    public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
    {
        System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);//创建Bitmap对象
        // 将位图背景填充为白色
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(destBmp);//创建Graphics对象
        g.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);//将位图背景填充为白色
        g.Dispose();//释放raphics对象
        double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;//判断扭曲方式
        for (int i = 0; i < destBmp.Width; i++)
        {
            for (int j = 0; j < destBmp.Height; j++)
            {
                double dx = 0;
                dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                dx += dPhase;
                double dy = Math.Sin(dx);
                // 取得当前点的颜色
                int nOldX = 0, nOldY = 0;
                nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                nOldY = bXDir ? j : j + (int)(dy * dMultValue);
                System.Drawing.Color color = srcBmp.GetPixel(i, j);
                if (nOldX >= 0 && nOldX < destBmp.Width
                 && nOldY >= 0 && nOldY < destBmp.Height)
                {
                    destBmp.SetPixel(nOldX, nOldY, color);
                }
            }
        }
        return destBmp;
    }
    #endregion
    /// <summary>
    /// 生成校验码图片
    /// </summary>
    /// <param name="code">验证码</param>
    /// <returns></returns>
    public Bitmap CreateImage(string code)
    {
        int fSize = FontSize;
        int fWidth = fSize + Padding;
        int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
        int imageHeight = fSize * 2 + Padding * 2;
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);
        Graphics g = Graphics.FromImage(image);
        g.Clear(BackgroundColor);
        //给背景添加随机生成的燥点
        if (this.Chaos)
        {
            Pen pen = new Pen(ChaosColor, 0);
            int c = Length * 10;
            for (int i = 0; i < c; i++)
            {
                int x = rndNumber.Next(image.Width);
                int y = rndNumber.Next(image.Height);

                g.DrawRectangle(pen, x, y, 1, 1);
            }
        }
        int left = 0, top = 0, top1 = 1, top2 = 1;
        int n1 = (imageHeight - FontSize - Padding * 2);
        int n2 = n1 / 4;
        top1 = n2;
        top2 = n2 * 2;
        Font f;
        Brush b;
        int cindex, findex;
        //随机字体和颜色的验证码字符
        for (int i = 0; i < code.Length; i++)
        {
            cindex = rndNumber.Next(Colors.Length - 1);
            findex = rndNumber.Next(Fonts.Length - 1);
            f = new System.Drawing.Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
            b = new System.Drawing.SolidBrush(Colors[cindex]);
            if (i % 2 == 1)
            {
                top = top2;
            }
            else
            {
                top = top1;
            }
            left = i * fWidth;
            g.DrawString(code.Substring(i, 1), f, b, left, top);
        }
        //画一个边框 边框颜色为Color.Gainsboro
        g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
        g.Dispose();
        //产生波形（Add By 51aspx.com）
        image = TwistImage(image, true, 8, 4);
        return image;
    }
    /// <summary>
    /// 生成随机字符码
    /// </summary>
    /// <param name="codeLen">字符串长度</param>
    /// <param name="zhCharsCount">中文字符数</param>
    /// <returns></returns>
    public string CreateVerifyCode(int codeLen, int zhCharsCount)
    {
        char[] chs = new char[codeLen];

        int index;
        for (int i = 0; i < zhCharsCount; i++)
        {
            index = rndNumber.Next(0, codeLen);
            if (chs[index] == '\0')
                chs[index] = CreateZhChar();
            else
                --i;
        }
        for (int i = 0; i < codeLen; i++)
        {
            if (chs[i] == '\0')
                chs[i] = CreateEnOrNumChar();
        }

        return new string(chs, 0, chs.Length);
    }
    // 生成默认长度5的随机字符码
    public string CreateVerifyCode()
    {
        return CreateVerifyCode(Length, 0);
    }
    // 生成英文或数字字符
    protected char CreateEnOrNumChar()
    {
        return mrEnglishOrNumChars[rndNumber.Next(0, mrEnglishOrNumChars.Length)];
    }
    protected char CreateZhChar() // 生成汉字字符
    {
        //若提供了汉字集，查询汉字集选取汉字
        if (mrChineseChars.Length > 0)
        {
            return mrChineseChars[rndNumber.Next(0, mrChineseChars.Length)];
        }
        //若没有提供汉字集，则根据《GB2312简体中文编码表》编码规则构造汉字
        else
        {
            byte[] bytes = new byte[2];

            //第一个字节值在0xb0, 0xf7之间
            bytes[0] = (byte)rndNumber.Next(0xb0, 0xf8);
            //第二个字节值在0xa1, 0xfe之间
            bytes[1] = (byte)rndNumber.Next(0xa1, 0xff);

            //根据汉字编码的字节数组解码出中文汉字
            string str1 = Encoding.GetEncoding("gb2312").GetString(bytes);

            return str1[0];
        }
    }
}