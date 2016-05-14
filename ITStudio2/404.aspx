<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-CN">
<head>
    <title>爱特工作室 - 后台管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="爱特工作室 - 后台管理系统" />
    <meta name="keywords" content="爱特工作室 - 后台管理系统" />
    <meta name="Author" content="爱特工作室 - 后台管理系统" />
    <meta name="CopyRight" content="爱特工作室 - 后台管理系统" />
</head>
<frameset rows="64,*"  frameborder="no" border="0" framespacing="0">
    <!--头部-->
    <frame src="./top.aspx" name="top" noresize="noresize" frameborder="0"  scrolling="no" marginwidth="0" marginheight="0" />
    <!--主体部分-->
    <frameset cols="185,*">
        <!--主体左部分-->
        <frame src="./left.aspx" name="left" noresize="noresize" frameborder="0" scrolling="no" marginwidth="0" marginheight="0" />
        <!--主体右部分-->
        <frame src="./error.aspx" name="main" frameborder="0" scrolling="auto" marginwidth="0" marginheight="0" />
</frameset>
</html>