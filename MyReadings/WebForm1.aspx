<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="MyReadings.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial, Helvetica, sans-serif;
        }
        .layout-table {
            width: 100%;
            table-layout: fixed;
            border-collapse: collapse;
        }
        .layout-table td {
            vertical-align: top;
            border: 1px solid #ccc;
            padding: 8px;
        }
        .col-third {
            width: 33.33%;
        }
        .ListBoxLeft {
            width: 100%;
            height: 400px;
        }
        .form-row {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }
        .form-label {
            width: 110px;
            flex: 0 0 110px;
            font-weight: bold;
            padding-right: 8px;
            text-align: right;
        }
        .form-control {
            flex: 1;
            max-width: 400px;
        }
        .form-control textarea,
        .form-control input[type="text"] {
            width: 100%;
            box-sizing: border-box;
            border: 1px solid #aaa;
            border-radius: 6px;
            padding: 6px 10px;
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="layout-table">
                <tr>
                    <td class="col-third">
                        <asp:ListBox ID="ListBox1" runat="server" CssClass="ListBoxLeft"
                            AutoPostBack="true" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                            <asp:ListItem Text="Title Breaf" Value="Title Breaf"></asp:ListItem>
                            <asp:ListItem Text="figure" Value="figure"></asp:ListItem>
                            <asp:ListItem Text="author" Value="author"></asp:ListItem>
                            <asp:ListItem Text="Paragraph" Value="Paragraph"></asp:ListItem>
                            <asp:ListItem Text="Pull Quote" Value="Pull Quote"></asp:ListItem>
                            <asp:ListItem Text="author-bio" Value="author-bio"></asp:ListItem>
                        </asp:RadioButtonList>

                        <br />

                        <asp:Panel ID="pnlTitle" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="Titled" Text="Titled:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="Titled" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlTitleBreaf" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="Breaf" Text="Breaf:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="Breaf" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlFigure" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="src" Text="src:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="src" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="alt" Text="alt:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="alt" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="figcaption" Text="figcaption:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="figcaption" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlAuthor" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="Auther" Text="Auther:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="Auther" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="dateline" Text="dateline:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="dateline" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="Publine" Text="Publine:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="Publine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlParagraph" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="paragraph" Text="paragraph:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="paragraph" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlPullQuote" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="pullQuote" Text="Pull Quote:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="pullQuote" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlAuthorBio" runat="server" Visible="false">
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" Text="Auther:"></asp:Label>
                                <div class="form-control">
                                    <asp:Label ID="LabelAutherBio" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Label runat="server" CssClass="form-label" AssociatedControlID="Info" Text="Info:"></asp:Label>
                                <div class="form-control">
                                    <asp:TextBox ID="Info" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <br />

                        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="Button1" runat="server" Text="Write HTML" OnClick="Button1_Click" />
                        <br />
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
