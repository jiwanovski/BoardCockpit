INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('1', 'EK-Quote','Eigenkapitalquote', '1', 'Stellt den Anteil des Eigenkapitals am Gesamtkapital dar','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('1','bs.eqLiab.equity / bs.eqLiab * 100','1')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('2','FK-Struktur','Fremdkapitalstruktur', '1', 'Anteil kurzfristiges Fremdkapital an Verbindlichkeiten','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('2','( bs.eqLiab.liab.upTo1year + bs.eqLiab.liab.trade ) / bs.eqLiab.liab * 100','2')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('3','DSO','Days Sales Outstanding', '3', 'Kundenziel (Tage)','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('3','bs.ass.currAss.receiv.trade / ( is.netIncome.regular.operatingTC.grossTradingProfit * 1.19 ) * 360','3')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('4','Anlagendeckung','Anlagendeckung', '1', 'Anteil AV, welches durch Eigenkapital gedeckt ist','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('4','bs.eqLiab.equity / bs.ass.fixAss * 100','4')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('5','Working Capital Ratio','Working Capital Ratio', '1', 'Anteil der kurzfristigen Verbindlichkeiten, die durch das Umlaufvermögen gedeckt sind','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('5','bs.ass.currAss / ( bs.eqLiab.liab.upTo1year + bs.eqLiab.liab.trade ) * 100','5')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('7','ROI','Return on Investment', '1', 'Rendite des eingesetzten Kapitals','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('7','( is.netIncome.regular.operatingTC / is.netIncome.regular.operatingTC.grossTradingProfit ) * is.netIncome.regular.operatingTC.grossTradingProfit / bs.ass * 100','7')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('8','EBIT Marge','Earnings before Interest and Taxes', '1', 'Anteil des Ergebnisses vor Steuern und Zinsen am Umsatz','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('8','( is.netincome - is.netIncome.extraord - is.netIncome.tax + is.netIncome.regular.fin.netInterest.expenses ) / is.netIncome.regular.operatingTC.grossTradingProfit * 100','8')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('9','EBITDA Rentabilität','Earnings before Interest, Taxes, Depreciation and Amortization Rentabilität', '1', 'Anteil des Gesamtkapital, welches dem Unternehmen aus operativer Tätigkeit zugeflossen ist','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('9','( is.netincome - is.netIncome.extraord - is.netIncome.tax + is.netIncome.regular.fin.netInterest.expenses + is.netIncome.regular.operatingTC.deprAmort ) / is.netIncome.regular.operatingTC.grossTradingProfit * is.netIncome.regular.operatingTC.grossTradingProfit / bs.ass * 100','9')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('10','EK-Rentabilität','Eigenkapitalrentabilität', '1', 'Verzinsung des eingesetzten Kapitals','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('10','is.netincome / bs.eqLiab.equity * 100','10')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('11','EVA','Economic Value Added', '3', 'Wertzuwachs des Unternehmens','1', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('11','( ( is.netIncome - is.netIncome.extraord - is.netIncome.tax + is.netIncome.regular.fin.netInterest.expenses ) * ( 1 - 0.3 ) ) - ( ( bs.ass.fixAss + bs.ass.currass - bs.eqLiab.liab.upTo1year - bs.eqliab.liab.trade - bs.eqliab.liab.other ) * ( bs.eqLiab.equity / bs.eqLiab * 0.06 + bs.eqliab.liab / bs.eqliab * 0.02 ) )','11')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('12','EBITDA','Earnings before Interest, Taxes, Depreciation and Amortization', '3', 'Operatives Ergebnis vor Zinsen, Steuern und Abschreibung','3', null)

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('12','is.netincome - is.netIncome.extraord - is.netIncome.tax + is.netIncome.regular.fin.netInterest.expenses + is.netIncome.regular.operatingTC.deprAmort','12')

INSERT INTO Formula (FormulaID, Name, Description, ChartType, ToolTipDescription, Unit, LinkedFormulaID) VALUES ('6','Entschuldungsgrad','Entschuldungsgrad', '3', 'Welcher Prozentsatz der Netto Verschuldung könnte in einer Periode zurückgezahlt werden','1', '12')

INSERT INTO FormulaDetail (FormulaDetailID, FormulaExpression, FormulaID) VALUES ('6','( is.netincome - is.netIncome.extraord - is.netIncome.tax + is.netIncome.regular.fin.netInterest.expenses + is.netIncome.regular.operatingTC.deprAmort ) / ( bs.eqLiab.liab.securities + bs.eqLiab.liab.bank - bs.ass.currAss.cashEquiv - bs.ass.currAss.securities ) * 100','6')
