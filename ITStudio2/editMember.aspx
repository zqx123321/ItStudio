<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editMember.aspx.cs" Inherits="editMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-CN">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="./Style/skin.css" />
</head>
    <body>
        <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <!-- 头部开始 -->
            <tr>
                <td width="17" valign="top" background="./Images/mail_left_bg.gif">
                    <img src="./Images/left_top_right.gif" width="17" height="29" />
                </td>
                <td valign="top" background="./Images/content_bg.gif">
                    <table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" background="././Images/content_bg.gif">
                        <tr><td height="31"><div class="title">修改成员</div></td></tr>
                    </table>
                </td>
                <td width="16" valign="top" background="./Images/mail_right_bg.gif"><img src="./Images/nav_right_bg.gif" width="16" height="29" /></td>
            </tr>
            <!-- 中间部分开始 -->
            <tr>
                <!--第一行左边框-->
                <td valign="middle" background="./Images/mail_left_bg.gif">&nbsp;</td>
                <!--第一行中间内容-->
                <td valign="top" bgcolor="#F7F8F9">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <!-- 空白行-->
                        <tr><td colspan="2" valign="top">&nbsp;</td><td>&nbsp;</td><td valign="top">&nbsp;</td></tr>
                        <!-- 一条线 -->
                        <!-- 添加产品开始 -->
                        <tr>
                            <td width="2%">&nbsp;</td>
                            <td width="96%">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <div action="" method="">
                                                <table width="100%"class="cont">
                                                    <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td width="15%">成员姓名：</td>
                                                        <td width="20%">  <asp:TextBox ID="txtName" runat="server" ></asp:TextBox></td>
                                                        <td>设置成员姓名 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                            ErrorMessage=" 成员姓名是必填字段。" ForeColor="Red" /></td>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td>年级：</td>
                                                        <td width="20%"> <asp:DropDownList ID="ddlGrade" runat="server" >               
                                                                      </asp:DropDownList></td>
                                                        <td>设置成员年级<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGrade"
                                                             ErrorMessage=" 成员年级是必填字段。"  ForeColor="Red"/></td>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                     <td>&nbsp;</td>
                                                        <td>部门：</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDeparement" runat="server">
                                                                    <asp:ListItem>程序开发</asp:ListItem>
                                                                    <asp:ListItem>前端设计</asp:ListItem>
                                                                    <asp:ListItem>系统维护</asp:ListItem>
                                                                    <asp:ListItem>创客空间</asp:ListItem>
                                                                    <asp:ListItem>UI设计</asp:ListItem>
                                                               </asp:DropDownList>
                                                                                                            </td>
                                                        <td>设置成员部门<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDeparement"
                                                              ErrorMessage=" 成员部门是必填字段。" /></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                     <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td>当前成员照片：</td>
                                                        <td width="20%"> <asp:Image ID="ImgCurrentWorkPho" runat="server"  Width="100px" Height="90px"/></td>
                                                         <td>当前成员照片</td>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td>成员照片：</td>
                                                        <td width="20%"> <asp:FileUpload ID="fulPhoto" runat="server"  /></td>
                                                        <td>上传成员照片</td>
                                                           <asp:Label ID="lblUploadMessage" runat="server" Font-Size="20pt" Text="状态正常" Visible="False"></asp:Label>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                     <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td>当前成员头像：</td>
                                                        <td width="20%"> <asp:Image ID="ImgCurrentWorkIco" runat="server"  Width="100px" Height="90px"/></td>
                                                         <td>当前成员头像</td>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td>成员头像：</td>
                                                        <td width="20%"> <asp:FileUpload ID="fulIco" runat="server"  /></td>
                                                        <td>上传成员头像</td>
                                                          <asp:Label ID="lblUploadMessage2" runat="server" Font-Size="20pt" Text="状态正常" Visible="False"></asp:Label>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                     <tr>
                                                        <td width="2%">&nbsp;</td>
                                                        <td>简介：</td>
                                                        <td width="20%">    <textarea id="txtIntroduction" name="txtIntroduction"  runat="server"></textarea></td>
                                                        <td>设置成员简介<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIntroduction"
                                                             CssClass="field-validation-error" ErrorMessage=" 成员简介是必填字段。" ForeColor="Red" /></td>
                                                        <td width="2%">&nbsp;</td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td colspan="3"> <asp:Button ID="btnSubmit" runat="server" Text="提交"  OnClick="btnSubmit_Click" /></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%">&nbsp;</td>
                        </tr>
                        <!-- 添加产品结束 -->
                        <tr>
                            <td height="40" colspan="4">
                                <table width="100%" height="1" border="0" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC">
                                    <tr><td></td></tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="2%">&nbsp;</td>
                            <td width="51%" class="left_txt">
                                 copyright 2015 by 爱特工作室 all rights reserved
                            </td>
                            <td>&nbsp;</td><td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td background="./Images/mail_right_bg.gif">&nbsp;</td>
            </tr>
            <!-- 底部部分 -->
            <tr>
                <td valign="bottom" background="./Images/mail_left_bg.gif">
                    <img src="./Images/buttom_left.gif" width="17" height="17" />
                </td>
                <td background="./Images/buttom_bgs.gif">
                    <img src="./Images/buttom_bgs.gif" width="17" height="17">
                </td>
                <td valign="bottom" background="./Images/mail_right_bg.gif">
                    <img src="./Images/buttom_right.gif" width="16" height="17" />
                </td>           
            </tr>
        </table>
            </form>
    </body>
</html>
