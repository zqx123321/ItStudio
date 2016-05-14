<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-CN">
<head>
    <title>爱特工作室 - 后台管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="爱特工作室 - 后台管理系统" />
    <meta name="keywords" content="爱特工作室 - 后台管理系统" />
    <meta name="Author" content="爱特工作室 - 后台管理系统" />
    <meta name="CopyRight" content="爱特工作室 - 后台管理系统" />
    <link rel="stylesheet" type="text/css" href="./Style/skin.css" />
</head>

    <body>
          <form id="form1" runat="server">
        <table width="100%">
            <!-- 顶部部分 -->
            <tr height="41"><td colspan="2" background="./Images/login_top_bg.gif">&nbsp;</td></tr>
            <!-- 主体部分 -->
            <tr style="background:url(./Images/login_bg.jpg) repeat-x;" height="532">
                <!-- 主体左部分 -->
                <td id="left_cont">
                    <table width="100%" height="100%">
                        <tr height="155"><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td width="20%" rowspan="2">&nbsp;</td>
                            <td width="60%">
                                  <table width="100%">
                                  <img src="./Images/banner.jpg" width="500" height="300" >
                                 </table>
                            <td width="15%" rowspan="2">&nbsp;</td>
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                    </table>
                </td>
                <!-- 主体右部分 -->
                <td id="right_cont">
                    <table height="100%">
                        <tr height="30%"><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <td width="30%" rowspan="5">&nbsp;</td>
                            <td valign="top" id="form">
                                <form action="" method="">
                                    <table valign="top" width="50%">
                                        <tr><td colspan="2"><h4 style="letter-spacing:1px;font-size:16px;">爱特工作室后台管理系统</h4></td></tr>
                                        <tr><td>用户名：</td><td> <asp:TextBox runat="server" ID="UserName" Columns="24" /></td></tr>
                                        <tr><td>密&nbsp;&nbsp;&nbsp;&nbsp;码：</td><td>  <asp:TextBox runat="server" ID="Password" TextMode="Password" Columns="24" /></td></tr>
                                        <tr><td>验证码：</td><td> <asp:TextBox ID="TxtCaptcha" runat="server" placeholder="不区分大小写，点击图片刷新" style="width:80px;" /><img src="ValidateCode.aspx" id="valiCode" alt="验证码" /><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a title="刷新验证码" href="#" onclick="javascript:document.getElementById('valiCode').src='ValidateCode.aspx?id='+Math.random();return false;">
                                  看不清，换张图片？</a>
                                                         </td></tr>
                                        <tr class="bt" align="center"><td>&nbsp; <asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click" Text="登录" /></td><td>&nbsp;<asp:Button ID="BtnRevert" runat="server" OnClick ="BtnRevert_Click" Text="重填" /></td></tr>
                                    </table>
                                </form>
                            </td>
                            <td rowspan="5">&nbsp;</td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                    </table>
                </td>
            </tr>
            <!-- 底部部分 -->
            <tr id="login_bot"><td colspan="2"><p>Copyright 2015 by 爱特工作室 All rights reserved</p></td></tr>
        </table>
              </form>
    </body>
</html>
