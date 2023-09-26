namespace ExcelToPDF
{
    public class HTMLTemplate
    {
        public string BuildTemplate(
            string documentNumber,
            string employeeJoinDate,
            string contractNumber,
            string employeeName,
            string employeeAddress,
            string employeeCNP,
            string startDateAtNewOffice,
            string documentCreatedDate
        )
        {
            string html = $@"
            <div>
                <div style=""clear:both"">
                    <div style=""text-align:right"">
                        <table cellspacing=""0"" cellpadding=""0"" style=""margin-left:auto; border-collapse:collapse"">
                            <tr style=""height:81pt"">
                                <td style=""width:213.1pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                                    <p style=""margin-top:0pt; margin-bottom:0pt; font-size:12pt"">
                                        <img src=""data:image/png;base64,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"" width=""118"" height=""74"" alt="""">
                                    </p>
                                </td>
                                <td style=""width:76.7pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                                    <p style=""margin-top:0pt; margin-bottom:0pt; text-align:right; font-size:12pt"">&#xa0;</p>
                                </td>
                                <td style=""width:125.6pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                                    <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                                        <span style=""font-family:Satoshi"">IT Perspectives SRL</span>
                                    </p>
                                    <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                                        <span style=""font-family:Satoshi"">Sibiu 550076</span>
                                    </p>
                                    <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                                        <span style=""font-family:Satoshi"">Str. Gral Dragalina Nr. 1</span>
                                    </p>
                                    <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                                        <span style=""font-family:Satoshi"">office@itperspectives.ro</span>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <p style=""margin-top:0pt; margin-bottom:0pt"">&#xa0;</p>
                </div>
                <table cellspacing=""0"" cellpadding=""0"" style=""border-collapse:collapse"">
                    <tr>
                        <td style=""width:421.2pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                            <p style=""margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:14pt; background-color:#ffffff"">
                                <strong>
                                    <span style=""font-family:'Calibri Light'; "">ACT ADIŢIONAL Nr.</span>
                                </strong>
                                <strong>
                                    <span style=""font-family:'Calibri Light'; "">&#xa0; </span>
                                </strong>
                                <strong>
                                    <span style=""font-family:'Calibri Light'; "">&nbsp;&nbsp;{documentNumber}&nbsp;&nbsp;&nbsp; / &nbsp; &nbsp;&nbsp;&nbsp;{employeeJoinDate}&nbsp;&nbsp;&nbsp;</span>
                                </strong>
                            </p>
                        </td>
                    </tr>
                </table>
                <p style=""margin-top:0pt; margin-bottom:0pt; text-align:center; font-size:10pt"">
                    <span style=""font-family:'Calibri Light'"">la contractul individual de munca incheiat si inregistrat sub</span>
                    <span style=""font-family:'Calibri Light'"">&#xa0; </span><span style=""font-family:'Calibri Light'"">nr.</span>
                    <span style=""font-family:'Calibri Light'"">&#xa0; </span><span style=""font-family:'Calibri Light'""> &nbsp;&nbsp;&nbsp;<b>{contractNumber}</b>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </p>
                <p style=""margin-top:0pt; margin-bottom:0pt; font-size:10pt"">
                    <span style=""font-family:'Calibri Light'"">&#xa0;</span>
                </p>
                <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                    <strong><span style=""font-family:'Calibri Light'; "">Partile:</span></strong>
                </p>
                <p style=""margin-top:0pt; margin-bottom:15.75pt; text-align:justify; font-size:11pt; background-color:#ffffff"">
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">Angajatorul,</span>
                    </strong>
                    <span style=""font-family:'Calibri Light'""> IT Perspectives SRL, cu sediul social in Str. Fundatura Lanii, Nr.18, Sibiu, Jud.Sibiu, </span>
                    <span style=""font-family:'Calibri Light'"">&#xa0;</span><span style=""font-family:'Calibri Light'"">inregistrata la Registrul Comerțului Sibiu sub nr. J32/575/2004, CUI 16341004, reprezentata legal de domnii</span>
                    <span style=""font-family:'Calibri Light'"">&#xa0; </span>
                    <span style=""font-family:'Calibri Light'"">administratori, Secosan Radu Cosmin si Cristian Cimpineanu, </span>
                </p>
                <p style=""margin-top:0pt; margin-bottom:15.75pt; text-align:justify; font-size:11pt; background-color:#ffffff"">
                    <strong><span style=""font-family:'Calibri Light'; "">si</span></strong>
                </p>
                <p style=""margin-top:0pt; margin-bottom:15.75pt; text-align:justify; font-size:11pt; background-color:#ffffff"">
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">Angajatul/angajata</span>
                    </strong>
                    <span style=""font-family:'Calibri Light'""> – domnul/doamna</span><span style=""font-family:'Calibri Light'"">&#xa0;&#xa0;&#xa0;&#xa0; </span>
                    <span style=""font-family:'Calibri Light'""> &nbsp;&nbsp;&nbsp;&nbsp; <b>{employeeName}</b> &nbsp;&nbsp;&nbsp;&nbsp; 
                        <span>,</span><br> domiciliat/a
                    </span>
                    <span style=""font-family:'Calibri Light'"">&#xa0; </span>
                    <span style=""font-family:'Calibri Light'"">&nbsp;&nbsp;&nbsp;&nbsp; in </span>
                    <span style=""font-family:'Calibri Light'; background-color:#ffffff""> &nbsp;&nbsp;&nbsp;&nbsp; <b>{employeeAddress}</b> &nbsp;&nbsp;&nbsp;&nbsp;
                    <span>,</span>&nbsp;&nbsp;&nbsp; </span>
                    <span style=""font-family:'Calibri Light'; background-color:#ffffff"">&#xa0; </span>
                    <span style=""font-family:'Calibri Light'"">avand</span>
                    <span style=""font-family:'Calibri Light'"">&#xa0; </span>
                    <span style=""font-family:'Calibri Light'""> &nbsp;&nbsp;&nbsp;&nbsp; CNP &nbsp;&nbsp;&nbsp;&nbsp; <b>{employeeCNP}</b> &nbsp; .</span>
                </p>
                <p style=""margin-top:0pt; margin-bottom:15.75pt; text-align:justify; font-size:11pt; background-color:#ffffff"">
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">În temeiul art. 17</span>
                    </strong>
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">&#xa0; </span>
                    </strong>
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">coroborat cu art. 41. 3. b</span>
                    </strong>
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">&#xa0; </span>
                    </strong>
                    <strong>
                        <span style=""font-family:'Calibri Light'; "">din Legea nr. 53/2003</span>
                    </strong>
                    <span style=""font-family:'Calibri Light'"">, hotarasc:</span>
                </p>
                <ol type=""1"" style=""margin:0pt; padding-left:0pt"">
                    <li style=""margin-left:31.27pt; margin-bottom:15.75pt; text-align:justify; padding-left:4.73pt; font-family:'Calibri Light'; font-size:11pt; background-color:#ffffff""> Se modifica elementul 
                        <strong>
                            <span style=""font-family:'Arial Narrow'; background-color:#ffffff"">D.Locul de  muncă</span>
                        </strong>
                        <span style=""font-family:'Arial Narrow'; background-color:#ffffff"">&#xa0;</span>al contractului individual de munca,&#xa0;&#xa0; si va avea urmatoarele prevederi: salariatul va lucra de la&#xa0; noul sediul&#xa0; social al angajatorului din Str. Fundatura Lanii, Nr.18, Sibiu, Jud. Sibiu si in regim de telemunca de la adresa de domiciliu a angajatului/angajatei din&#xa0; <b>{employeeAddress}</b> &nbsp;
                        <span>,</span> &nbsp; incepand cu data de 
                        <strong>{startDateAtNewOffice} .</strong>
                    </li>
                    <li style=""margin-left:31.27pt; margin-bottom:15.75pt; padding-left:4.73pt; font-family:'Calibri Light'; font-size:11pt; background-color:#ffffff""> 
                        Celelalte elemente constitutive ale contractului individual de muncă&#xa0; mai sus mentionat raman neschimbate.
                    </li>
                </ol>
                <p style=""margin-top:0pt; margin-bottom:15.75pt; text-align:justify; font-size:11pt; background-color:#ffffff"">
                    <span style=""font-family:'Calibri Light'"">Prezentul act aditional a fost incheiat in 2 exemplare, cate unul pentru fiecare parte. </span>
                </p>
                <table cellspacing=""0"" cellpadding=""0"" style=""border-collapse:collapse"">
                    <tr style=""height:22.45pt"">
                        <td style=""width:229.9pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                            <h2 style=""margin-top:2pt; margin-bottom:0pt; text-align:center; page-break-inside:avoid; page-break-after:avoid; font-size:11pt""> 
                                <span style=""font-family:'Calibri Light'; color:#365f91"">Angajator,</span>
                            </h2>
                        </td>
                        <td style=""width:229.95pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                            <h2 style=""margin-top:2pt; margin-bottom:0pt; text-align:center; page-break-inside:avoid; page-break-after:avoid; font-size:11pt"">
                                <span style=""font-family:'Calibri Light'; color:#365f91"">Angajat,</span>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td style=""width:229.9pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                            <h2 style=""margin-top:2pt; margin-bottom:0pt; text-align:center; page-break-inside:avoid; page-break-after:avoid; font-size:11pt"">
                                <span style=""font-family:'Calibri Light'; color:#365f91"">IT Perspectives SRL</span>
                            </h2>
                        </td>
                        <td style=""width:229.95pt; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top"">
                            <h2 style=""margin-top:2pt; margin-bottom:0pt; text-align:center; page-break-inside:avoid; page-break-after:avoid; font-size:11pt"">
                                <span style=""font-family:'Calibri Light'; color:#365f91"">&nbsp;&nbsp;&nbsp;&nbsp;{employeeName}&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                <span style=""font-family:'Calibri Light'; color:#365f91"">&#xa0; </span>
                            </h2>
                        </td>
                    </tr>
                </table>
                <h2 style=""margin-top:2pt; margin-bottom:0pt; page-break-inside:avoid; page-break-after:avoid; font-size:11pt"">
                    <span style=""font-family:'Calibri Light'; color:#365f91"">&#xa0;</span>
                </h2>
                <h2 style=""margin-top:2pt; margin-bottom:0pt; page-break-inside:avoid; page-break-after:avoid; font-size:11pt"">
                    <span style=""font-family:'Calibri Light'; color:#365f91"">              </span>
                    <span style=""width:1.18pt; font-family:'Calibri Light'; display:inline-block"">&#xa0;</span>
                    <span style=""font-family:'Calibri Light'; color:#365f91"">....................................................</span>
                    <span style=""font-family:'Calibri Light'; color:#365f91"">&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0; </span>
                    <span style=""font-family:'Calibri Light'; color:#365f91"">..................................................</span>
                    <span style=""width:5.95pt; font-family:'Calibri Light'; display:inline-block"">&#xa0;</span>
                    <span style=""font-family:'Calibri Light'; color:#365f91"">              </span>
                    <span style=""font-family:'Calibri Light'; color:#365f91"">&#xa0;&#xa0;&#xa0;&#xa0; </span>
                </h2>
                <h2 style=""margin-top:2pt; margin-bottom:0pt; page-break-inside:avoid; page-break-after:avoid; font-size:11pt"">
                    <span style=""font-family:'Calibri Light'; color:#365f91"">&#xa0;&#xa0;&#xa0;&#xa0; </span>
                    <span style=""font-family:'Calibri Light'; font-weight:normal; color:#365f91"">&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;</span>
                </h2>
                <p style=""margin-top:0pt; margin-bottom:0pt; text-align:justify; font-size:11pt"">
                    <span style=""font-family:'Calibri Light'"">Subsemnatul/subsemnata</span>
                    <span style=""font-family:'Calibri Light'"">&#xa0;&#xa0; </span>
                    <span style=""font-family:'Calibri Light'""> &nbsp;&nbsp; <b>......................................</b> &nbsp; , </span>
                    <span style=""font-family:'Calibri Light'"">&#xa0; </span>
                    <span style=""font-family:'Calibri Light'"">am luat la cunostinta si am primit un exemplar in data de</span>
                    <span style=""font-family:'Calibri Light'"">&#xa0;</span>
                    <span style=""font-family:'Calibri Light'"">{documentCreatedDate} .</span>
                </p>
                <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                    <span style=""font-family:'Calibri Light'"">Semnatura:</span>
                </p>
                <p style=""margin-top:0pt; margin-bottom:0pt; font-size:11pt"">
                    <span style=""font-family:'Calibri Light'"">........................................................................................</span>
                </p>
            </div>
            ";
            return html;
        }
    }
}
