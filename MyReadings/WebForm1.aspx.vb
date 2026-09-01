Public Class WebForm1
    Inherits System.Web.UI.Page

    Private Const FIELD_SEP As String = "|~|"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        ShowSelectedPanel()
    End Sub

    Private Sub ShowSelectedPanel()
        pnlTitle.Visible = False
        pnlTitleBreaf.Visible = False
        pnlFigure.Visible = False
        pnlAuthor.Visible = False
        pnlParagraph.Visible = False
        pnlAuthorBio.Visible = False

        Select Case RadioButtonList1.SelectedValue
            Case "Title"
                pnlTitle.Visible = True
            Case "Title Breaf"
                pnlTitleBreaf.Visible = True
            Case "figure"
                pnlFigure.Visible = True
            Case "author"
                pnlAuthor.Visible = True
            Case "Paragraph"
                pnlParagraph.Visible = True
            Case "author-bio"
                LabelAutherBio.Text = Auther.Text
                pnlAuthorBio.Visible = True
        End Select
    End Sub

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If RadioButtonList1.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Dim selectedValue As String = RadioButtonList1.SelectedValue
        Dim firstComponentText As String = String.Empty
        Dim itemValue As String = String.Empty

        Select Case selectedValue
            Case "Title"
                firstComponentText = Titled.Text
                itemValue = selectedValue & FIELD_SEP & Titled.Text
                Titled.Text = String.Empty

            Case "Title Breaf"
                firstComponentText = Breaf.Text
                itemValue = selectedValue & FIELD_SEP & Breaf.Text
                Breaf.Text = String.Empty

            Case "figure"
                firstComponentText = src.Text
                itemValue = selectedValue & FIELD_SEP & src.Text & FIELD_SEP & alt.Text & FIELD_SEP & figcaption.Text
                src.Text = String.Empty
                alt.Text = String.Empty
                figcaption.Text = String.Empty

            Case "author"
                firstComponentText = Auther.Text
                itemValue = selectedValue & FIELD_SEP & Auther.Text & FIELD_SEP & dateline.Text & FIELD_SEP & Publine.Text
                ' Note: Auther is intentionally NOT cleared here -
                ' its value is reused later to populate the author-bio label.
                dateline.Text = String.Empty
                Publine.Text = String.Empty

            Case "Paragraph"
                Dim ParagraphArr() As String
                ParagraphArr = Split(paragraph.Text, vbCrLf)

                For Each str As String In ParagraphArr
                    If Trim(str) = "" Then Continue For
                    Dim lineValue As String = selectedValue & FIELD_SEP & str
                    ListBox1.Items.Add(New ListItem(selectedValue & ":" & GetFirstWords(str, 5), lineValue))
                Next

                paragraph.Text = String.Empty

            Case "author-bio"
                firstComponentText = LabelAutherBio.Text
                itemValue = selectedValue & FIELD_SEP & LabelAutherBio.Text & FIELD_SEP & Info.Text
                Info.Text = String.Empty
        End Select

        If selectedValue <> "Paragraph" Then
            ListBox1.Items.Add(New ListItem(selectedValue & ":" & GetFirstWords(firstComponentText, 5), itemValue))
        End If

    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Dim parts() As String = ListBox1.SelectedItem.Value.Split(New String() {FIELD_SEP}, StringSplitOptions.None)
        Dim itemType As String = parts(0)

        If RadioButtonList1.Items.FindByValue(itemType) Is Nothing Then
            Exit Sub
        End If

        RadioButtonList1.SelectedValue = itemType
        ShowSelectedPanel()

        Select Case itemType
            Case "Title"
                Titled.Text = GetPart(parts, 1)

            Case "Title Breaf"
                Breaf.Text = GetPart(parts, 1)

            Case "figure"
                src.Text = GetPart(parts, 1)
                alt.Text = GetPart(parts, 2)
                figcaption.Text = GetPart(parts, 3)

            Case "author"
                Auther.Text = GetPart(parts, 1)
                dateline.Text = GetPart(parts, 2)
                Publine.Text = GetPart(parts, 3)

            Case "Paragraph"
                paragraph.Text = GetPart(parts, 1)

            Case "author-bio"
                LabelAutherBio.Text = GetPart(parts, 1)
                Info.Text = GetPart(parts, 2)
        End Select
    End Sub

    Private Function GetPart(ByVal parts() As String, ByVal index As Integer) As String
        If index < parts.Length Then
            Return parts(index)
        End If
        Return String.Empty
    End Function

    Private Function GetFirstWords(ByVal text As String, ByVal wordCount As Integer) As String
        If String.IsNullOrWhiteSpace(text) Then
            Return String.Empty
        End If

        Dim words() As String = text.Trim().Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim takeCount As Integer = Math.Min(wordCount, words.Length)
        Return String.Join(" ", words, 0, takeCount)
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sb As New System.Text.StringBuilder()
        Dim i As Integer = 0
        Dim documentTitleText As String = String.Empty

        ' Pre-scan to find the index of the last "Paragraph" item, so we know
        ' where to close the article-body div once we reach it below.
        Dim lastParagraphIndex As Integer = -1
        For idx As Integer = 0 To ListBox1.Items.Count - 1
            Dim scanParts() As String = ListBox1.Items(idx).Value.Split(New String() {FIELD_SEP}, StringSplitOptions.None)
            If scanParts(0) = "Paragraph" Then
                lastParagraphIndex = idx
            End If
        Next

        Dim articleBodyOpened As Boolean = False

        While i < ListBox1.Items.Count
            Dim item As ListItem = ListBox1.Items(i)
            Dim parts() As String = item.Value.Split(New String() {FIELD_SEP}, StringSplitOptions.None)
            Dim itemType As String = parts(0)

            Select Case itemType
                Case "Title"
                    Dim titledText As String = GetPart(parts, 1)
                    Dim brafText As String = String.Empty

                    ' If the very next item is the matching Title Breaf, pull its text in
                    ' and skip it so it isn't processed again on its own.
                    If i + 1 < ListBox1.Items.Count Then
                        Dim nextParts() As String = ListBox1.Items(i + 1).Value.Split(New String() {FIELD_SEP}, StringSplitOptions.None)
                        If nextParts(0) = "Title Breaf" Then
                            brafText = GetPart(nextParts, 1)
                            i += 1
                        End If
                    End If

                    ' Remember the first Title's text so we can build the output filename.
                    If documentTitleText = String.Empty Then
                        documentTitleText = titledText
                    End If

                    sb.AppendLine("            <header class=""article-header"">")
                    sb.AppendLine("                <h1 class=""headline"">" & titledText & "</h1>")
                    sb.AppendLine("                <p class=""dek"">" & brafText & "</p>")
                    sb.AppendLine("            </header>")

                Case "Title Breaf"
                    ' Standalone Title Breaf with no preceding Title item.
                    sb.AppendLine("            <header class=""article-header"">")
                    sb.AppendLine("                <h1 class=""headline""></h1>")
                    sb.AppendLine("                <p class=""dek"">" & GetPart(parts, 1) & "</p>")
                    sb.AppendLine("            </header>")

                Case "figure"
                    Dim srcText As String = GetPart(parts, 1)
                    Dim altText As String = GetPart(parts, 2)
                    Dim figcaptionText As String = GetPart(parts, 3)

                    sb.AppendLine("            <figure>")
                    sb.AppendLine("                <img src=""images/" & srcText & """ alt=""" & altText & """>")
                    sb.AppendLine("                <figcaption>" & figcaptionText & "</figcaption>")
                    sb.AppendLine("            </figure>")

                Case "author"
                    Dim autherText As String = GetPart(parts, 1)
                    Dim datelineText As String = GetPart(parts, 2)
                    Dim publineText As String = GetPart(parts, 3)

                    sb.AppendLine("            <div class=""byline-block"">")
                    sb.AppendLine("                <span class=""by"">By " & autherText & "</span>")
                    If datelineText <> "" Then
                        sb.AppendLine("                <span class=""dateline"">" & datelineText & "</span>")
                    End If
                    sb.AppendLine("                <span class=""pubdate"">" & publineText & "</span>")
                    sb.AppendLine("            </div>")

                Case "Paragraph"
                    Dim paragraphText As String = GetPart(parts, 1)

                    If Not articleBodyOpened Then
                        sb.AppendLine("            <div class=""article-body"">")
                        articleBodyOpened = True
                    End If

                    sb.AppendLine("<p>" & paragraphText & "</p>")

                    If i = lastParagraphIndex Then
                        sb.AppendLine("            </div>")
                    End If

                Case "author-bio"
                    Dim bioNameText As String = GetPart(parts, 1)
                    Dim infoText As String = GetPart(parts, 2)

                    sb.AppendLine("            <div class=""author-bio"">")
                    sb.AppendLine("                <p><strong>" & bioNameText & "</strong>" & infoText & "</p>")
                    sb.AppendLine("            </div>")

            End Select

            i += 1
        End While

        Dim htmlOutput As String = sb.ToString()

        Try
            ' Assumes Template.txt lives in the App_Data folder - adjust the path below if it's elsewhere.
            Dim templatePath As String = Server.MapPath("~/App_Data/Template.txt")

            If Not System.IO.File.Exists(templatePath) Then
                lblStatus.ForeColor = System.Drawing.Color.Red
                lblStatus.Text = "Template.txt was not found at: " & templatePath
                Exit Sub
            End If

            Dim templateText As String = System.IO.File.ReadAllText(templatePath)
            Dim finalHtml As String = templateText.Replace("[[[[Article]]]]", htmlOutput)

            Dim safeFileName As String = documentTitleText.Replace(" ", "") & ".html"
            Dim outputPath As String = Server.MapPath("~/App_Data/" & safeFileName)

            System.IO.File.WriteAllText(outputPath, finalHtml)

            lblStatus.ForeColor = System.Drawing.Color.Green
            lblStatus.Text = "Saved: " & safeFileName
        Catch ex As Exception
            lblStatus.ForeColor = System.Drawing.Color.Red
            lblStatus.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class