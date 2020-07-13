@echo off
rem wymaga polecenia antlr4, najłatwiej dodać poprzez instalację pakietu chocolatey:
rem choco install antlr4

antlr4 -package StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr -Dlanguage=CSharp -visitor Vtl.g4